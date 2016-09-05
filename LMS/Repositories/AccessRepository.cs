using LMS.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

/* Work in progress */
namespace LMS.Repositories
{
    public class AccessRepository
    {
        private ApplicationDbContext ctx;
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public AccessRepository()
        {
            this.ctx = new ApplicationDbContext();
        }

        public ApplicationUserManager GetUserManager()
        {
            return this.UserManager;
        }

        public AccessRepository(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        //Kolla om en student är med i en klass
        public bool UserAttendsKlass(string userId, int KlassId)
        {
            Debug.WriteLine("Klass {0} och User {1}", KlassId, userId);
            Klass klass = ctx.Klasses.SingleOrDefault(k => k.ID == KlassId);
            ApplicationUser appUser = ctx.Users.SingleOrDefault(u => u.Id == userId);
            return klass.Members.Any(u => u.Id == appUser.Id);
        }

        //Returnera en lista på Klasser som den här användaren är medlem i
        //public IEnumerable<Klass> GetKlassesForUser(string userId)
        //{
            //ApplicationUser appUser = ctx.Users.SingleOrDefault(u => u.Id == userId);
            //Debug.WriteLine(appUser.Id);

            //IEnumerable<Klass> klasses = appUser.Klasses;
            //return klasses;
            /*  Klasses.Members.Any(u => u.Id == appUser.Id);*/
            //IEnumerable<Klass> klasses = ctx.Klasses.Where(x => x.Categories.Any(c => c.ID == id)); 

            /*var prod = EFModel.Products.Where(x => x.Categories.Any(c => c.ID == id));
            */
            /*IEnumerable<Klass> klasses = 
                from k in Klasses
                from u in AspNetUsers
                where u.Id == k.ID
                select s;*/

            /*return klasses;*/
        //}

        //Kolla om en User är en lärare
        public bool IsTeacher(string userId)
        {
            //ApplicationUser appUser = ctx.Users.SingleOrDefault(u => u.Id == userId);
            if(UserManager.IsInRole(userId, "Teacher"))
            {
                return true;
            }
            return false;
        }

		public bool IsStudent(string Id) {
			return UserManager.IsInRole(Id, "Student");
		}

        public bool IsAdmin(ApplicationUser user)
        {
            if (UserManager.IsInRole(user.Id, "Admin"))
            {
                return true;
            }
            return false;
        }
    }
}