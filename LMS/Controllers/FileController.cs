using LMS.Models;
using LMS.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace LMS.Controllers {
    public abstract class FileController<T> : Controller where T : LMS.Models.File {
		protected FileRepository<T> repo = new FileRepository<T>();

		public FileContentResult Download(int ID) {
			var file = repo.GetSpecific(ID);
			return File(file.Content, file.ContentType, file.FileName);
		}

		public ViewResult Index(int KlassID) {
			ViewBag.KlassName = repo.GetKlassName(KlassID);
			ViewBag.KlassID = KlassID;			
			return View(repo.GetKlassFiles(KlassID).ToList());
		}
		[HttpPost]
		public ActionResult GetDocument(HttpPostedFileBase file, string KlassID)
		{
			// Verify that the user selected a file
			if (file != null && file.ContentLength > 0)
			{
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

				T dgf = (T)Activator.CreateInstance(typeof(T), new object[] { });// {FileName = "aaaa" , ContentType = MimeMapping.GetMimeMapping("shit6.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the sixth brown fuck you"), Uploader;
				dgf.FileName = fileName;
				dgf.ContentType = contentType;
				dgf.UploaderID = User.Identity.GetUserId();
				dgf.KlassID = Int32.Parse(KlassID);
				dgf.Content = data;

				// Show success ...
				repo.Add(dgf);
				return RedirectToAction("Index", new { KlassID = KlassID });
			}
			else
			{
				// Show error ...
				return View("Error");
			}
		}
	   
    }
}