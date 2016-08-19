using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.Models;
using System.Data.Entity;

namespace LMS.Repositories {
	public class FileRepository<T> where T : File {
		private ApplicationDbContext ctx = new ApplicationDbContext();
		private DbSet<T> Files { get { return (DbSet<T>)((typeof(T).Equals(typeof(SharedFile))) ? (IQueryable<File>)ctx.SharedFiles : (IQueryable<File>) ctx.SubmissionFiles); } }

		public File GetSpecific(int ID) {
			return Files.FirstOrDefault(f => f.ID == ID);
		}

		public void Add(T file) {
			Files.Add(file);
			ctx.SaveChanges();
		}

	}
}