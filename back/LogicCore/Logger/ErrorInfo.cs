using System;

namespace LogicCore.Common
{
	public class ErrorInfo
	{
		public ErrorInfo(string source, string message, System.Exception exception)
		{
			Source = source;
			Message = message;
			Exception = exception;
		}

		public string Source { get; private set; }

		public string Message { get; private set; }

		public System.Exception Exception { get; private set; }
	}
}
