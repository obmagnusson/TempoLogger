﻿using System;
using System.Globalization;

namespace TempoLogger.Helpers
{
	public static class WorkLogHelper
	{
		//public static int GetDurationSeconds(this WorkLog log)
		//{
		//	var durStr = log.DurationSeconds;
		//	if (string.IsNullOrEmpty(durStr) || durStr == "0") return 0;


		//	var regex = new Regex(@"^(([0-9]+)h)?\s*(([0-9]+)m)?$");
		//	var match = regex.Match(durStr);
		//	if (match.Success)
		//	{
		//		var hours = 0;
		//		var minutes = 0;
		//		var seconds = 0;

		//		if (!string.IsNullOrEmpty(match.Groups[2]?.Value)) hours = int.Parse(match.Groups[2]?.Value);
		//		if (!string.IsNullOrEmpty(match.Groups[4]?.Value)) minutes = int.Parse(match.Groups[4]?.Value);

		//		seconds = (hours * 3600) + (minutes * 60);
		//		return seconds;
		//	}
		//	return 0;
		//}

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
