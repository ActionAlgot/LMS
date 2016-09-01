using LMS.Models;
using LMS.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LMS.Controllers
{
	[Authorize(Roles="Teacher")]
	//todo här ska studeter inte komma åt heka filecontroller.
	//[Authorize(Roles = "Student")]
	public class TeacherSubmissionController : FileController<SubmissionFile>
	{

		
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