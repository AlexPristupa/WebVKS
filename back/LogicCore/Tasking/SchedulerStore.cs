using LogicCore.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace LogicCore.Tasking
{
    public interface ISchedulerStore
    {
        void Prepare();

        void Save(TaskContext ctx);

        ScheduleInfo[] Find(TaskContext ctx);

        void Flush();
    }

    class SchedulerStore : ISchedulerStore
    {
        readonly ConcurrentDictionary<string, ScheduleInfo[]> _data = new();
        public ScheduleInfo[] Find(TaskContext ctx)
        {
            return _data.Find(ctx.Name);
        }

        public void Prepare()
        {
        }
        public void Flush()
        {
        }

        public void Save(TaskContext ctx)
        {
            _data[ctx.Name] = ctx.GetInfo();
        }
    }

    public class ScheduleInfo
    {
        public string Name { get; set; }

        public string Key { get; set; }
        
        public DateTime? CurrentRun { get; set; }

        public DateTime? NextRun { get; set; }

        public DateTime? LastRun { get; set; }

        public int ExecutionsCount { get; set; }
    }
}
