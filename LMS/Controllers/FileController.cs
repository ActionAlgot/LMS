using LMS.Models;
using LMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers {
    public abstract class FileController<T> : Controller where T : File {
		protected FileRepository<T> repo = new FileRepository<T>();
		public FileContentResult View(int ID) {
			var file = repo.GetSpecific(ID);
			return File(file.Content, file.ContentType);
		}
    }
}