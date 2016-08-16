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

		public void Add(Klass klass) {
			ctx.Klasses.Add(klass);
			ctx.SaveChanges();
		}

		//get users that are not members of klass
		public IEnumerable<ApplicationUser> GetNonMembers(int Id){
			var klass = ctx.Klasses.SingleOrDefault(k => k.ID == Id);
			if (klass == null) return null;
			IQueryable<ApplicationUser> nonMembers = ctx.Users;
			foreach (var m in klass.Members) nonMembers = nonMembers.Where(u => u.Id != m.Id);
			return nonMembers;
		}

		public bool AddKlassMember(int Id, string UId) {
			var klass = ctx.Klasses.SingleOrDefault(k => k.ID == Id);
			var member = ctx.Users.SingleOrDefault(u => u.Id == UId);
			if (klass != null && member != null && !klass.Members.Any(u => u.Id == UId)) {
				klass.Members.Add(member);
				ctx.SaveChanges();
				return true;
			}
			return false;
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