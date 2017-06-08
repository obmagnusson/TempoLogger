using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempoLogger.Models;

namespace TempoLogger
{
	public class WorkLogRepository
	{
		private readonly List<WorkLog> _logs = new List<WorkLog>
			{
				new WorkLog
				{
					Issue = "AB-123",
					Title = "Test issue",
					Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce non nibh quis odio aliquet venenatis.",
					From = "13:00",
					To = "14:00",
					Duration = "1h",
					Date = DateTime.Now
				},
				new WorkLog
				{
					Issue = "AB-123",
					Title = "Test issue",
					Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce non nibh quis odio aliquet venenatis.",
					From = "13:00",
					To = "14:00",
					Duration = "1h",
					Date = DateTime.Now
				},
				new WorkLog
				{
					Issue = "AB-123",
					Title = "Test issue",
					Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce non nibh quis odio aliquet venenatis.",
					From = "13:00",
					To = "14:00",
					Duration = "1h",
					Date = DateTime.Now
				},
				new WorkLog
				{
					Issue = "AB-123",
					Title = "Test issue",
					Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce non nibh quis odio aliquet venenatis.",
					From = "13:00",
					To = "14:00",
					Duration = "1h",
					Date = DateTime.Now
				},
				new WorkLog
				{
					Issue = "XY-321",
					Title = "Test issue",
					Comment = "Lucas ipsum dolor sit amet ubese aayla max quermian owen terrik bib soontir ryn x1. Darth binks alderaan darpa x1 anthos vratix.",
					From = "13:00",
					To = "14:00",
					Duration = "1h",
					Date = DateTime.Now.AddDays(-1)
				},
				new WorkLog
				{
					Issue = "XY-321",
					Title = "Test issue",
					Comment = "Lucas ipsum dolor sit amet ubese aayla max quermian owen terrik bib soontir ryn x1. Darth binks alderaan darpa x1 anthos vratix.",
					From = "13:00",
					To = "14:00",
					Duration = "1h",
					Date = DateTime.Now.AddDays(-1)
				},
				new WorkLog
				{
					Issue = "XY-321",
					Title = "Test issue",
					Comment = "Lucas ipsum dolor sit amet ubese aayla max quermian owen terrik bib soontir ryn x1. Darth binks alderaan darpa x1 anthos vratix.",
					From = "13:00",
					To = "14:00",
					Duration = "1h",
					Date = DateTime.Now.AddDays(-1)
				},
				new WorkLog
				{
					Issue = "XY-321",
					Title = "Test issue",
					Comment = "Lucas ipsum dolor sit amet ubese aayla max quermian owen terrik bib soontir ryn x1. Darth binks alderaan darpa x1 anthos vratix.",
					From = "13:00",
					To = "14:00",
					Duration = "1h",
					Date = DateTime.Now.AddDays(-1)
				},
			};
		public List<WorkLog> GetByDate(DateTime date)
		{
			return _logs.Where(x => x.Date.Date == date.Date).ToList();
		}
	}
}
