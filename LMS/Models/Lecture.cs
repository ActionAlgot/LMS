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

		//number of times lectures overlap at the most within a single instant over 'this'
		public int GetMaxOverlaps(IEnumerable<_lecture> lectures) {	
			Func<int, int, int> getLargest = (n1, n2) => n1 > n2 ? n1 : n2;
			return this.GetOverlapping(lectures).Aggregate(0,
					(n, l2) => getLargest(n,
						l2.GetOverlapping(lectures).Intersect(this.GetOverlapping(lectures))
						.Count()+2));	//+2 for 'this' and 'l2'
		}

		public int GetMaxOverlaps(Schedule schedule) {
			return GetMaxOverlaps(schedule.Lectures);
		}

		//number of times lectures overlap at the most within a single instant over all the lectures connected by overlapping from 'this'
		public int GetMaxTreeOverlaps(IEnumerable<_lecture> lectures) {	
			List<_lecture> l = new List<_lecture>();
			var nl = GetOverlapping(lectures);
			while (nl.Any()) {
				l.AddRange(nl);
				nl = nl.SelectMany(j => j
					.GetOverlapping(lectures).Except(l));
			}
			return l.Aggregate((l1, l2) => l1.GetMaxOverlaps(lectures) > l2.GetMaxOverlaps(lectures) ? l1 : l2)
				.GetMaxOverlaps(lectures);
		}

		public int GetMaxTreeOverlaps(Schedule schedule) {
			return GetMaxTreeOverlaps(schedule.Lectures);
		}

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

	public class OverlapLecture : _lecture {	//lecture representing several 'Lecture's which are too overlapped to display
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