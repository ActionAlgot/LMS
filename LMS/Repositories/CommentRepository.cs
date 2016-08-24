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
	}
}