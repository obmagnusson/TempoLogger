using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
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
		private static Cookie _cookie;
		private const string BaseUrl = "https://sendiradid.atlassian.net";

		/// <summary>
		/// Posts the list of worklogs to Jira, marks them as read and reports the progress
		/// </summary>
		/// <param name="logs"></param>
		/// <param name="progressDialog"></param>
		/// <param name="progress"></param>
		public async Task PostWorkLogs(List<WorkLog> logs, ProgressDialog progressDialog, IProgress<int> progress)
		{
			if (!logs.Any()) return;

			// Check if cookie has expired
			//TODO if (_cookie != null && !TestCookie()) _cookie = null;

			// If user is not authenticated and authentication fails, return
			if(_cookie == null && !await Authenticate()) return;

			try
			{
				await PostWorkLogsHelper(logs, progress);
			}
			finally
			{
				progressDialog.Close();
			}
		}

		/// <summary>
		/// Contains all async stuff for posting worklogs so that a dialog can be shown after it has
		/// started and closed when it has finished
		/// </summary>
		/// <param name="logs"></param>
		/// <param name="progress"></param>
		/// <returns></returns>
		private async Task PostWorkLogsHelper(IReadOnlyCollection<WorkLog> logs, IProgress<int> progress)
		{
			// Only post logs that are not marked as logged
			var unLogged = logs.Where(x => !x.Logged).ToList();
			var totalCount = unLogged.Count;
			var tempCount = 0;

			foreach (var log in unLogged)
			{
				tempCount++;
				var issue = await GetIssue(log.Issue);
				//await PostWorkLog(log);
				progress?.Report(tempCount * 100 / totalCount);
				log.Logged = true;
			}
		}

		private async Task<Issue> GetIssue(string key)
		{
			var uri = new Uri(BaseUrl);

			var cookieContainer = new CookieContainer();

			cookieContainer.Add(uri, _cookie);

			var handler = new HttpClientHandler
			{
				CookieContainer = cookieContainer
			};
			using (var client = new HttpClient(handler))
			{
				client.BaseAddress = new Uri(BaseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync($"rest/api/2/issue/{key}?fields=summary,description,timetracking,io.tempo.jira__account");
				var json = await response.Content.ReadAsStringAsync();

				var issue = JsonConvert.DeserializeObject<Issue>(json);
				return issue;
			}
		}

		//private Task PostWorkLog(WorkLog log)
		//{
		//	var uri = new Uri(BaseUrl);

		//	var cookieContainer = new CookieContainer();

		//	cookieContainer.Add(uri, _cookie);

		//	var handler = new HttpClientHandler
		//	{
		//		CookieContainer = cookieContainer
		//	};
		//	using (var client = new HttpClient(handler))
		//	{
				
		//	}
		//}


		private static async Task<bool> Authenticate()
		{
			while (true)
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


					var response = await client.PostAsync("/rest/auth/1/session", body);

					if (response.StatusCode == HttpStatusCode.Unauthorized)
					{
						MessageBox.Show("Incorrect username or password", "Login failed", MessageBoxButton.OK);
						continue;
					}
					if (!response.IsSuccessStatusCode)
					{
						MessageBox.Show("Error", "Login failed", MessageBoxButton.OK);
						continue;
					}

					var model = new { session = new {name = "", value = ""}};

					var responseContent = await response.Content.ReadAsStringAsync();
					var session = JsonConvert.DeserializeAnonymousType(responseContent, model);

					_cookie = new Cookie(session.session.name, session.session.value);
					return true;
				}
			}
		}
	}
}
