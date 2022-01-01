using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace LogicCore
{
    public static class LoggerExtensions
    {
        [Obsolete("Опечатка названия")]
        public static void WriteSartLine(this ILogger logger, string message)
        {
            WriteStartLine(logger, message);
        }

        public static void WriteStartLine(this ILogger logger, string message)
        {
            logger.Info(string.Empty);
            logger.Info($"------------------{message}---------------------");
        }

        public static void WriteStartLineDebug(this ILogger logger, string message)
        {
            logger.Debug(string.Empty);
            logger.Debug($"------------------{message}---------------------");
        }

        public static void LogApplicationInfo(this ILogger logger, bool printDebug = false)
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            var info = "Версия приложения: " + fvi.FileVersion;
            if (printDebug) logger.Debug(info); else logger.Info(info);

            var ver = assembly.GetName().Version;
            info = "Версия сборки: " + ver;
            if (printDebug) logger.Debug(info); else logger.Info(info);

            var culture = CultureInfo.CurrentCulture.Name;
            info = "Культура потока: " + culture;
            if (printDebug) logger.Debug(info); else logger.Info(info);
        }
    }
}
