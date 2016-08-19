using LMS.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/* Work in progress */
namespace LMS.Repositories
{
    public class AccessRepository
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public AccessRepository()
        {
        }

        public AccessRepository(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        //Förenklingar av rättighetskontroll
        public dynamic StudentAttendsKlass(ApplicationUser user, Klass klass)
        {
            if (IsAdmin(user)) return true;
            return new NotImplementedException();
        }

        public dynamic KlassHasStudent(Klass klass, ApplicationUser user)
        {
            if (IsAdmin(user)) return true;
            return new NotImplementedException();
        }

        public dynamic TeacherLeadsKlass(ApplicationUser user, Klass klass)
        {
            if (IsAdmin(user)) return true;
            return new NotImplementedException();
        }

        public dynamic KlassHasTeacher(ApplicationUser user, Klass klass)
        {
            if (IsAdmin(user)) return true;
            return new NotImplementedException();
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