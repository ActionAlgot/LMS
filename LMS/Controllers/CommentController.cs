using LMS.Models;
using LMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace LMS.Controllers {
	[Authorize(Roles="Teacher")]
    public class CommentController : Controller {
		private CommentRepository repo = new CommentRepository();

		[HttpPost]
		public JsonResult Create(Comment comment) {
			if (ModelState.IsValid) {
				comment.Date = DateTime.Now;
				comment.Read = false;
				comment.CommenterID = User.Identity.GetUserId();
				repo.Add(comment);
			}
			return Json(new { }, JsonRequestBehavior.AllowGet);
		}
    }
}