using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TempoLogger.Helpers;
using TempoLogger.Models;

namespace TempoLogger
{
	/// <summary>
	/// Interaction logic for LogForm.xaml
	/// </summary>
	public partial class LogForm
	{
		public WorkLog Model { get; set; }

		public LogForm(WorkLog edit)
		{
			InitializeComponent();

			Model = edit;
			Init();
		}

		public LogForm(DateTime date)
		{
			InitializeComponent();

			Model = new WorkLog { Date = date };
			Init();
		}

		private void Init()
		{
			TxtIssue.Text = Model.Issue;
			DpDate.SelectedDate = Model.Date;
			TxtStart.Text = Model.Start;
			TxtEnd.Text = Model.End;
			TxtDuration.Text = WorkLogHelper.SecondsToString(Model.DurationSeconds);
			TxtComment.Text = Model.Comment;
			SetTxtDuration();
			TxtIssue.Focus();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var mainWindow = Application.Current.MainWindow;
			Left = mainWindow.Left + (mainWindow.Width - ActualWidth) / 2;
			Top = mainWindow.Top + (mainWindow.Height - ActualHeight) / 2;
		}

		/// <summary>
		/// Saves worklog
		/// </summary>
		private void Submit()
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

		/// <summary>
		/// Event handler for keydown events on text inputs, if enter is pressed the worklog is submitted
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Txt_HandleKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return) Submit();
		}

		/// <summary>
		/// Event handler for keydown events on comment input, if ctrl+enter is pressed the worklog is submitted
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtComment_HandleKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && Keyboard.Modifiers == ModifierKeys.Control) Submit();
		}

		/// <summary>
		/// Event handler for onclick event on save button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			Submit();
		}

		private bool Validate()
		{
			const string issueRegex = @"^[[A-Za-z0-9]+\-[0-9]+$";
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

			if (!Regex.IsMatch(TxtStart.Text, timeRegex))
			{
				if (Model.SecondsLogged > 0) return true;
				LblError.Content = "Start is not in correct format";
				return false;
			}

			if (!Regex.IsMatch(TxtEnd.Text, timeRegex))
			{
				if (Model.SecondsLogged > 0) return true;
				LblError.Content = "End is not in correct format";
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

			if (Model.SecondsLogged > 0)
			{
				TxtDuration.Text = Model.DurationString;
			}
		}
	}
}
