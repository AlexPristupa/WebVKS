using LogicCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogicCore.Tasking
{
    public class TaskIntervalTimeContext : TaskContext
    {
        public TaskIntervalTimeContext(ITaskingBase task) : base(task)
        {
        }

        protected TaskIntervalTimeContext()
        {
        }

        /// <summary>
        /// Частота опроса данных
        /// </summary>
        //public IScheduleCondition Schedule { get; set; }
        public TimeSpan FrequencyInterval { get; set; }

        public TimeSpan? TimeStart { get; set; }

        public override void Dispose()
        {
            base.Dispose();
        }

        public bool ImmediatelyStart { get; set; } = true;

        protected override bool ReadyForProcessing(DateTime now)
        {
            if (TimeStart != null &&
                NextTimeProcessing == DateTime.MinValue &&
                ImmediatelyStart == false)
            {
                //если время запуска еще не настало (StartTime), то запускаем в ближайший интервал, если настало то в следующий
                var nextTime = GetNextTimeProcessing(now);
                return (now == nextTime);
            }
            return base.ReadyForProcessing(now);
        }

        //Подготовка к новому запуску
        protected override DateTime GetNextTimeProcessing(DateTime now)
        {
            var nextTime = NextTimeProcessing;
            if (now >= nextTime)
            {
                if (TimeStart == null) //здесь обычная периодичность
                {
                    var ntime = nextTime + FrequencyInterval;
                    if (ntime > now)
                        return ntime;
                    return now + FrequencyInterval;
                }
                var time = now.Date + TimeStart.Value; //Schedule.GetNextTime(now);
                if (time > now &&
                    time > nextTime)
                    return time;

                var diffSec = (int)(now - time).TotalSeconds / (int)FrequencyInterval.TotalSeconds + 1;
                time += TimeSpan.FromSeconds(diffSec * FrequencyInterval.TotalSeconds);
                return time;
            }
            return nextTime;
        }
    }
}
