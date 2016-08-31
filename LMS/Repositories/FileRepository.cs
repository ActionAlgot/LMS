using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.Models;
using System.Data.Entity;

namespace LMS.Repositories {
	public class FileRepository<T> where T : File {
		private ApplicationDbContext ctx = new ApplicationDbContext();
		private DbSet<T> Files {	//shorthand for the proper DbSet, with plenty of ridiculous casting because compilator is a bitch
			get { 
				return (DbSet<T>)
					((typeof(T).Equals(typeof(SharedFile)))
					? (IQueryable<T>) ctx.SharedFiles
					: (IQueryable<T>) ctx.SubmissionFiles);
			}
		}

		/*public IEnumerable<T> GetMySubmittedFiles(string userId)
		{
			//ctx.Klasses.SingleOrDefault(k => k.ID == KlassID) //proper check if the klass exists
			return Files.Where(f => f.UploaderID == userId);
		} */

		public T GetSpecific(int ID) {
			return Files.FirstOrDefault(f => f.ID == ID);
		}

		public string GetKlassName(int ID) {
			var klass = ctx.Klasses.SingleOrDefault(k => k.ID == ID);
			return klass != null ? klass.Name : null;
		}
		public IEnumerable<T> GetKlassFiles(int KlassID) {
			//ctx.Klasses.SingleOrDefault(k => k.ID == KlassID) //proper check if the klass exists
			return Files.Where(f => f.KlassID == KlassID);
		}

		public void Add(T file) {
			Files.Add(file);
			ctx.SaveChanges();
		}

		public void Remove(T file) {
			Files.Remove(file);
			ctx.SaveChanges();
		}
		

	}
}