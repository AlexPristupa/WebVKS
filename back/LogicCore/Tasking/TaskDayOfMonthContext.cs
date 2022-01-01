using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogicCore.Tasking
{
    public class TaskDayOfMonthContext : TaskContext
    {
        public TaskDayOfMonthContext(ITaskingBase task) : base(task)
        {
        }

        protected TaskDayOfMonthContext()
        {
        }

        /// <summary>
        /// Частота опроса данных
        /// </summary>
        //public IScheduleCondition Schedule { get; set; }
        //public TimeSpan FrequencyInterval { get; }  = TimeSpan.fro

        public TimeSpan TimeStart { get; set; }

        public int DayOfMonth { get; set; }

        public override void Dispose()
        {
            base.Dispose();
        }

        //Подготовка к новому запуску
        protected override DateTime GetNextTimeProcessing(DateTime now)
        {
            var nextTime = NextTimeProcessing;
            if (LastRun == null && nextTime == DateTime.MinValue)
            {
                nextTime = new DateTime(now.Year, now.Month, DayOfMonth).Add(TimeStart);
            }
            if (nextTime <= now)
            {
                nextTime = new DateTime(now.Year, now.Month, DayOfMonth).AddMonths(1).Add(TimeStart);
            }
            return nextTime;
        }

        protected override bool ReadyForProcessing(DateTime now)
        {
            return (now >= NextTimeProcessing && now.Day == DayOfMonth);
        }
    }
}
