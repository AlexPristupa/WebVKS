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

namespace LogicCore.Tasking
{
    public class TaskIntervalContext : TaskContext
    {
        public TaskIntervalContext(ITaskingBase task) : base(task)
        {
        }

        public TaskIntervalContext()
        {
        }

        /// <summary>
        /// Частота опроса данных
        /// </summary>
        public TimeSpan FrequencyInterval { get; set; }

        public override void Dispose()
        {
            base.Dispose();
        }

        //Подготовка к новому запуску
        protected override DateTime GetNextTimeProcessing(DateTime now)
        {
            var nextTime = NextTimeProcessing;
            if (now >= nextTime)
            {
                if (FrequencyInterval == TimeSpan.MaxValue)
                    return DateTime.MaxValue;

                var time = nextTime + FrequencyInterval;
                if (time > now)
                    return time;
                return now + FrequencyInterval;
            }
            return nextTime;
        }
    }
}