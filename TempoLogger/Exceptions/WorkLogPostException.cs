using System;

namespace TempoLogger.Exceptions
{
	public class WorkLogPostException : Exception
	{
		public WorkLogPostException(string message) : base(message) { }
	}
}
