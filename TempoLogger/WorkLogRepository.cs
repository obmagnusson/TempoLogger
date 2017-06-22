using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using TempoLogger.Models;

namespace TempoLogger
{
	public class WorkLogRepository
	{
		private readonly List<WorkLog> _logs;

		public WorkLogRepository()
		{
			try
			{
				// Create a new serializer
				var serializer = new XmlSerializer(typeof(List<WorkLog>));

				// Create a StreamReader
				// TODO check if file exists
				TextReader reader = new StreamReader("logs.xml");

				// Deserialize the file
				_logs = (List<WorkLog>)serializer.Deserialize(reader);

				// Close the reader
				reader.Close();
			}
			catch (Exception)
			{
				_logs = new List<WorkLog>();
			}
		}

		public List<WorkLog> GetByDate(DateTime date)
		{
			return _logs.Where(x => x.Date.Date == date.Date).ToList();
		}

		public void Add(WorkLog entity)
		{
			_logs.Add(entity);
		}

		public void Delete(WorkLog entity)
		{
			_logs.Remove(entity);
		}

		/// <summary>
		/// Saves the list of logs to file
		/// </summary>
		public void Save()
		{
			// Create a new Serializer
			var serializer = new XmlSerializer(typeof(List<WorkLog>));

			// Create a new StreamWriter
			TextWriter writer = new StreamWriter("logs.xml");

			// Serialize the file
			serializer.Serialize(writer, _logs);

			// Close the writer
			writer.Close();
		}

	}
}
