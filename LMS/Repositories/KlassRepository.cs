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

		public void AddKlassMember(int Id, string UId) {
			ctx.Klasses.SingleOrDefault(k => k.ID == Id).Members.Add(ctx.Users.SingleOrDefault(u => u.Id == UId));
			ctx.SaveChanges();
		}

		public bool RemoveKlassMember(int Id, string UId) {
			var klass = ctx.Klasses.SingleOrDefault(k => k.ID == Id);
			if (klass != null) {
				var member = klass.Members.SingleOrDefault(m => m.Id == UId);
				if (member != null) {
					klass.Members.Remove(member);
					ctx.SaveChanges();
					return true;
				}
			}
			return false;
		}
	}
}