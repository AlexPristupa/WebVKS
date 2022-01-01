using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace LogicCore.Extensions
{
    /// <summary>
    /// Класс расширение для отмены асинхронных задач
    /// </summary>
    public static class TaskExtension
    {
        public static T TryRepeat<T>(Func<T> action, TimeoutInfo timeout)
        {
            timeout.CurrentCount = 0;
            string lastMessage = null;
            while (timeout.CurrentCount++ < timeout.RequestCount)
            {
                timeout?.Cancellation.ThrowIfCancellationRequested();
                try
                {
                    return action();
                    //var result = await Task.WhenAny(task, Task.Delay(timeout.Timeout));
                    //if (result == task) return await task;
                }
                catch (OperationCanceledException)
                {
                    //lastMessage = null;
                }
                catch (Exception ex)
                {
                    lastMessage = ex.Message;
                }
            }
            timeout?.Cancellation.ThrowIfCancellationRequested();
            throw new TimeoutException(lastMessage ??
                $"Превышено время ожидания {timeout.Timeout}, попыток {timeout.RequestCount}");
        }

        public static async Task<T> TryRepeatAsync<T>(Func<Task<T>> func, TimeoutInfo timeout)
        {
            timeout.CurrentCount = 0;
            string lastMessage = null;
            while (timeout.CurrentCount++ < timeout.RequestCount)
            {
                timeout?.Cancellation.ThrowIfCancellationRequested();
                try
                {
                    await func().ConfigureAwait(false);
                    //var result = await Task.WhenAny(task, Task.Delay(timeout.Timeout));
                    //if (result == task) return await task;
                }
                catch (OperationCanceledException)
                {
                    //lastMessage ;
                }
                catch (Exception ex)
                {
                    lastMessage = ex.Message;
                }
            }
            timeout?.Cancellation.ThrowIfCancellationRequested();
            throw new TimeoutException(lastMessage ??
                $"Превышено время ожидания {timeout.Timeout}, попыток {timeout.RequestCount}");
        }

        /// <summary>
        /// Криво отменяет задачу, не высвобождает ресурсы исходного task
        /// </summary>
        /// <returns></returns>
        //public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        //{
        //    var tcs = new TaskCompletionSource<bool>();
        //    using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
        //    {
        //        if (task != await Task.WhenAny(task, tcs.Task))
        //        {
        //            throw new OperationCanceledException(cancellationToken);
        //        }
        //    }
        //    return task.Result;
        //}

        //public static async Task WithCancellation(this Task task, CancellationToken cancellationToken)
        //{
        //    var tcs = new TaskCompletionSource<bool>();

        //    using (cancellationToken.Register(
        //        s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
        //    {
        //        if (task != await Task.WhenAny(task, tcs.Task))
        //        {
        //            throw new OperationCanceledException(cancellationToken);
        //        }
        //    }
        //    await task;
        //}

        public static async Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout, Action closeTask)
        {
            try
            {
                using (var cancellation = new CancellationTokenSource(timeout))
                {
                    using (cancellation.Token.Register(closeTask))
                    {
                        return await task.ConfigureAwait(false);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException($"Превышено время ожидания {timeout}");
            }
        }

        //public static async Task WithTimeout(this Task task, TimeSpan timeout)
        //{
        //    try
        //    {
        //        using (var cancellation = new CancellationTokenSource(timeout))
        //        {
        //            //await Task.Run(async () => await task, cancellation.Token);
        //            await task.WithCancellation(cancellation.Token);
        //            return;
        //        }
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        throw new TimeoutException($"Превышено время ожидания {timeout}");
        //    }
        //}

        //public static async Task<T> WithTimeout<T>(this Task<T> task, TimeoutInfo timeout)
        //{
        //    timeout.CurrentCount = 0;
        //    string lastMessage = null;
        //    while (timeout.CurrentCount++ < timeout.RequestCount)
        //    {
        //        timeout?.Cancellation.ThrowIfCancellationRequested();
        //        try
        //        {
        //            //using (var cancellation = new CancellationTokenSource(timeout.Timeout))
        //            {

        //                var result = await Task.WhenAny(task, Task.Delay(timeout.Timeout));
        //                if (result == task) return await task;
        //                //return await task
        //                //    .WithCancellation(cancellation.Token);
        //            }
        //        }
        //        catch (OperationCanceledException)
        //        {
        //            lastMessage = null;
        //        }
        //        catch (SocketException ex)
        //        {
        //            lastMessage = ex.Message;
        //        }
        //    }
        //    timeout?.Cancellation.ThrowIfCancellationRequested();
        //    throw new TimeoutException(lastMessage ??
        //        $"Превышено время ожидания {timeout.Timeout}, попыток {timeout.RequestCount}");
        //}


        //public static async Task WithTimeout(this Task task, TimeoutInfo timeout)
        //{
        //    timeout.CurrentCount = 0;
        //    string lastMessage = null;
        //    while (timeout.CurrentCount++ < timeout.RequestCount)
        //    {
        //        timeout?.Cancellation.ThrowIfCancellationRequested();
        //        try
        //        {
        //            using (var cancellation = new CancellationTokenSource(timeout.Timeout))
        //            {
        //                //await Task.Run(async () => await task, cancellation.Token);
        //                await task.WithCancellation(cancellation.Token);
        //                return;
        //            }
        //        }
        //        catch (OperationCanceledException)
        //        {
        //            lastMessage = null;
        //        }
        //        catch (SocketException ex)
        //        {
        //            lastMessage = ex.Message;
        //        }
        //    }
        //    timeout?.Cancellation.ThrowIfCancellationRequested();
        //    throw new TimeoutException(lastMessage ??
        //        $"Превышено время ожидания {timeout.Timeout}, попыток {timeout.RequestCount}");
        //}
    }
    public class TimeoutInfo
    {
        public TimeSpan Timeout;
        public int RequestCount = 1;
        public CancellationToken Cancellation;
        /// <summary>
        /// Сколько попыток было сделано
        /// </summary>
        public int CurrentCount;
    }
}
