using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Repositories;

namespace LMS.Controllers
{
    public class LectureController : Controller {
		private LectureRepository repo = new LectureRepository();
        public ActionResult Lecture(int ID){
            return View(repo.get(ID));
        }
    }
}