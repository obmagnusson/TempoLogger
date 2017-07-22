using System;
using System.Globalization;
using System.Windows.Media;

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
		/// Formats seconds to a hh:mm:ss string
		/// </summary>
		/// <param name="seconds"></param>
		/// <param name="includeSeconds"></param>
		/// <returns></returns>
		public static string SecondsToDateString(int seconds, bool includeSeconds = true)
		{
			TimeSpan time = TimeSpan.FromSeconds(seconds);

			string format = includeSeconds ? @"hh\:mm\:ss" : @"hh\:mm";

			string str = time.ToString(format);
			return str;
		}

		public static SolidColorBrush GetColorFromHexa(string hexaColor)
		{
			byte R = Convert.ToByte(hexaColor.Substring(1, 2), 16);
			byte G = Convert.ToByte(hexaColor.Substring(3, 2), 16);
			byte B = Convert.ToByte(hexaColor.Substring(5, 2), 16);
			SolidColorBrush scb = new SolidColorBrush(Color.FromArgb(0xFF, R, G, B));
			return scb;
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
