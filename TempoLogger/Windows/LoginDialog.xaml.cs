using System.Windows;
using System.Windows.Input;

namespace TempoLogger.Windows
{
	/// <summary>
	/// Interaction logic for LoginDialog.xaml
	/// </summary>
	public partial class LoginDialog
	{
		public string Username { get; set; }
		public string Password { get; set; }

		public LoginDialog()
		{
			InitializeComponent();
			TxtUsername.Focus();

			TxtUsername.KeyDown += TextBox_OnEnter;
			TxtPassword.KeyDown += TextBox_OnEnter;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var mainWindow = Application.Current.MainWindow;
			Left = mainWindow.Left + (mainWindow.Width - ActualWidth) / 2;
			Top = mainWindow.Top + (mainWindow.Height - ActualHeight) / 2;
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			Submit();
		}

		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}

		private void TextBox_OnEnter(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return) Submit();
		}

		private void Submit()
		{
			DialogResult = true;
			Username = TxtUsername.Text;
			Password = TxtPassword.Password;
			Close();
		}
	}
}
