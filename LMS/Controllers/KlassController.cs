using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
	[Authorize]
    public class KlassController : Controller {
		private Repositories.KlassRepository repo = new Repositories.KlassRepository();
        // GET: Klass
		[Authorize(Roles="Teacher")]
        public ActionResult Index(){
            return View(repo.GetAll());
        }

		public ActionResult Details(int Id) {
			return View(repo.GetSpecific(Id));
		}
		
	}
}