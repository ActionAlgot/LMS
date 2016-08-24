using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models {
	public abstract class Schedule {
		public Schedule() { }
		public virtual ICollection<Lecture> Lectures { get; set; }
	}

	public class KlassSchedule : Schedule {
		[Key, ForeignKey("Klass")]
		public int ID { get; set; }
		public virtual Klass Klass { get; set; }
	}

	public class UserSchedule : Schedule {
		public UserSchedule(Schedule l1, Schedule l2) {
			Lectures = (ICollection<Lecture>)l1.Lectures.Concat(l2.Lectures);
		}
	}
}