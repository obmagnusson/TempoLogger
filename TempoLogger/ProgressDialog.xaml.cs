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
	}
}
