using System.Windows;

namespace TempoLogger
{
	/// <summary>
	/// Interaction logic for ProgressDialog.xaml
	/// </summary>
	public partial class ProgressDialog
	{
		public ProgressDialog()
		{
			InitializeComponent();
			ProgressBar.Value = 0;
		}

		public void SetProgress(int newProgress)
		{
			ProgressBar.Value = newProgress;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var mainWindow = Application.Current.MainWindow;
			Left = mainWindow.Left + (mainWindow.Width - ActualWidth) / 2;
			Top = mainWindow.Top + (mainWindow.Height - ActualHeight) / 2;
		}
	}
}
