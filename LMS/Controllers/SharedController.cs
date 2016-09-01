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
	[Authorize]
    public class SharedFilesController : FileController<SharedFile>
    {
		public ActionResult Share()
		{
			//var repo = new KlassRepository();
			//var klasses = repo.GetMyClasses(User.Identity.GetUserId()).Select(k => new SelectListItem { Value = k.ID.ToString(), Text = k.Name });
			//var model = new UploadFileViewModel();
			//model.KlassList = klasses;

			var klassRepo = new KlassRepository();
			var klasses = klassRepo.GetMyClasses(User.Identity.GetUserId())/*.Select(k => new SelectListItem { Value = k.ID.ToString(), Text = k.Name });*/	;
			var model = new UploadFileViewModel();
			model.KlassList = klasses.Select(k => new SelectListItem { Value = k.ID.ToString(), Text = k.Name });
			model.Files = klasses.SelectMany(k => k.Shared);

			return View("Share", model);
		}

		public override dynamic Download(int? ID)
		{
			int ID2;
			if (ID == null) { return View("AccessDenied"); }
			else ID2 = (int) ID;
			var Arepo = new AccessRepository();
			var file = repo.GetSpecific(ID2);
			if (file == null) { return View("AccessDenied"); }
			if (Arepo.UserAttendsKlass(User.Identity.GetUserId(), file.KlassID))
			{
				return File(file.Content, file.ContentType, file.FileName);
			}
			else
			{
				return View("AccessDenied");
			}
		}
    }
}