using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
	[Authorize(Roles="Teacher")]
    public class SubmissionController : FileController<SubmissionFile>{

		public ActionResult Create()
		{
			return View();
		}

    }
}