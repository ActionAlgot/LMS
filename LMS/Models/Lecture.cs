using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models {
	public class Lecture {
		[Key]
		public int ID { get; set; }

		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public string Name { get { return Schedule.Klass.Name; } }
		public string Description { get; set; }
		public string Location { get; set; }

		public int ScheduleID { get; set; }
		[ForeignKey("ScheduleID")]
		public virtual KlassSchedule Schedule { get; set; }
	}
}