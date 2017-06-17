using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TempoLogger.Helpers;
using TempoLogger.Models;

namespace TempoLogger
{
	/// <summary>
	/// Interaction logic for LogForm.xaml
	/// </summary>
	public partial class LogForm : Window
	{
		public WorkLog Model { get; set; }
		public LogForm()
		{
			InitializeComponent();

			// Create new Log (else is edit)
			if (Model == null)
			{
				Model = new WorkLog
				{
					Date = DateTime.Now.Date,
				};
			}

			TxtIssue.Text = Model.Issue;
			DpDate.SelectedDate = Model.Date;
			TxtStart.Text = Model.Start;
			TxtEnd.Text = Model.End;
			TxtDuration.Text = WorkLogHelper.SecondsToString(Model.DurationSeconds);
			TxtComment.Text = Model.Comment;
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			if (!Validate()) return;

			Model.Issue = TxtIssue.Text;
			Model.Start = TxtStart.Text;
			Model.End = TxtEnd.Text;
			Model.Comment = TxtComment.Text;

			// ReSharper disable once PossibleInvalidOperationException
			// HasValue is checked in Validate()
			Model.Date = DpDate.SelectedDate.Value;

			DialogResult = true;
			Close();
		}

		private bool Validate()
		{
			const string issueRegex = @"^[[A-Za-z]+\-[0-9]+$";
			const string timeRegex = @"^[0-9][0-9]?:[0-9]{2}$";
			//var durationRegex = @"^(([0-9]+)h)?\s*(([0-9]+)m)?$";

			if (!Regex.IsMatch(TxtIssue.Text, issueRegex))
			{
				LblError.Content = "Issue is not in correct format";
				return false;
			}

			if (!DpDate.SelectedDate.HasValue)
			{
				LblError.Content = "You need to pick a date";
				return false;
			}

			if (!Regex.IsMatch(TxtStart.Text, timeRegex))
			{
				LblError.Content = "Start is not in correct format";
				return false;
			}

			if (!Regex.IsMatch(TxtEnd.Text, timeRegex))
			{
				LblError.Content = "End is not in correct format";
				return false;
			}

			//if (string.IsNullOrEmpty(TxtDuration.Text) ||!Regex.IsMatch(TxtDuration.Text, durationRegex))
			//{
			//	LblError.Content = "Duration is not in correct format";
			//	return false;
			//}

			if (string.IsNullOrEmpty(TxtComment.Text))
			{
				LblError.Content = "You need to write a comment";
				return false;
			}

			LblError.Content = "";
			return true;
		}

		private void TxtStart_LostFocus(object sender, RoutedEventArgs e)
		{
			TxtStart.Text = FormatTimeString(TxtStart.Text);
			SetTxtDuration();
		}

		private void TxtEnd_LostFocus(object sender, RoutedEventArgs e)
		{
			TxtEnd.Text = FormatTimeString(TxtEnd.Text);
			SetTxtDuration();
		}

		/// <summary>
		/// Formats a time string in the HH:MM format
		/// </summary>
		/// <param name="timeString"></param>
		/// <returns></returns>
		private static string FormatTimeString(string timeString)
		{
			const string regex = @"^([0-9][0-9]?):?([0-9]{2})?$";

			var match = Regex.Match(timeString, regex);

			if (!match.Success) return timeString;

			var hourStr = match.Groups[1].Value;

			var minuteStr = match.Groups.Count == 3 && !string.IsNullOrEmpty(match.Groups[2].Value)
				? match.Groups[2].Value
				: "00";

			if (int.Parse(hourStr) > 23 || int.Parse(minuteStr) > 59) return timeString;

			hourStr = hourStr.Length == 1 ? "0" + hourStr : hourStr;

			return hourStr + ":" + minuteStr;
		}

		private void SetTxtDuration()
		{
			var durationSeconds = WorkLogHelper.CalculateDurationSeconds(TxtStart.Text, TxtEnd.Text);
			TxtDuration.Text = WorkLogHelper.SecondsToString(durationSeconds);
		}
	}
}
