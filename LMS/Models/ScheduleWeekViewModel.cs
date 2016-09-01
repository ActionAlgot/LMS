using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models {
	public class ScheduleWeekViewModel {
		public Schedule Schedule { get; set; }
		public int Year { get; set; }
		public int WeekOfYear { get; set; }
	}
}