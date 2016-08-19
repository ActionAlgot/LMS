using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS.Models;
using LMS.Repositories;

namespace LMS.Controllers
{
	[Authorize(Roles = "Teacher")]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            var repo = new UserRepository();
            return View(repo.GetAll());
        }

		public JsonResult GetAll() {
			var repo = new UserRepository();
			return Json(repo.GetAll(), JsonRequestBehavior.AllowGet);
		}
		
		public ActionResult Students()
		{
			var repo = new UserRepository();
			return View("Index",repo.GetAllStudents() );

		}
        //Form for create user
        public ActionResult Create()
        {
            return View();
        }

        //Create user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel newUser) /*[Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)*/
        {
            if (ModelState.IsValid)
            {
                var repo = new UserRepository();
                bool result = repo.CreateNewUser(newUser);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(newUser);
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var repo = new UserRepository();
            UserViewModel user = repo.GetSpecific(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
            /*ApplicationUser applicationUser = UserManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);*/
        }




		//FRÅGA OM ANVÄNDAREN SKA TAS BORT
		// GET: Users/Delete/5
		public ActionResult Delete(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var repo = new UserRepository();
			UserViewModel user = repo.GetSpecific(id);
			return View(user);
		}


		//TA BORT PÅ RIKTIGT
		// POST: Users/Delete/5
		//[ValidateAntiForgeryToken]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(string id)
		{
			var repo = new UserRepository();
			if (repo.Remove(id))
			{
				return RedirectToAction("Index");
			}
			return View();
		}


		public ActionResult Edit(string id, string returnUrl)
		{
			if (id == null)	   //todo isNullOrEmpty
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var repo = new UserRepository();
			UserViewModel user = repo.GetSpecific(id);
			ViewBag.ReturnUrl = returnUrl;
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,UserName")] UserViewModel user, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var repo = new UserRepository();
				repo.Update(user);
				//return RedirectToAction("Index");
				//Response.Cookies.Get("UserEditLocation") == "Klass"
				//var value=Request.Cookies["key"].Value
				var value = Request.Cookies["UserEditLocation"].Value;

				return Redirect(returnUrl);
				/*if (value == "Klass")
				{
					return RedirectToRoute(new { controller = "Klass", action = "Index" });
				}
				else
				{
					return RedirectToRoute(new { controller = "Users", action = "Index" });
				} */
			}
			return View(user);
		}



        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
				UserManager.Create(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			ApplicationUser applicationUser = UserManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			ApplicationUser applicationUser = UserManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Delete/5
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
			ApplicationUser applicationUser = UserManager.FindById(id);
			UserManager.Delete(applicationUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        */
    }
}
