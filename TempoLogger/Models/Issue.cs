// ReSharper disable InconsistentNaming

using Newtonsoft.Json;

namespace TempoLogger.Models
{

	public class Issue
	{
		public string expand { get; set; }
		public string id { get; set; }
		public string self { get; set; }
		public string key { get; set; }
		public IssueFields fields { get; set; }
	}

	public class IssueFields
	{
		public string summary { get; set; }
		public string description { get; set; }
		[JsonProperty(PropertyName = "io.tempo.jira__account")]
		public IssueAccount iotempojira__account { get; set; }
		public IssueTimetracking timetracking { get; set; }
	}

	public class IssueAccount
	{
		public int id { get; set; }
		public string value { get; set; }
	}

	public class IssueTimetracking
	{
		public string originalEstimate { get; set; }
		public string remainingEstimate { get; set; }
		public string timeSpent { get; set; }
		public int originalEstimateSeconds { get; set; }
		public int remainingEstimateSeconds { get; set; }
		public int timeSpentSeconds { get; set; }
	}
}
