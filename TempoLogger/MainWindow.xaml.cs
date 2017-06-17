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
			_repo.Save();
		}

		/// <summary>
		/// Opens a new LogForm window dialog to edit log that was double clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListViewItem_DoubleClick(object sender, MouseButtonEventArgs e)
		{
			var workLog = ((ListViewItem)sender).Content as WorkLog;
			EditHelper(workLog);
		}

		/// <summary>
		/// Opens a new LogForm window dialog to edit log that was double clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnEditLog_Click(object sender, RoutedEventArgs e)
		{
			var workLog = Logs.SelectedItem as WorkLog;
			EditHelper(workLog);
		}

		/// <summary>
		/// Opens edit dialog
		/// </summary>
		/// <param name="workLog"></param>
		private void EditHelper(WorkLog workLog)
		{
			var logform = new LogForm(workLog);
			var success = logform.ShowDialog() ?? false;

			if (!success) return;
			LoadSelectedDay();
			_repo.Save();
		}

		/// <summary>
		/// Deletes a log
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnDeleteLog_Click(object sender, RoutedEventArgs e)
		{
			var messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);

			if (messageBoxResult != MessageBoxResult.Yes) return;
			var workLog = Logs.SelectedItem as WorkLog;
			_repo.Delete(workLog);
			_repo.Save();
			LoadSelectedDay();
		}

		/// <summary>
		/// Trick to unselect
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Logs_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Logs.UnselectAll();
		}

		//private void ListViewItem_RightClick(object sender, MouseButtonEventArgs e)
		//{
		//	LogsContextMenu.IsOpen = true;
		//}
	}
}
