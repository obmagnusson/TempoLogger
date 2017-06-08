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

			lblCurrentDayTotal.Content = "0";
			CalculateDayTotal();
		}

		/// <summary>
		/// Calculates and sets the total value for the day
		/// </summary>
		private void CalculateDayTotal()
		{
			var totalSeconds = 0;
			foreach (var log in _logs)
			{
				totalSeconds += log.GetDurationSeconds();
			}

			lblCurrentDayTotal.Content = WorkLogHelper.SecondsToString(totalSeconds);
		}

		private void btnPrev_Click(object sender, RoutedEventArgs e)
		{
			_selectedDate = _selectedDate.AddDays(-1);
			LoadSelectedDay();
		}

		private void btnToday_Click(object sender, RoutedEventArgs e)
		{
			_selectedDate = DateTime.Now;
			LoadSelectedDay();
		}

		private void btnNext_Click(object sender, RoutedEventArgs e)
		{
			_selectedDate = _selectedDate.AddDays(+1);
			LoadSelectedDay();
		}

		private void LoadSelectedDay()
		{
			_logs = _repo.GetByDate(_selectedDate);
			logs.ItemsSource = _logs;
			CalculateDayTotal();
			lblDate.Content = _selectedDate.ToString("d.M.yyyy");
		}
	}
}
