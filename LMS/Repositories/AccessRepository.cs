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