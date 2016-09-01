using LMS.Models;
using LMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace LMS.Controllers
{
	[Authorize(Roles="Student")]
    public class StudentSubmissionController : Controller
    {
		private FileRepository<SubmissionFile> repo = new FileRepository<SubmissionFile>();

		public ActionResult Submit()
		{
			var klassRepo = new KlassRepository();
			var klasses = klassRepo.GetMyClasses(User.Identity.GetUserId()).Select(k => new SelectListItem { Value = k.ID.ToString(), Text = k.Name });
			var model = new UploadFileViewModel();
			model.KlassList = klasses;

			return View("Submit", model);
		}
    }
}