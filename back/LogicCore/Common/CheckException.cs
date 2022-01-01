using System;

namespace LogicCore
{
	public class CheckException : ApplicationException
	{
		public CheckException(string message)
			: base(message)
		{
		}

		public CheckException(string message, System.Exception inner)
			: base(message, inner)
		{
		}

        //protected CashBoxCheckException(
        //    System.Runtime.Serialization.SerializationInfo info,
        //    System.Runtime.Serialization.StreamingContext context)
        //    : base(info, context)
        //{
        //}
	}

	public class ErrorException : ApplicationException
	{
		public ErrorException(string message)
			: base(message)
		{
		}

		public ErrorException(string message, System.Exception inner)
			: base(message, inner)
		{
		}

        //protected CashBoxErrorException(
        //    System.Runtime.Serialization.SerializationInfo info,
        //    System.Runtime.Serialization.StreamingContext context)
        //    : base(info, context)
        //{
        //}
	}
}

