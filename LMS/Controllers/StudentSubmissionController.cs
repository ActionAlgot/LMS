using LMS.Models;
using LMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;

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

		[HttpPost]
		public ActionResult GetDocument(HttpPostedFileBase file, string SelectedKlassId)
		{
			// Verify that the user selected a file
			if (file != null && file.ContentLength > 0)
			{
				if (file.ContentLength > 10000000) { return RedirectToAction("FileSizeToBig", "Error"); };
				// Get file info
				var fileName = Path.GetFileName(file.FileName);
				var contentLength = file.ContentLength;
				var contentType = file.ContentType;

				// Get file data
				byte[] data = new byte[] { };
				using (var binaryReader = new BinaryReader(file.InputStream))
				{
					data = binaryReader.ReadBytes(file.ContentLength);
				}
				var newFile = new SubmissionFile();
				newFile.FileName = fileName;
				newFile.ContentType = contentType;
				newFile.UploaderID = User.Identity.GetUserId();
				newFile.KlassID = Int32.Parse(SelectedKlassId);
				newFile.Content = data;

				// Show success ...
				repo.Add(newFile);
				return RedirectToAction("Submit", new { KlassID = SelectedKlassId });
			}
			else
			{
				// Show error ...
				return View("Error");
			}
		}
    }
}