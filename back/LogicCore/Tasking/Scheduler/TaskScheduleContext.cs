using LogicCore.Extensions;
using LogicCore.Tasking.Scheduler;
using LogicCore.Tasking.Scheduler.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogicCore.Tasking
{
    public class TaskScheduleContext : TaskContext
    {
        public TaskScheduleContext(ITaskingBase task) : base(task)
        {
        }

        protected TaskScheduleContext()
        {
        }

        public ScheduleConditions Schedule { get; set; }

        //[Obsolete("Using Schedule.TimeStart")]
        public TimeSpan? TimeStart { get => Schedule.TimeStart; set => Schedule.TimeStart = value; }

        /// <summary>
        /// Максимальное отклонение текущего времени от указанного времени запуска
        /// </summary>
        //[Obsolete("Using Schedule.TimeStartDeviation")]
        public TimeSpan TimeStartDeviation { get => Schedule.TimeStartDeviation; set => Schedule.TimeStartDeviation = value; } 

        public void SetConditions(string conditions)
        {
            Schedule = SchedulerFormatter.Default.Parse(conditions);
        }

        //Подготовка к новому запуску
        protected override DateTime GetNextTimeProcessing(DateTime now)
        {
            var nextTime = Schedule.GetNextTime(now, LastRun, ExecutionsCount);
            return nextTime;
        }

        protected override bool ReadyForProcessing(DateTime now)
        {
            if (now >= NextTimeProcessing)
            {
                return Schedule.Check(now, LastRun, ExecutionsCount);
            }
            return false;
        }

        public override void Restore(IEnumerable<ScheduleInfo> infos)
        {
            var info = infos.OrderBy(i => i.NextRun).First();
            //сразу смещаем время запуска, для устранения старта вне расписания
            if (info.LastRun != null && info.NextRun == null)
            {
                info.NextRun = GetNextTimeProcessing(info.LastRun.Value);
            }
            base.Restore(infos);
        }
    }
}
