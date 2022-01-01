using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;
using System.Runtime.CompilerServices;
using LogicCore.Common;
using LogicCore.Extensions;

namespace LogicCore.Tasking
{
    public abstract class TaskContext : Disposable, ITaskContext
    {
        protected ILogger _logger; //логгер
        private DateTime? _lastRun;
        private DateTime? _currentRun;
        private DateTime _nextTimeProcessing = DateTime.MinValue; //время последней обработки
        private int _executionsCount;
        private DateTime? _outOfTurnTime = null; //время внепланового запуска
        protected volatile TaskContextStatus _status;
        protected object _locker = new object();
        private readonly SemaphoreSlim _syncRunner = new SemaphoreSlim(1);
        private readonly ITaskingBase _task;

        public event Action<TaskContext> Changed;

        protected TaskContext(ITaskingBase task)
        {
            _task = task;
            Name = GetType().Name;
        }

        protected TaskContext()
        {
            _task = (ITaskingBase)this;
            Name = GetType().Name;
        }

        public void OnChanged()
        {
            OnChanged(true);
        }

        private void OnChanged(bool clearNextTime)
        {
            if (clearNextTime)
                _nextTimeProcessing = DateTime.Now;
            Changed?.Invoke(this);
        }

        public ITasking Task => _task as ITasking;
        public ITaskingAsync TaskAsync => _task as ITaskingAsync;

        public DateTime? CurrentRun => _currentRun;

        private CancellationTokenSource _cancelation;

        public CancellationToken Cancellation { get; private set; }

        /// <summary>
        /// Количество попыток исполнения задачи
        /// </summary>
        public int TryExecuteCount { get; set; } = 1;

        public int ExecutionsCount => _executionsCount;

        public int Order { get; internal set; }

        /// <summary>
        /// Время следующего запуска
        /// </summary>
        public virtual DateTime NextTimeProcessing
        {
            get { return DateTimeExtensions.Min(_outOfTurnTime ?? _nextTimeProcessing, _nextTimeProcessing); }
            set 
            {
                if (value != _nextTimeProcessing)
                {
                    Logger.Trace($"TaskManager: Изменено время следующего запуска: с {_nextTimeProcessing:dd.MM.yy HH:mm} на {value:dd.MM.yy HH:mm}");
                    _nextTimeProcessing = value;
                    OnChanged(false);
                }
            }
        }

        /// <summary>
        /// Запуск вне расписания
        /// </summary>
        public DateTime? OutOfTurnTime
        {
            get => _outOfTurnTime;
            set { _outOfTurnTime = value; OnChanged(false); }
        }

        public DateTime? LastRun => _lastRun;

        public string Name { get; set; }

        public string[] Dependences { get; set; }

        public TimeSpan MaxTimeoutInterval { get; set; } = TimeSpan.Zero;

        public bool InProcessing
        {
            get { lock(_locker) return _status == TaskContextStatus.InProcessing; }
        }

        public bool Used
        {
            get { lock (_locker) return _status == TaskContextStatus.InProcessing || _status == TaskContextStatus.InProcessingQueue; }
        }

        public TaskContextStatus Status
        {
            get { lock (_locker) return _status; }
        }

        public bool ChangeStatusToEnque()
        {
            lock (_locker)
            {
                if (_status == TaskContextStatus.InProcessingQueue)
                {
                    _logger.Debug($"Задача {Name} уже в очереди");
                    return false;
                }
                _status = TaskContextStatus.InProcessingQueue;
            }
            _logger.Debug($"Задача {Name} поставлена в очередь на выполнение{Environment.NewLine}");
            return true;
        }
        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="logger">Логирование конкретного источника</param>
        public virtual void Initialize(ILogger logger)
        {
            //logger.WriteSartLine("");
            _logger = logger;
            if (_task is ITaskingInitialize tctx)
            {
                tctx.Initialize(this);
            }
        }

        public ILogger Logger { get { return _logger; } }

        bool ITaskContext.Canceled => Cancellation != null && Cancellation.IsCancellationRequested;
        CancellationTokenSource ITaskContext.Cancellation => _cancelation;
        DateTime ITaskContext.GetNextTimeProcessing(DateTime now) => GetNextTimeProcessing(now);

        /// <summary>
        /// запуск обработки 
        /// </summary
        [MethodImpl(MethodImplOptions.Synchronized)]
        //перехват системных исключений
        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        //[System.Security.SecurityCritical]
        internal void Processing(object state)
        {
            //_syncRunner.Wait();
            try
            {
                lock (_locker) _status =  TaskContextStatus.InProcessing;
                _cancelation = state as CancellationTokenSource;
                Cancellation = _cancelation.Token;
                var sw = Stopwatch.StartNew();
#if !DEBUG
                _logger.LogApplicationInfo();
#endif
                _logger.Info("Начало выполнения " + Name);
                _logger.Debug("Задействовано памяти перед началом: {0:0.0} мб", GC.GetTotalMemory(false) / (1024d * 1024d));
                //Task.Execute(new TaskRunningContext(this, cancelation));
                TryExecute();
                _logger.Info($"Выполнение завершено {Name} за {sw.Elapsed.TotalSeconds:0.00} сек");
                _logger.Debug("Задействовано памяти после запуска: {0:0.0} мб", GC.GetTotalMemory(false) / (1024d * 1024d));
            }
            catch (OperationCanceledException ex)
            {
                _logger.Warn(ex.AllMessages() ?? "Задача отменена");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при выполнении задачи");
                if (ex.IsCritical()) //идет перехват фатальных для приложения ислючений, глушить нельзя
                    throw;
            }
            finally
            {
                lock (_locker) _status = TaskContextStatus.Waiting;
                _lastRun = _currentRun;
                IncrementExecutionCount();
                //_syncRunner.Release();
            }
        }

        protected virtual void IncrementExecutionCount()
        {
            _executionsCount++;
        }

        private void TryExecute()
        {
            var currentCount = 0;
            PrepareExecute();
            while (currentCount++ < TryExecuteCount)
            {
                Cancellation.ThrowIfCancellationRequested();
                if (currentCount > 1)
                {
                    Logger.Info("Повторная попытка выполнения " + Name);
                }
                try
                {
                    Task.Execute(this);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch(Exception ex)
                {
                    if (currentCount >= TryExecuteCount) throw;
                    Logger.Error("Ошибка при выполнении задачи", ex);
                }
                finally
                {
                    CompleteExecute();
                }
            }
        }

        protected virtual void CompleteExecute() { }
        protected virtual void PrepareExecute() { }

        /// <summary>
        /// запуск обработки 
        /// </summary
        //[MethodImpl(MethodImplOptions.Synchronized)]
        //перехват системных исключений
        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        //[System.Security.SecurityCritical]
        internal async Task ProcessingAsync(object state)
        {
            await _syncRunner.WaitAsync();
            try
            {
                lock (_locker) _status = TaskContextStatus.InProcessing;
                var cancelation = state as CancellationTokenSource;
                Cancellation = cancelation.Token;
                var sw = Stopwatch.StartNew();
#if !DEBUG
                _logger.LogApplicationInfo();
#endif
                _logger.Debug("Начало выполнения " + Name);
                _logger.Debug("Задействовано памяти перед началом: {0:0.0} мб", GC.GetTotalMemory(false) / (1024d * 1024d));
                await TryExecuteAsync();
                _logger.Debug($"Выполнение завершено {Name} за {sw.Elapsed.TotalSeconds:0.00} сек");
                _logger.Debug("Задействовано памяти после запуска: {0:0.0} мб", GC.GetTotalMemory(false) / (1024d * 1024d));
            }
            catch (OperationCanceledException ex)
            {
                _logger.Warn(ex.AllMessages() ?? "Задача отменена");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при выполнении задачи");
                if (ex.IsCritical()) //идет перехват фатальных для приложения ислючений, глушить нельзя
                    throw;
            }
            finally
            {
                lock (_locker) _status = TaskContextStatus.Waiting;
                IncrementExecutionCount();
                _lastRun = _currentRun;
                _syncRunner.Release();
            }
        }

        private async Task TryExecuteAsync()
        {
            var currentCount = 0;
            while (currentCount++ < TryExecuteCount)
            {
                Cancellation.ThrowIfCancellationRequested();
                if (currentCount > 1)
                {
                    Logger.Info("Повторная попытка выполнения " + Name);
                }
                try
                {
                    await TaskAsync.ExecuteAsync(this);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    if (currentCount >= TryExecuteCount) throw;
                    Logger.Error("Ошибка при выполнении задачи", ex);
                }
            }
        }


        //Подготовка к новому запуску
        public bool PrepareForProcessing(DateTime now)
        {
            if (Monitor.TryEnter(_locker, MaxTimeoutInterval) == false)
            {
                throw new TimeoutException("Превышен интервал ожидания блокировки");
            }
            try
            {
                var ready = false;
                //пока в обработке не принимаем
                if (Used)
                {
                    if (_status == TaskContextStatus.InProcessing)
                        _logger.Info($"Задача {Name} уже выполняется, пропускаем");
                    else if (_status == TaskContextStatus.InProcessingQueue)
                        _logger.Info($"Задача {Name} уже поставлена в очередь на выполнение, пропускаем");
                }
                else
                {
                    try
                    {
                        //сначала определяем годность
                        ready = _outOfTurnTime <= now ||
                                ReadyForProcessing(now);
                    }
                    catch(Exception ex)
                    {
                        Logger.Error(ex, "Ошибка при определении готовности задачи " + Name);
                        ready = false;
                    }
                    if (_outOfTurnTime <= now && _outOfTurnTime != _nextTimeProcessing)
                    {
                        _currentRun = now;// _outOfTurnTime; //_outOfTurnTime может сильно отличаться от now
                        _outOfTurnTime = null; //сбрасываем принудительное время
                        //выходим досрочно при внеплановом исполнении
                        return true;
                    }
                }
                //затем определяем следующий запуск
                if (ready)
                {
                    _currentRun = (_nextTimeProcessing != DateTime.MinValue) ? _nextTimeProcessing : now;
                    _outOfTurnTime = null;
                }
                try
                {
                    _nextTimeProcessing = GetNextTimeProcessing(now);
                }
                catch(Exception ex)
                {
                    _nextTimeProcessing = now.AddMinutes(5); //повтор через 5 мин при ошибке
                    _outOfTurnTime = _nextTimeProcessing; //для прохождения контроля
                    Logger.Error(ex, "Ошибка при определении нового времени запуска задачи " + Name);
                    return false;
                }
                return ready;
            }
            finally
            {
                Monitor.Exit(_locker);
            }
        }

        protected virtual bool ReadyForProcessing(DateTime now)
        {
            return now >= NextTimeProcessing;
        }

        protected abstract DateTime GetNextTimeProcessing(DateTime now);

        public virtual void Restore(IEnumerable<ScheduleInfo> infos)
        {
            var info = infos.OrderBy(i => i.NextRun).First();
            _lastRun = info.LastRun;
            _nextTimeProcessing = info.NextRun ?? _nextTimeProcessing;
            _executionsCount = info.ExecutionsCount;
        }

        public virtual ScheduleInfo[] GetInfo()
        {
            var info = new ScheduleInfo
            {
                Name = Name,
                NextRun = NextTimeProcessing,
                LastRun = _lastRun,
                ExecutionsCount = _executionsCount
            };
            return new[] { info };
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum TaskContextStatus
    {
        Waiting,
        InProcessingQueue,
        InProcessing,
    }

}