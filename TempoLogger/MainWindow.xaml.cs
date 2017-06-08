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
		private readonly List<WorkLog> _logs;

		public MainWindow()
		{
			InitializeComponent();

			_logs = new List<WorkLog>
			{
				new WorkLog
				{
					Issue = "AB-123",
					Title = "Test issue",
					From = "13:00",
					To = "14:00",
					Duration = "1h"
				},
				new WorkLog
				{
					Issue = "AB-123",
					Title = "Test issue",
					From = "13:00",
					To = "14:00",
					Duration = "1h"
				},
				new WorkLog
				{
					Issue = "AB-123",
					Title = "Test issue",
					From = "13:00",
					To = "14:00",
					Duration = "1h"
				},
				new WorkLog
				{
					Issue = "AB-123",
					Title = "Test issue",
					From = "13:00",
					To = "14:00",
					Duration = "1h"
				},
			};

			logs.ItemsSource = _logs;

			CurrentDayTotal.Content = "0";
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

			CurrentDayTotal.Content = WorkLogHelper.SecondsToString(totalSeconds);
		}

	}
}
