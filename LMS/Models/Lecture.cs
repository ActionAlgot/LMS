using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models {

	public abstract class _lecture {
		public virtual DateTime Start { get; set; }
		public virtual DateTime End { get; set; }
		public abstract string Name { get; }
		public virtual string Description { get; set; }
		public virtual string Location { get; set; }

		public IEnumerable<_lecture> GetOverlapping(IEnumerable<_lecture> lectures) {
			return lectures.Where(that
				=> this != that
				&& this.Start < that.End
				&& that.Start < this.End);
		}
		public IEnumerable<_lecture> GetOverlapping(Schedule schedule) {
			return GetOverlapping(schedule.Lectures);
		}
	}

	public class OverlapLecture : _lecture {
		public override string Name { get { return "Overlap"; } }
	}

	public class Lecture : _lecture {
		[Key]
		public int ID { get; set; }

		public override DateTime Start { get; set; }
		public override DateTime End { get; set; }
		public override string Name { get { return Schedule.Klass.Name; } }
		public override string Description { get; set; }
		public override string Location { get; set; }

		public int ScheduleID { get; set; }
		[ForeignKey("ScheduleID")]
		public virtual KlassSchedule Schedule { get; set; }

		public IEnumerable<_lecture> GetOverlapping() {
			return GetOverlapping(Schedule);
		}
	}
}