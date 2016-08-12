using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Repositories {
	public class KlassRepository {
		private ApplicationDbContext ctx = new ApplicationDbContext();

		public IEnumerable<Klass> GetAll() {
			return ctx.Klasses;
		}

		public Klass GetSpecific(int Id) {
			return ctx.Klasses.SingleOrDefault(k => k.ID == Id);
		}

		public void Add(int Id, string UId) {
			ctx.Klasses.SingleOrDefault(k => k.ID == Id).Members.Add(ctx.Users.SingleOrDefault(u => u.Id == UId));
			ctx.SaveChanges();
		}

		public void Remove(int Id, string UId) {
			ctx.Klasses.SingleOrDefault(k => k.ID == Id).Members.Remove(ctx.Users.SingleOrDefault(u => u.Id == UId));
			ctx.SaveChanges();
		}
	}
}