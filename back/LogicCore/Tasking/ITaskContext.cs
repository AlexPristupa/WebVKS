using System;
using System.Linq;
using System.Threading;

namespace LogicCore.Tasking
{
    public interface ITaskContext
    {
        string Name { get; }

        ILogger Logger { get; }

        DateTime? CurrentRun { get; }

        DateTime? LastRun { get; }

        DateTime NextTimeProcessing { get; set; }

        DateTime GetNextTimeProcessing(DateTime now);

        bool Canceled { get; }

        CancellationTokenSource Cancellation { get; }

        ScheduleInfo[] GetInfo();

        void OnChanged();
    }

    public static class TaskContextExtensions
    {
        public static ScheduleInfo GetInfo(this ITaskContext task, string key)
        {
            var info = task.GetInfo().FirstOrDefault(inf => inf.Key == key);
            return info;
        }
    }
}