using System;

namespace LogicCore.Common
{
	public class MessageInfo
	{
		public MessageInfo(string msg)
		{
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
		}

		public MessageInfo(string source, string msg)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			Source = source;
			Message = msg;
		}

		public string Source { get; private set; }

		public string Message { get; private set; }
	}
}
