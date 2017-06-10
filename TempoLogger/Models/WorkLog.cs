using System;

namespace TempoLogger.Models
{
	public class WorkLog
	{
		public string Issue { get; set; }
		public string Title { get; set; }
		public string Start { get; set; }
		public string End { get; set; }
		public string Duration { get; set; }
		public DateTime Date { get; set; }
		public string Account { get; set; }
		public string Comment { get; set; }
	}
}
