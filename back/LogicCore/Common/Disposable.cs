//#define NO_TRACE_STACK //ускорение для тестов
using LogicCore.Extensions;
using System;
using System.Diagnostics;

namespace LogicCore
{
	public abstract class Disposable : IDisposable
	{
#if !NO_TRACE_STACK && DEBUG
        private string _stack;
#endif

        protected Disposable()
		{
#if !NO_TRACE_STACK && DEBUG
			var stack = new System.Diagnostics.StackTrace();
			_stack = stack.ToString();
            if (_stack == null)
                throw new ArgumentNullException("_stack");
#endif
		}

		private bool _dispose;
        private bool _finalize;

        ~Disposable()
		{
			//если финализатор остался, значит Dispose не вызывали
			//если dispose вызывали, то финализатор будет выключен и вызова этого метода не будет
			try
			{
                _finalize = true;
                Dispose();
			}
			catch (System.Exception ex)
			{
                Trace.TraceError("Ошибка при финализации объекта " + ex.AllMessages());
			}
			finally //в финале ругаемся
			{
				var msg = "Не вызыван метод Dispose у " + GetType();
#if !NO_TRACE_STACK && DEBUG
					msg += Environment.NewLine;
					msg += _stack;
#endif
                Trace.WriteLine(msg);
#if DEBUG //для контроля тестов
				throw new System.Exception(msg);
#endif
			}
		}

		public bool Disposed { get { return _dispose; } }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public virtual void Dispose()
		{
			if (_dispose)
			{
				throw new InvalidOperationException("Объект " + GetType() + " уже закрыт");
			}
            if (_finalize == false)
			    GC.SuppressFinalize(this);
			_dispose = true;
		}
	}
}
