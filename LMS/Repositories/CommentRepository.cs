using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Repositories {
	public class CommentRepository {
		private ApplicationDbContext ctx = new ApplicationDbContext();

		public void Add(Comment comment) {
			ctx.Comments.Add(comment);
			ctx.SaveChanges();
		}

		public IEnumerable<Comment> getFeedback(string userId)
		{
			return ctx.Users.SingleOrDefault(u => u.Id == userId).SubmittedFiles
				.SelectMany(f => f.Comments)
				.OrderBy(c => c.Date);

			//Här ska en lista med filer och deras feedback kompileras ihop och återsändas som en viewmodel 

		}
	}
}