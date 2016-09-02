using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.Models;

namespace LMS.Repositories {
	public class LectureRepository {
		private ApplicationDbContext ctx = new ApplicationDbContext();

		public Lecture get(int ID) {
			return ctx.Lectures.FirstOrDefault(l => l.ID == ID);
		}

		public bool Update(Lecture lecture) {
			ctx.Lectures.Add(lecture);
			ctx.SaveChanges();
			return true;
		}

		public bool Add(Lecture lecture) {
			ctx.Lectures.Add(lecture);
			ctx.SaveChanges();
			return true;
		}

		public void Delete(int ID) {
			ctx.Lectures.Remove(get(ID));
			ctx.SaveChanges();
		}
	}
}