using System;
using System.Globalization;

namespace TempoLogger.Helpers
{
	public static class WorkLogHelper
	{
		/// <summary>
		/// Formats seconds to 1h 30m format
		/// </summary>
		/// <param name="seconds"></param>
		/// <returns></returns>
		public static string SecondsToString(int seconds)
		{
			var ret = "";

			if (seconds <= 0)
				return "0m";

			var hour = seconds / 3600;
			if (hour > 0)
				ret += hour + "h";

			var minutes = (seconds % 3600) / 60;
			if (hour > 0 && minutes > 0)
				ret += " ";

			if (minutes > 0)
				ret += minutes + "m";

			return ret;
		}

		/// <summary>
		/// Takes start and end times (HH:mm) and returns the difference in seconds
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public static int CalculateDurationSeconds(string start, string end)
		{
			if (!DateTime.TryParseExact(start, "HH:mm", null, DateTimeStyles.None, out DateTime startDate)) return 0;
			if (!DateTime.TryParseExact(end, "HH:mm", null, DateTimeStyles.None, out DateTime endDate)) return 0;

			var timeSpan = endDate - startDate;
			return (int) timeSpan.TotalSeconds;
		}
	}
}
