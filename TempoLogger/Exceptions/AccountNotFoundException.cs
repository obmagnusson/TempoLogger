using System;

namespace TempoLogger.Exceptions
{
	public class AccountNotFoundException : Exception
	{
		public AccountNotFoundException() { }
		public AccountNotFoundException(string message) : base(message) { }
	}
}
