using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using TempoLogger.Models;
using TempoLogger.Windows;

namespace TempoLogger
{
	public class JiraApi
	{
		private readonly string BaseUrl = "https://sendiradid.atlassian.net";


		/// <summary>
		/// Posts the list of worklogs to Jira, marks them as read and reports the progress
		/// </summary>
		/// <param name="logs"></param>
		/// <param name="progressDialog"></param>
		/// <param name="progress"></param>
		public async Task PostWorkLogs(List<WorkLog> logs, ProgressDialog progressDialog, IProgress<int> progress)
		{
			// If user is not authenticated and authentication fails, return
			if(!CheckAuthentication() && !await Authenticate()) return;;
			//try
			//{
			//	await PostWorkLogsHelper(logs, progressDialog, progress);
			//}
			//finally
			//{
			//	progressDialog.Close();
			//}
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

		/// <summary>
		/// Checks if a cookie exists for Jira Api, if it does not then it opens login dialog
		/// </summary>
		private bool CheckAuthentication()
		{
			var cookiePath = new Uri(BaseUrl);
			try
			{
				//// Calculate "one day ago"
				//DateTime expiration = DateTime.UtcNow - TimeSpan.FromDays(1);
				//// Format the cookie as seen on FB.com.  Path and domain name are important factors here.
				//string cookie = String.Format("{0}=; expires={1}; path=/; domain=.facebook.com", "", expiration.ToString("R"));
				//// Set a single value from this cookie (doesnt work if you try to do all at once, for some reason)
				//Application.SetCookie(cookiePath, cookie);
				Application.GetCookie(cookiePath);
				return true;
			}
			catch (Win32Exception)
			{
				return false;
			}
		}

		private async Task<bool> Authenticate()
		{
			var loginDialog = new LoginDialog();
			var result = loginDialog.ShowDialog() ?? false;

			if (!result) return false;

			
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var data = JsonConvert.SerializeObject(new
				{
					username = loginDialog.Username,
					password = loginDialog.Password
				});

				var body = new StringContent(data, Encoding.UTF8, "application/json");


				var response = await client.PostAsync("jira/rest/auth/1/session", body);

				if (response.StatusCode == HttpStatusCode.Unauthorized)
				{
					MessageBox.Show("Login failed", "Incorrect username or password", MessageBoxButton.OK);
				}
				if (!response.IsSuccessStatusCode) return false;

				var responseContent = await response.Content.ReadAsStringAsync();
				var session = JsonConvert.DeserializeObject<JiraAuthenticationSession>(responseContent);

				Application.SetCookie(new Uri(BaseUrl), session.Name + "=" + session.Value);
				return true;
			}
		}
	}
}
