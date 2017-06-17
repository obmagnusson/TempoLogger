using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TempoLogger.Helpers;
using TempoLogger.Models;

namespace TempoLogger
{

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly WorkLogRepository _repo;
		private List<WorkLog> _logs;
		private DateTime _selectedDate;
		public MainWindow()
		{
			InitializeComponent();

			_selectedDate = DateTime.Now;
			_repo = new WorkLogRepository();

			LoadSelectedDay();

			LblCurrentDayTotal.Content = "0";
			CalculateDayTotal();
		}

		/// <summary>
		/// Calculates and sets the total value for the day
		/// </summary>
		private void CalculateDayTotal()
		{
			var totalSeconds = _logs.Sum(log => log.DurationSeconds);

			LblCurrentDayTotal.Content = WorkLogHelper.SecondsToString(totalSeconds);
		}

		/// <summary>
		/// Goes back a day
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnPrev_Click(object sender, RoutedEventArgs e)
		{
			_selectedDate = _selectedDate.AddDays(-1);
			LoadSelectedDay();
		}

		/// <summary>
		/// Go to todays date
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnToday_Click(object sender, RoutedEventArgs e)
		{
			_selectedDate = DateTime.Now;
			LoadSelectedDay();
		}

		/// <summary>
		/// Go forward a day
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnNext_Click(object sender, RoutedEventArgs e)
		{
			_selectedDate = _selectedDate.AddDays(+1);
			LoadSelectedDay();
		}

		/// <summary>
		/// Fetches the selected day's logs and sets them as the item source of listview.
		/// </summary>
		private void LoadSelectedDay()
		{
			_logs = _repo.GetByDate(_selectedDate);
			Logs.ItemsSource = _logs;
			CalculateDayTotal();
			LblDate.Content = _selectedDate.ToString("d.M.yyyy");
		}

		/// <summary>
		/// Opens a log form to create a new log
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnNewLog_Click(object sender, RoutedEventArgs e)
		{
			var logform = new LogForm();
			var success = logform.ShowDialog() ?? false;

			if (!success) return;
			_repo.Add(logform.Model);
			LoadSelectedDay();
		}

		private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var workLog = ((ListViewItem)sender).Content as WorkLog;
			
			var logform = new LogForm(workLog);
			var success = logform.ShowDialog() ?? false;

			if (!success) return;
			//_repo.Add(logform.Model);
			LoadSelectedDay();

		}
	}
}
