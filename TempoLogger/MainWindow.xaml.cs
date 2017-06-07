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
using TempoLogger.Models;

namespace TempoLogger
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			var logList = new List<WorkLog>
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

			logs.ItemsSource = logList;
		}

	}
}
