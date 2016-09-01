using LMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace LMS.Controllers {
	[Authorize]
    public class ScheduleController : Controller {
		private ScheduleRepository repo = new ScheduleRepository();

        public ActionResult MySchedule(int? year, int? week) {
			ViewBag.Year = year;
			ViewBag.Week = week;
			return View(repo.GetUserSchedule(User.Identity.GetUserId()));
        }
    }
}