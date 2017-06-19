using System.Windows;

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
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Username = TxtUsername.Text;
			Password = TxtPassword.Password;
			Close();
		}

		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}
