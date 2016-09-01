using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Models;
using LMS.Repositories;

namespace LMS.Controllers
{
    public class LectureController : Controller {
		private LectureRepository repo = new LectureRepository();

        public ActionResult Lecture(int ID){
            return View(repo.get(ID));
        }

		[HttpGet]
		public ActionResult Edit(int ID) {
			return View(repo.get(ID));
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(Lecture lecture) {
			if (ModelState.IsValid && repo.Update(lecture)) return RedirectToAction("Lecture", new { ID = lecture.ID });
			return View(lecture);
		}
    }
}