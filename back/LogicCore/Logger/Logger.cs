using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicCore
{
    public interface ILogger
    {
        event Action<ILogger, LoggerEventArgs> Logging;

        string Name { get; }
        string GetFileName();

        void Debug(string message);
        void Debug(string message, params object[] args);

        void Trace(string message);
        bool IsTraceEnabled { get; }

        void Info(string message);
        void Info(string message, params object[] args);

        void Warn(string message);
        void Warn(string message, params object[] args);
        void Warn(Exception exception, string message, params object[] args);

        void Error(string message);
        void Error(Exception exception);
        void Error(string message, Exception exception);
        void Error(Exception exception, string message, params object[] args);

        void Fatal(string message);
        void Fatal(Exception exception);
        void Fatal(string message, Exception exception);
        void Fatal(Exception exception, string message, params object[] args);
    }

    public class LoggerEventArgs : EventArgs
    {
        public LoggerEventArgs(DateTime dtime, string message, Exception exception, LoggerMessageTypeEnum logType)
        {
            DateTime = dtime;
            Message = message;
            Exception = exception;
            LogType = logType;
        }
        public DateTime DateTime { get; }
        public LoggerMessageTypeEnum LogType { get; }
        public Exception Exception { get; }
        public string Message { get; }
    }


    public enum LoggerMessageTypeEnum
    {
        //Debug,
        //Trace,
        Info,
        Warning,
        Error,
        Fatal
    }

    public class ConsoleLogger : ILogger
    {
        public event Action<ILogger, LoggerEventArgs> Logging;

        public string Name { get { return "Console"; } }

        public void Debug(string message)
        {
            if (IsDebugEnabled)
              Console.WriteLine($"{DateTime.Now.TimeOfDay} DEBUG: {message}");
        }

        public bool IsTraceEnabled { get; set; } = true;
        public bool IsDebugEnabled { get; set; } = true;

        public void Trace(string message)
        {
            if (IsTraceEnabled)
                Console.WriteLine($"{DateTime.Now.TimeOfDay} TRACE: {message}");
        }

        public void Info(string message)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} INFO: {message}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, message, null, LoggerMessageTypeEnum.Info));
        }

        public void Warn(string message)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} WARN: {message}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, message, null, LoggerMessageTypeEnum.Warning));
        }

        public void Error(string message)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} ERROR: {message}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, message, null, LoggerMessageTypeEnum.Error));
        }

        public void Error(string message, Exception exception)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} ERROR: {message}, {exception.Message}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, message, exception, LoggerMessageTypeEnum.Error));
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} ERROR: {string.Format(message, args)}, {exception.Message}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, message, exception, LoggerMessageTypeEnum.Error));
        }

        public void Fatal(string message)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} FATAL: {message}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, message, null, LoggerMessageTypeEnum.Fatal));
        }

        public void Fatal(string message, Exception exception)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} FATAL: {message}, {exception.Message}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, message, exception, LoggerMessageTypeEnum.Fatal));
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} FATAL: {string.Format(message, args)}, {exception.Message}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, message, exception, LoggerMessageTypeEnum.Fatal));
        }

        public void Debug(string message, params object[] args)
        {
            if (IsDebugEnabled)
                Console.WriteLine($"{DateTime.Now.TimeOfDay} DEBUG: {string.Format(message, args)}");
        }

        public void Info(string message, params object[] args)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} INFO: {string.Format(message, args)}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, message, null, LoggerMessageTypeEnum.Info));
        }

        public void Warn(string message, params object[] args)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} WARN: {string.Format(message, args)}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, message, null, LoggerMessageTypeEnum.Warning));
        }

        public void Warn(Exception exception, string message, params object[] args)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} WARN: {string.Format(message, args)}, {exception.Message}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, message, exception, LoggerMessageTypeEnum.Warning));
        }

        public void Error(Exception exception)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} ERROR: {exception.Message}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, null, exception, LoggerMessageTypeEnum.Error));
        }

        public void Fatal(Exception exception)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} FATAL: {exception.Message}");
            Logging?.Invoke(this, new LoggerEventArgs(DateTime.Now, null, exception, LoggerMessageTypeEnum.Fatal));
        }

        public string GetFileName() => null;
    }
}
