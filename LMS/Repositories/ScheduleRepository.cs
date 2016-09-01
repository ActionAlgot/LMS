using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.Models;

namespace LMS.Repositories {
	public class ScheduleRepository {
		private ApplicationDbContext ctx = new ApplicationDbContext();

		public UserSchedule GetUserSchedule(string uID) {
			var user = ctx.Users.SingleOrDefault(u => u.Id == uID);
			if(user == null) return null;
			return new UserSchedule { Lectures = (ICollection<Lecture>)(user.Klasses.SelectMany(k => k.Schedule.Lectures).ToList()) };
		}

		public KlassSchedule GetKlassSchedule(int kID) {
			return ctx.Schedules.FirstOrDefault(s => s.Klass.ID == kID);
		}
	}
}