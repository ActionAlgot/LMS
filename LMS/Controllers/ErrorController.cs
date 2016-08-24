using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        public ActionResult NotYourKlass()
        {
            return View("NotYourKlass");
        }
    }
}