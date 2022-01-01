using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace LogicCore.Extensions
{
	public static class ExceptionExtensions
	{
		public static string AllMessages(this System.Exception ex)
		{
			if (ex == null)
			{
				throw new NullReferenceException();
			}
			if (ex.InnerException == null)
			{
				return ex.Message;
			}

			var msg = new StringBuilder();
			while (ex != null)
			{
				msg.Append(ex.Message);
				msg.Append(Environment.NewLine);
				ex = ex.InnerException;
			}
			msg.Length = msg.Length - Environment.NewLine.Length;
			return msg.ToString();
		}

        public static bool IsCritical(this Exception ex)
        {
            if (ex is OutOfMemoryException) return true;
            if (ex is AppDomainUnloadedException) return true;
            if (ex is BadImageFormatException) return true;
            if (ex is CannotUnloadAppDomainException) return true;
            if (ex is AccessViolationException) return true;
            if (ex is InsufficientMemoryException) return true;
            //if (ex is ExecutionEngineException) return true;
            if (ex is InvalidProgramException) return true;
            if (ex is ThreadAbortException) return true;
            //if (ex is RuntimeWrapperException)
            if (ex is System.Runtime.InteropServices.SEHException sehex) return true;
            //{
            //    if (sehex.ErrorCode == EXCEPTION_ILLEGAL_INSTRUCTION)
            //    return true;
            //}
            if (ex is System.ComponentModel.Win32Exception) return true;
            //{

            //}
            if (ex is ThreadAbortException) return true;
/*
EXCEPTION_ACCESS_VIOLATION – System.AccessViolationException
EXCEPTION_STACK_OVERFLOW – System.StackOverflowException
EXCEPTION_ILLEGAL_INSTRUCTION – SEHException
EXCEPTION_IN_PAGE_ERROR – SEHException
EXCEPTION_INVALID_DISPOSITION – SEHException
EXCEPTION_NONCONTINUABLE_EXCEPTION – SEHException
EXCEPTION_PRIV_INSTRUCTION – SEHException
STATUS_UNWIND_CONSOLIDATE – SEHException
*/
            return false;
        }
	}
}
