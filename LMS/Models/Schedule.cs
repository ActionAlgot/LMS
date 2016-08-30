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

		private class FuckOOPEqualityComparer<T> : IEqualityComparer<T> {
			private Func<T, T, bool> Comparer;
			private Func<T, int> Hashcoder;

			public FuckOOPEqualityComparer(Func<T, T, bool> comparer, Func<T, int> hash) {
				Comparer = comparer;
				Hashcoder = hash;
			}
		
			public bool Equals(T x, T y){
				return Comparer(x, y);
			}

			public int GetHashCode(T obj){
				return Hashcoder(obj);
			}
		}

		//gets timespan where lecrtures overlap n times
		public IEnumerable<OverlapLecture> GetOverlaps(int minOverlaps){
			return Lectures
				.Where(l => l.GetOverlapping(this).Count() >= minOverlaps)
				.Where(l => l.GetOverlapping(this)
					.Where(l2 =>	//lectures overlap at least 'minOverlaps' times in a single instant
						(l2.GetOverlapping(this).Intersect(l.GetOverlapping(this)
						).Count() >= minOverlaps -1))	//-1 to account for 'l'
					.Any())
				.Select(l => {	//'lecture' thats spans over lectures involved in overlap
					var newL = new OverlapLecture { Start = l.Start, End = l.End };
					while(newL.GetOverlapping(this).Any()){
						var seedL = newL;
						newL = newL.GetOverlapping(this).Aggregate(
							newL,
							(fug1, fug2) =>
								new OverlapLecture {
									Start = fug1.Start < fug2.Start ? fug1.Start : fug2.Start,
									End = fug1.End > fug2.End ? fug1.End : fug2.End
								});
						if (seedL.Start == newL.Start && seedL.End == newL.End) break;
					}
					return newL;
					})
				.Distinct(new FuckOOPEqualityComparer<OverlapLecture>(
					(a, b) => a.Start == b.Start && a.End == b.End,
					obj => obj.ToString().GetHashCode()))
				.OrderBy(l => l.Start)	//sort to ensure weirdness does not happen in logiuc below
				.Aggregate(new List<OverlapLecture>(),	//combine overlapping overlaps
					(lList, l) =>{
						var lel = lList.SingleOrDefault(el => el.Start < l.End && l.Start < el.End);
						if (lel != null) {
							lel.Start = lel.Start < l.Start ? lel.Start : l.Start;
							lel.End = lel.End < l.End ? lel.End : l.End;
						} else lList.Add(l);
						return lList;
						}
				);
		}
	}

	public class KlassSchedule : Schedule {
		[Key, ForeignKey("Klass")]
		public int ID { get; set; }
		public virtual Klass Klass { get; set; }
	}

	public class UserSchedule : Schedule {
		public UserSchedule() { }
		public UserSchedule(Schedule l1, Schedule l2) {
			Lectures = (ICollection<Lecture>)l1.Lectures.Concat(l2.Lectures);
		}
	}
}