using System;

namespace TempoLogger.Exceptions
{
	public class IssueNotFoundException : Exception
	{
		public IssueNotFoundException() { }
		public IssueNotFoundException(string message) : base(message) { }
	}
}
