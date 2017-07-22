using System.Windows;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TempoLogger.Helpers;
using TempoLogger.Models;


namespace TempoLogger
{
	/// <summary>
	/// Interaction logic for TrackerWindow.xaml
	/// </summary>
	public partial class TrackerWindow
	{
		public LogForm Form { get; set; }
		public int SecondsTracked { get; set; }
		public DispatcherTimer DispatcherTimer { get; set; }

		public TrackerWindow(LogForm issue)
		{
			InitializeComponent();
			SecondsTracked = 56;
			Form = issue;
			Init();
		}

		private void Init()
		{
			Title = string.IsNullOrEmpty(Form.Model.Issue) ? "New Tracker" : "Issue No : " + Form.Model.Issue;
			this.WindowStyle = WindowStyle.None;
			Timer1.Content = "00:00:00";
			DispatcherTimer = new DispatcherTimer();
			DispatcherTimer.Tick += dispatcherTimer_Tick;
			DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var mainWindow = Application.Current.MainWindow;
			Left = mainWindow.Left + (mainWindow.Width - ActualWidth) / 2;
			Top = mainWindow.Top + (mainWindow.Height - ActualHeight) / 2;
		}
		
		//  System.Windows.Threading.DispatcherTimer.Tick handler 
		// 
		//  Updates the current seconds display and calls 
		//  InvalidateRequerySuggested on the CommandManager to force  
		//  the Command to raise the CanExecuteChanged event. 
		private void dispatcherTimer_Tick(object sender, EventArgs e)	
		{
			// Updating the Label which displays the current second
			
			Timer1.Content = WorkLogHelper.SecondsToDateString(++SecondsTracked);

			// Forcing the CommandManager to raise the RequerySuggested event
			CommandManager.InvalidateRequerySuggested();
		}
		/// <summary>
		/// Event handler for onclick event on start button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnStartTracker_Click(object sender, RoutedEventArgs e)
		{
			DispatcherTimer.Start();
			//MainGrid.Background = WorkLogHelper.GetColorFromHexa("#2d2d30");
			//Timer1.BorderBrush = WorkLogHelper.GetColorFromHexa("#FFFFFF");
		}

		/// <summary>
		/// Event handler for onclick event on pause button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnPauseTracker_Click(object sender, RoutedEventArgs e)
		{
			DispatcherTimer.Stop();
			//MainGrid.Background = WorkLogHelper.GetColorFromHexa("#FFFFFF");
		}

		/// <summary>
		/// Sends the timer info into a new log window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnLog_Click(object sender, RoutedEventArgs e)
		{
			Form.Model.SecondsLogged = SecondsTracked;

			var success = Form.ShowDialog() ?? false;

			if (!success) return;
			this.Close();
			/*_repo.Add(logform.Model);
			LoadSelectedDay();

			SaveHelper();*/
			//MainGrid.Background = WorkLogHelper.GetColorFromHexa("#FFFFFF");
		}
	}
}
