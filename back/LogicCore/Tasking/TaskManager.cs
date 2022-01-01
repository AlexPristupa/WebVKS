using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using LogicCore.Extensions;
using LogicCore.Collection;

namespace LogicCore.Tasking
{
    /// <summary>
    /// Менеджер сборщиков
    /// </summary>
    public class TaskManager : Disposable
    {
        private ILogger _logger;
        private readonly IDictionary<string, TaskContext> _contextByName = new DictionaryInfo<string, TaskContext>();
        private readonly ConcurrentQueue<TaskContext> _queueItems = new ConcurrentQueue<TaskContext>();

        private readonly TaskFactory _taskFactory;
        private List<Task> _tasks = new List<Task>(); //активные рабочие задачи
        private CancellationTokenSource _cancelation = new CancellationTokenSource(); //дескриптор отмены потоков
        private Task _mainTask; //головная задача сборщика
        private Timer _timerChange;
        private readonly EventWaitHandle _waitHandle = new ManualResetEvent(false);
        private readonly object _locker = new object();
        private readonly object _timerLocker = new object();
        private readonly ISchedulerStore _store;
        private static readonly ISchedulerStore _globalStore = new SchedulerStore();
        private DateTime _lastDriveStatistics;
        private bool _refresh;

        public event Func<TimeSpan?> PrepareForTasksProcessing;
        public event Func<TaskContext, bool> TaskChanged;
        public event Action TaskQueueCompleted;
        public event Action TaskQueueSkipped;
        public event Func<List<TaskContext>, IEnumerable<TaskContext>> PrepareTaskQueue;

        public TaskManager(bool async = false)
        {
            _store = _globalStore;
            if (async)
            {
                _taskFactory = new TaskFactory(TaskCreationOptions.RunContinuationsAsynchronously, TaskContinuationOptions.None);
                Async = true;
            }
            else
            {
                _taskFactory = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.LongRunning);
            }
            TaskParallelCount = GetParallelCount();
        }

        public TaskManager(ISchedulerStore store, bool async = false)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            if (async)
            {
                _taskFactory = new TaskFactory(TaskCreationOptions.RunContinuationsAsynchronously, TaskContinuationOptions.None);
                Async = true;
            }
            else
            {
                _taskFactory = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.LongRunning);
            }
            TaskParallelCount = GetParallelCount();
        }

        public static TaskManager FromConfig(string threadPool, int? syncAutoCount = null, int? asyncAutoCount = null)
        {
            threadPool = threadPool.ToLower();
            if (threadPool.Contains("async"))
            {
                var countS = threadPool.Replace("async", "");
                if (countS.Any())
                    return new TaskManager(true) { TaskParallelCount = int.Parse(countS) };
                return new TaskManager(true) { TaskParallelCount = asyncAutoCount ?? Environment.ProcessorCount * 40 };
            }
            else
            {
                if (threadPool.Equals("auto", StringComparison.OrdinalIgnoreCase))
                    return new TaskManager(false) { TaskParallelCount = syncAutoCount ?? Environment.ProcessorCount * 4 };
                if (threadPool.Any())
                    return new TaskManager(false) { TaskParallelCount = int.Parse(threadPool) };
                return new TaskManager(false);
            }
        }

        public bool Async { get; }

        public CancellationTokenSource Cancelation { get { return _cancelation; } }

        public int TaskParallelCount { get; set; }

        public IEnumerable<TaskContext> Queue => _queueItems;

        public TimeSpan MaxWaitingInterval { get; set; } = TimeSpan.FromHours(1);

        public TimeSpan MaxTimeoutInterval { get; set; } = TimeSpan.FromMinutes(5);

        public Task[] ActiveTasks
        {
            get { lock (_locker) return _tasks.ToArray(); }
        }

        public ISchedulerStore Store => _store;

        public IEnumerable<TaskContext> Contexts
        {
            get
            {
                lock (_locker) { return _contextByName.Values.ToArray(); }
            }
        }

        //Переопросить все задачи
        public void Refresh()
        {
            _logger.Debug("Переопрос готовности задач");
            _refresh = true;
            _waitHandle.WaitOne();
        }

        int GetParallelCount()
        {
            return Async ? Environment.ProcessorCount * 50 : Environment.ProcessorCount * 3;
        }
        /// <summary>
        /// Инициализация сборщика
        /// </summary>
        public void Initialize(ILogger logger)
        {
            _logger = logger;
            _store.Prepare();
            var mode = Async ? "Асинхронный" : "Многопоточный";
            _logger.Info($"Режим работы диспетчера потоков: {mode}");
        }

        /// <summary>
        /// Запуск сборщика согласно установленной конфигурации
        /// </summary>
        public void Start(bool waitRun = false)
        {
            if (_mainTask != null)
                throw new InvalidOperationException("Задачи уже запущены");
            if (_cancelation == null)
                _cancelation = new CancellationTokenSource();
            if (_logger == null)
                _logger = new ConsoleLogger();
            _mainTask = _taskFactory.StartNew(InternalProcessing, TaskCreationOptions.LongRunning);
            if (waitRun)
                _waitHandle.WaitOne();
        }

        //определяем необходимый набор запускаемых задач
        void InternalProcessing()
        {
            while (_cancelation != null && _cancelation.IsCancellationRequested == false)
            {
                try
                {
                    _logger.Info($"Начало обработки{Environment.NewLine}");
                    GC.Collect(); //дополнительно чистим память перед началом работы
                    _logger.Debug("Задействовано памяти перед началом: {0:0.0} мб", 
                        GC.GetTotalMemory(false) / (1024d * 1024d));
                    _logger.Debug("Выделено памяти процессу: {0:0.0} мб", 
                        Process.GetCurrentProcess().WorkingSet64 / (1024d * 1024d));
                    _logger.Info("Количество {0} потоков: {1}", Async ? "асинхронных" : "параллельных", TaskParallelCount);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
                var now = DateTime.Now;
                try
                {
                    _cancelation?.Token.ThrowIfCancellationRequested();
                    if (PrepareAndStartTasks(now) == false)
                    {
                        _logger.Debug("Пропускаем выполнение из-за невыполненных условий");
                        continue;
                    }
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
                finally
                {
                    _waitHandle.Set();   //уведомление о запуске задач
                    _waitHandle.Reset(); //сбрасываем состояние
                }

                //определяем время ожидания ближайшей задачи, ждем
                TimeSpan wait = MaxWaitingInterval;
                try
                {
                    _cancelation?.Token.ThrowIfCancellationRequested();
                    var minTimeContext = _contextByName.Any() ? _contextByName.Values.OrderBy(item => item.NextTimeProcessing).First() : null;
                    if (minTimeContext != null)
                        _logger.Debug($"Ближайшее время запуска {minTimeContext.NextTimeProcessing:dd MMM HH:mm} у {minTimeContext.Name}");
                    var nextTime = minTimeContext?.NextTimeProcessing ?? DateTime.MaxValue; //берем минимальное ближайшее время

                    if (nextTime < DateTime.MaxValue)
                    {
                        now = DateTime.Now;
                        wait = nextTime - now; //берем минимальное ближайшее время
                        if (wait.TotalSeconds < 1)
                        {
                            wait = TimeSpan.FromSeconds(1); //минимум одна секунда
                        }
                    }
                    else
                    {
                        wait -= DateTime.Now - now; //вычитаем ожидание запуска
                    }
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
                try
                {
                    if (wait > MaxWaitingInterval) wait = MaxWaitingInterval;
                    _logger.Debug($"Переходим в ожидание до ближайшей задачи {(int)wait.TotalHours}ч {(int)wait.Minutes}м {(int)wait.Seconds}с");
                    _cancelation?.Token.ThrowIfCancellationRequested();
                    _waitHandle.WaitOne(wait);
                    _waitHandle.Reset(); //любой сброс после означает повторный опрос
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        private bool PrepareAndStartTasks(DateTime now)
        {
            //уведомляем о начале
            try
            {
                var timeout = PrepareForTasksProcessing?.Invoke();
                if (timeout != null)
                {
                    _logger.Debug($"Отсрочка запуска задач на " + timeout.Value.ToString());
                    _waitHandle.WaitOne(timeout.Value);
                    _waitHandle.Reset(); //любой сброс после означает повторный опрос
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            //обновляем очередь для запуска
            try
            {
                _logger.Debug("Подготовка задач и формирование очереди");
                var newCount = UpdateQueueTaskForRun(now);
                //lock (_queueItems)
                {
                    _logger.Debug("Новых задач для запуска: " + newCount);
                    string tasksNames;
                    lock (_locker)
                        tasksNames = string.Join(Environment.NewLine, _queueItems.Select(t => t.Name));
                    if (tasksNames.Any())
                        _logger.Debug($"Текущая очередь:{Environment.NewLine}{tasksNames}");
                    else
                        _logger.Debug($"Очередь пустая");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            try
            {
                StartTasks();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            try
            {
                _logger.Debug("Задействовано памяти после запуска: {0:0.0} мб",
                    GC.GetTotalMemory(false) / (1024d * 1024d));
                ResourceDiagnostic.LogCpuMemory(_logger);
                if (now - _lastDriveStatistics > TimeSpan.FromHours(1))
                {
                    ResourceDiagnostic.LogDriveInfo(_logger);
                    _lastDriveStatistics = now;
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
            }
            return true;
        }

        public void Wait(TimeSpan timeSpan)
        {
            do
            {
                Task.WaitAll(_tasks.ToArray(), timeSpan);
            } while (_queueItems.Count > 0);// || _tasks.Any(t => t.Status == TaskStatus.WaitingForActivation));
        }

        private void StartTasks()
        {
            if (Monitor.TryEnter(_locker, MaxTimeoutInterval) == false)
            {
                throw new TimeoutException("Превышен интервал ожидания блокировки");
            }
            try
            {                //оставляем только активные задачи
                var tasks = _tasks.Where(t => t.IsCompleted == false && t.IsCanceled == false).ToList();
                if (TaskParallelCount <= 0)
                    TaskParallelCount = GetParallelCount();
                //запускаем недостающие
                //var queueNeeded = Math.Min(_queueItems.Count, TaskParallelCount);
                var diff = Math.Min(_queueItems.Count, TaskParallelCount - tasks.Count);
                _logger.Trace($"Количество дополнительно запускаемых конвееров: {diff}");
                //запускаем
                for (int i = 0; i < diff; i ++)
                {
                    if (_cancelation == null || _cancelation.IsCancellationRequested) break; //проверяем отмену
                    try
                    {
                        if (Async)
                        {
                            //var task = Task.Run(async () => await TasksProcessingAsync(i), _cancelation.Token);
                            var task = _taskFactory.StartNew(TasksProcessingAsync, i, _cancelation.Token);
                            tasks.Add(task);
                        }
                        else
                        {
                            var task = _taskFactory.StartNew(TasksProcessing, i, _cancelation.Token);
                            tasks.Add(task);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Ошибка при запуске потока");// + item.Name);
                    }
                };
                Thread.Sleep(0);//даем возможность новым потокам ожить
                _tasks = tasks; //обновляем задачи
                //_logger.Debug("Задачи запущены: " + items.Count);
                if (tasks.Count > 0)
                {
                    _taskFactory.ContinueWhenAll(tasks.ToArray(), t => AfterTaskingComplete());
                }
                else
                {
                    _taskFactory.StartNew(() => TaskQueueSkipped?.Invoke());
                }
            }
            finally
            {
                Monitor.Exit(_locker);
            }
        }

        private void TasksProcessing(object state)
        {
            try
            {
                _logger.Debug($"Конвеер {state} в обработке");
                TaskContext ctx = null;
                try
                {
                    while ((ctx = GetNextTaskForRun().Result) != null)
                    {
                        _cancelation?.Token.ThrowIfCancellationRequested();
                        try
                        {
                            ctx.Processing(_cancelation);
                        }
                        catch (Exception ex) when (ex.GetType() != typeof(OperationCanceledException))
                        {
                            _logger.Error(ex, $"Ошибка при выполнении задачи {ctx?.Name}");
                        }
                        finally
                        {
                            _store.Save(ctx);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"Ошибка при выполнении очереди задач ({ctx?.Name})");
                }
                CheckLostTask();
            }
            catch (OperationCanceledException ex)
            {
                _logger?.Debug(ex.Message);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex);
            }
        }

        private async Task TasksProcessingAsync(object state)
        {
            try
            {
                _logger.Trace($"Конвеер {state} в обработке");
                TaskContext ctx = null;
                try
                {
                    while ((ctx = await GetNextTaskForRun()) != null)
                    {
                        try
                        {
                            await ctx.ProcessingAsync(_cancelation);
                        }
                        catch (Exception ex) when (ex.GetType() != typeof(OperationCanceledException))
                        {
                            _logger.Error(ex, $"Ошибка при выполнении задачи {ctx?.Name}");
                        }
                        finally
                        {
                            _store.Save(ctx);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"Ошибка при выполнении очереди задач ({ctx?.Name})");
                }
                CheckLostTask();
            }
            catch (OperationCanceledException ex)
            {
                _logger?.Debug(ex.Message);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex);
            }
        }

        private void CheckLostTask()
        {
            _cancelation?.Token.ThrowIfCancellationRequested();
            //дополнительно проверяем отсутствие потерянных потоков
            if (Monitor.TryEnter(_locker, MaxTimeoutInterval) == false)
            {
                throw new TimeoutException("Превышен интервал ожидания блокировки");
            }
            try
            { 
                if (_tasks.Count < this.TaskParallelCount &&
                    _queueItems.Count > 0)
                {
                    _logger.Debug("Дополнительный запуск появившихся задач");
                    //вызываем доп. запуск
                    StartTasks();
                }
            }
            finally
            {
                Monitor.Exit(_locker);
            }
        }

        private void task_Changed(TaskContext task)
        {
            try
            {
                if (TaskChanged == null ||
                    TaskChanged(task) == false)
                {
                    //блокируем выполнение для пропуска нескольких уведомлений
                    lock (_timerLocker)
                    {
                        if (_timerChange == null)
                        {
                            var time = TimeSpan.FromSeconds(3);
                            _logger.Info($"Получено уведомление об изменении задачи {task.Name}. Ждем {time.TotalSeconds} сек");
                            //запускаем в отдельном потоке, для отсечения очереди событий при множественном уведомлении
                            _timerChange = new Timer(BeginProcessing, null, time, TimeSpan.FromMilliseconds(-1));
                        }
                    }
                }
            } 
            catch(Exception ex)
            {
                _logger.Error(ex, $"Ошибка при изменении задачи {task?.Name}");
            }
        }

        private void BeginProcessing(object state)
        {
            try
            {
                _logger.Trace("BeginProcessing");
                lock (_timerLocker)
                {
                    if (Disposed == false)
                    {
                        _waitHandle.Set(); //отпускаем ожидание
                        _timerChange?.Dispose();
                        _timerChange = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        [Obsolete("Use RecheckTasks")]
        public void ForceCheckTask()
        {
            RecheckTasks();
        }

        public void RecheckTasks()
        {
            _waitHandle.Set(); //отпускаем ожидание и переопрашиваем задачи
        }

        internal int UpdateQueueTaskForRun(DateTime now)
        {
            List<TaskContext> contexts;
            lock(_locker) contexts = _contextByName.Values
                    .Where(c => _refresh || c.NextTimeProcessing <= now)
                    .ToList();

            //todo нужно проработать механизм фоновой подготовки задач к запуску для исключения зависания менеджера задач
            //var task = new Task<bool>(() => contexts.First().PrepareForProcessing(now));
            //var readyTasks = contexts
            //    .Select(item => Task.Run<bool>(() => item.PrepareForProcessing(now)))
            //    .ToArray();
            //if (Task.WaitAll(readyTasks, TimeSpan.FromSeconds(1)) == false)
            //{
            //}
            //foreach(var t in readyTasks)
            //{
            //    t.Status == TaskStatus.RanToCompletion
            //}

            var readyTask = contexts
                .Where(item => item.PrepareForProcessing(now))
                .OrderBy(item => item.Order)
                .ToList();

            if (Monitor.TryEnter(_locker, MaxTimeoutInterval) == false)
            {
                throw new TimeoutException("Превышен интервал ожидания блокировки");
            }
            try
            {
                if (PrepareTaskQueue != null)
                {
                    var result = PrepareTaskQueue(readyTask);
                    foreach (var task in result)
                    {
                        if (task.ChangeStatusToEnque())
                        {
                            _queueItems.Enqueue(task);
                        }
                    }
                }
                else
                {
                    var taskSet = readyTask.ToDictionary(t => t.Name);
                    //нужно еще отсортировать с учетом зависимостей
                    foreach (var task in readyTask)
                    {
                        if (taskSet.ContainsKey(task.Name))
                        {
                            if (task.Dependences != null)
                            {
                                ReadDependence(task, task, taskSet);
                            }
                            if (task.ChangeStatusToEnque())
                            {
                                _queueItems.Enqueue(task);
                            }
                            taskSet.Remove(task.Name);
                        }
                    }
                }
                return readyTask.Count;
            }
            finally
            {
                _refresh = false;
                Monitor.Exit(_locker);
            }
        }

        private void ReadDependence(TaskContext mainTask, TaskContext task, Dictionary<string, TaskContext> set)
        {
            foreach (var dep in task.Dependences)
            {
                if (set.TryGetValue(dep, out var depTask))
                {
                    if (depTask != mainTask &&
                        depTask.Dependences != null)
                    {
                        ReadDependence(mainTask, depTask, set);
                    }
                    if (depTask.ChangeStatusToEnque())
                    {
                        _queueItems.Enqueue(depTask);
                    }
                    set.Remove(dep);
                }
            }
        }

        public void RemoveContexts(IEnumerable<TaskContext> contexts)
        {
            lock (_locker)
            {
                foreach (var ctx in contexts)
                {
                    RemoveContext(ctx);
                }
            }
        }

        public void RemoveContext(TaskContext context)
        {
            lock (_locker)
            {
                if (_contextByName.Remove(context.Name) == false)
                {
                    throw new InvalidOperationException($"Ошибка при удалении задачи {context.Name}");
                }
                context.Changed -= task_Changed;
            }
            //пока затычка на ожидание завершения потока
            var count = 0;
            while (context.Used && count ++ < 3000) Thread.Sleep(10);
            context.Dispose();
        }

        public TaskContext FindTaskContext(string name)
        {
            lock(_locker)
                return _contextByName.Find(name);
        }

        public ITasking FindTask(string name)
        {
            lock(_locker)
                return _contextByName.Find(name)?.Task;
        }

        private async Task<TaskContext> GetNextTaskForRun()
        {
            var count = 0;
            while (_queueItems.TryDequeue(out var item))
            {
                if (_cancelation == null || _cancelation.IsCancellationRequested)
                    return null;

                if (item.Dependences != null)
                {
                    IEnumerable<TaskContext> deps;
                    lock (_locker)
                    {
                        deps = item.Dependences
                            .Select(dep => _contextByName.Find(dep))
                            .Where(dep => dep != null && dep.Used)
                            .ToList();
                    }
                    //нужно добавить полноценное ожидание завершающихся задач
                    if (deps.Any())
                    {
                        //если зависимая задача занята, помещаем ее в конец очереди и берем следующую
                        _queueItems.Enqueue(item);
                        //если все занято ждем
                        if (count++ > _queueItems.Count)
                        {
                            await Task.Delay(100);
                            count = 0;
                        }
                        //else
                        //{
                        //    _logger.Debug($"Задача {item.Name} ожидает зависимые задачи");
                        //}
                        continue;
                    }
                }
                //_logger.Debug($"Выдали задачу на исполнение {item.Name}");
                return item;
            }
            return null;
        }

        public void Add(ITaskingBase task, TimeSpan interval, string taskName)
        {
	        if (string.IsNullOrEmpty(taskName))
		        throw new ArgumentException(nameof(taskName));
	        AddContext(new TaskIntervalContext(task) { Name = taskName, FrequencyInterval = interval });
        }

        public void Add(ITaskingBase task, TimeSpan interval)
        {
            AddContext(new TaskIntervalContext(task) { FrequencyInterval = interval });
        }

        int _count;
        public void AddContext(TaskContext context)
        {
            if (Async && context.TaskAsync == null)
                throw new ArgumentException("Нельзя добавлять синхронный конекст в асинхронном режиме");
            if (Async == false && context.Task == null)
                throw new ArgumentException("Нельзя добавлять асинхронный конекст в синхронном режиме");
            lock (_locker)
            {
                if (string.IsNullOrEmpty(context.Name))
                    context.Name = (_contextByName.Count + 1).ToString();
                context.Order = _count++;
                _contextByName.Add(context.Name, context);
                if (_logger != null && context.Logger == null)
                {
                    context.Initialize(_logger);
                }
                context.Changed += task_Changed;
                if (context.MaxTimeoutInterval == TimeSpan.Zero)
                    context.MaxTimeoutInterval = MaxTimeoutInterval;
                var info = _store.Find(context);
                if (info != null && info.Any())
                {
                    context.Restore(info);
                }
            }
        }

        public void AddSingleRun(TaskContext context, bool disposeAfter = true)
        {
            lock (_locker)
            {
                try
                {
                    if (_cancelation == null || _cancelation.IsCancellationRequested) return; //проверяем отмену
                    if (context.Logger == null)
                        throw new ArgumentNullException("Logger", "Не инициализирован логгер у " + context.Name);
                    var task = _taskFactory.StartNew(context.Processing, _cancelation, _cancelation.Token);
                    _tasks.Add(task);
                    if (disposeAfter)
                        _taskFactory.ContinueWhenAll(new[] { task }, tasks => context.Dispose());
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Ошибка при запуске задачи " + context.Name);
                    throw;
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void AfterTaskingComplete()
        {
            try
            {
                if (_cancelation == null || _cancelation.IsCancellationRequested)
                    return;

                GC.Collect(2);
                GC.WaitForPendingFinalizers();

                CheckLostTask();
                TaskQueueCompleted?.Invoke();
                _store.Flush();
            }
            catch (OperationCanceledException)
            {
                _logger.Debug("Задача отменена");
            }
            catch (Exception ex)
            {
                _logger?.Error(ex);
            }
        }

        /// <summary>
        /// Остановка сборки с завершением потоков
        /// </summary>
        public void Stop()
        {
            if (_cancelation != null)
            {
                _cancelation.Cancel();
                _waitHandle.Set(); //отпускаем ожидание
                lock (_timerLocker)
                {
                    _timerChange?.Dispose();
                    _timerChange = null;
                }
                try
                {
                    _logger.Debug("Ожидание завершения подзадач");
                    lock (_tasks)
                    {
                        Task.WaitAll(_tasks.ToArray(), MaxTimeoutInterval);
                        _logger.Debug("Подзадачи завершены");
                    }
                    _store.Flush();
                }
                catch (OperationCanceledException)
                {
                    _logger.Warn("Часть задач отменена");
                }
                catch (AggregateException aex)
                {
                    if (aex.InnerException is OperationCanceledException)
                        _logger.Warn("Часть задач отменена");
                    else
                        _logger.Error(aex, "Не удалось дождаться корректного завершения задач");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Не удалось дождаться корректного завершения задач");
                }
            }

            try
            {
                if (_mainTask != null)
                {
                    _logger.Debug("Ожидание завершения головной задачи");
                    _mainTask.Wait(MaxTimeoutInterval);
                    _mainTask.Dispose();
                    _logger.Debug("Головная задача завершена");
                }
                _cancelation?.Dispose();
                _cancelation = null;
            }
            catch (OperationCanceledException)
            {
                _logger.Warn("Задача отменена");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Не удалось дождаться корректного завершения задачи");
            }
            _mainTask = null;
        }

        public override void Dispose()
        {
            try
            {
                if (_mainTask != null)
                    Stop();
                _waitHandle.Dispose();
            }
            finally
            {
                base.Dispose();
                lock (_locker)
                {
                    _contextByName.Values.ForEach(item => item.Dispose());
                }
            }
        }
    }
}
