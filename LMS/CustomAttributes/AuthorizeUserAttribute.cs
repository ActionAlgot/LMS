using LMS.Models;
using LMS.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LMS.CustomAttributes
{
    /*
     * usage:
     * 
     * [Student(attendsKlass = true)] Nuvarande User måste vara med i klassen
     * [Student(attendsKlass = false)] Nuvarande User får inte vara med i klassen
     */

    public class StudentAttribute : AuthorizeAttribute
    {
        //custom
        public bool AttendsKlass { get; set; }
        //public string AccessLevel { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            //Kontrollera nu om studenten går i den här klassen
            AccessRepository accessRepo = new AccessRepository();

            //TODO: Ska komma från adressfältet eller dylikt
            string accessedKlassName = "k2";

            bool UserIsInKlass = accessRepo.UserAttendsKlass(httpContext.User.Identity.GetUserId(), accessedKlassName);

            //Användaren vill veta om usern attends a klass ( [Student(attendsKlass = true)] )
            if (AttendsKlass)
            {
                if (UserIsInKlass)
                {
                    Debug.Write("[Student(attendsKlass = true)] SANT!! usern är i denna klass");
                    return true;
                }
                else
                {
                    Debug.Write("[Student(attendsKlass = true)] FALSKT!! usern är inte i denna klass");
                    return false;
                }
            }
            else
            {
                if (UserIsInKlass)
                {
                    Debug.Write("[Student(attendsKlass = false)] SANT!! usern är inte i denna klass");
                    return true;
                }
                else
                {
                    Debug.Write("[Student(attendsKlass = false)] FALSKT!! usern är i denna klass");
                    return false;
                }
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary( new { controller = "Error", action = "AccessDenied" }));
        }
    }
}