using System;

// ReSharper disable InconsistentNaming

namespace TempoLogger.Models
{
	public class WorkLogPostModel
	{
		public object id { get; set; }
		public IssuePostModel issue { get; set; }
		public int timeSpentSeconds { get; set; }
		public string dateStarted { get; set; }
		public string comment { get; set; }
		public Meta meta { get; set; }
		public Author author { get; set; }
		public Workattributevalue[] workAttributeValues { get; set; }
		public int billedSeconds { get; set; }
	}

	public class IssuePostModel
	{
		public string key { get; set; }
		public int remainingEstimateSeconds { get; set; }
		public string id { get; set; }
	}

	public class Meta
	{
		public string analyticsoriginaction { get; set; }
		public string analyticsoriginpage { get; set; }
		public string analyticsoriginview { get; set; }
	}

	public class Author
	{
		public string name { get; set; }
	}

	public class Workattributevalue
	{
		public object worklogId { get; set; }
		public Workattribute workAttribute { get; set; }
		public string value { get; set; }
	}

	public class Workattribute
	{
		public int id { get; set; }
		public string key { get; set; }
		public string name { get; set; }
		public Type type { get; set; }
		public string externalUrl { get; set; }
		public bool required { get; set; }
		public int sequence { get; set; }
		public object staticListValues { get; set; }
	}

	public class Type
	{
		public string name { get; set; }
		public string value { get; set; }
	}

}
