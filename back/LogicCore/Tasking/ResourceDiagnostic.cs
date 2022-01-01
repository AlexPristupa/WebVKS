using LogicCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LogicCore.Tasking
{
    public static class ResourceDiagnostic
    {
        static DateTime _lastTime = Process.GetCurrentProcess().StartTime;
        static TimeSpan _lastCpuTime;

        public static void LogCpuMemory(ILogger logger)
        {
            try
            {
                var process = Process.GetCurrentProcess();
                logger.Info($"Выделено памяти для процесса {process.WorkingSet64 / (1024 * 1024)} Mb");

                //var before = process.VirtualMemorySize64;
                var time = DateTime.Now - _lastTime;
                _lastTime = DateTime.Now;
                var cpuTime = process.TotalProcessorTime - _lastCpuTime;
                _lastCpuTime = process.TotalProcessorTime;
                var pcount = Environment.ProcessorCount;
                var usage = cpuTime.TotalSeconds * 100 / (time.TotalSeconds * pcount);
                logger.Info($"Среднее использование процессора за {time.TotalSeconds:0.0} сек: {usage:0.0}% (процессоров: {pcount})");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public static void LogDriveInfo(ILogger logger)
        {
            try
            {
                foreach (DriveInfo d in DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed && d.IsReady))
                {
                    try
                    {
                        var space = d.AvailableFreeSpace / (1024 * 1024);
                        var percent = (float)d.TotalFreeSpace / d.TotalSize;
                        logger.Info($"Объем свободного места на {d.Name} {space:# ### ##0} Mb ({percent:0%})");
                    }
                    catch (Exception ex)
                    {
                        logger.Debug($"Диск {d.Name} {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
    }
}
