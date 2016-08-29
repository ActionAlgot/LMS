using LMS.Models;
using LMS.Repositories;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class SharedFilesController : FileController<SharedFile>
    {
		public ActionResult Share()
		{
			var repo = new KlassRepository();
			var klasses = repo.GetMyClasses(User.Identity.GetUserId()).Select(k => new SelectListItem { Value = k.ID.ToString(), Text = k.Name });
			var model = new UploadFileViewModel();
			model.KlassList = klasses;
			return View("Share", model);
		}


    }
}