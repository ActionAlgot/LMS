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

		[HttpGet, Authorize(Roles="Teacher")]
		public ActionResult Edit(int ID) {
			return View(repo.get(ID));
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(Lecture lecture) {
			if (ModelState.IsValid && repo.Update(lecture)) return RedirectToAction("Lecture", new { ID = lecture.ID });
			return View(lecture);
		}

		[HttpGet, Authorize(Roles = "Teacher")]
		public ActionResult Create(int sID) {
			return View(new Lecture { ScheduleID = sID });
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(Lecture lecture) {
			if (ModelState.IsValid && repo.Add(lecture)) return RedirectToAction("Lecture", new { ID = lecture.ID });
			return View(lecture);
		}

		[HttpGet, Authorize(Roles = "Teacher")]
		public ActionResult Remove(int ID) {
			return View(repo.get(ID));
		}

		[HttpPost, ValidateAntiForgeryToken, ActionName("Remove")]
		public ActionResult RemoveConfirmed(int ID) {
			repo.Delete(ID);
			return RedirectToAction("Index", "Home");	//TODO proper redirect to previous
		}
    }
}