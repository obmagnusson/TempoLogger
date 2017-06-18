using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TempoLogger.Models;

namespace TempoLogger
{
	public class JiraApi
	{
		/// <summary>
		/// Posts the list of worklogs to Jira, marks them as read and reports the progress
		/// </summary>
		/// <param name="logs"></param>
		/// <param name="progressDialog"></param>
		/// <param name="progress"></param>
		public async Task PostWorkLogs(List<WorkLog> logs, ProgressDialog progressDialog, IProgress<int> progress)
		{
			try
			{
				await PostWorkLogsHelper(logs, progressDialog, progress);
			}
			finally
			{
				progressDialog.Close();
			}
		}

		private async Task PostWorkLogsHelper(IReadOnlyCollection<WorkLog> logs, ProgressDialog progressDialog, IProgress<int> progress)
		{
			var totalCount = logs.Count;
			var tempCount = 0;


			// Only post logs that are not marked as logged
			foreach (var log in logs.Where(x => !x.Logged))
			{
				tempCount++;
				//await the processing and uploading logic here
				await PostWorkLog(log);
				progress?.Report((tempCount * 100 / totalCount));
				log.Logged = true;
			}
		}

		private Task PostWorkLog(WorkLog log)
		{
			return Task.Run(() =>
			{
				Thread.Sleep(1000);
			});
		}

	}
}
