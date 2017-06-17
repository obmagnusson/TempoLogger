using System;
using TempoLogger.Helpers;

namespace TempoLogger.Models
{
	public class WorkLog
	{
		public string Issue { get; set; }
		public string Start { get; set; }
		public string End { get; set; }
		public DateTime Date { get; set; }
		public string Account { get; set; }
		public string Comment { get; set; }

		public int DurationSeconds => WorkLogHelper.CalculateDurationSeconds(Start, End);
		public string DurationString => WorkLogHelper.SecondsToString(DurationSeconds);
	}
}
