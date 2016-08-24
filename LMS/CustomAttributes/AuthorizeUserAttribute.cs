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
using System.Web.UI;

namespace LMS.CustomAttributes
{
    /*
     * usage:
     * 
     * [Student(AttendsKlass = true, ParamPos = 2)] Nuvarande User måste vara med i klassen
     * [Student(AttendsKlass = false, ParamPos = 2)] Nuvarande User får inte vara med i klassen
     */

    public class StudentAttribute : AuthorizeAttribute
    {
        //custom
        public bool AttendsKlass { get; set; }
        public int KlassIDParamPos { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            //Splitta URL så vi har varje del i en array - ex: /Klass/Details/3 blir { 'Klass', 'Details', '3' }
            var requestedAdress = httpContext.Request.RawUrl;
            char[] charsToTrim = { '/', ' ' };
            string trimmedAdress = requestedAdress.Trim(charsToTrim);
            char[] delimiter = { '/' };
            string[] adressParts = trimmedAdress.Split(delimiter);

            //Positionen för värdet vi ska använda finns i ParamPos från annoteringen
            if(KlassIDParamPos >= adressParts.Count())
            {
                Debug.WriteLine($"Fick ingen giltig parameterposition att jobba med. Parameter {KlassIDParamPos} finns inte.");
                return false;
            }
            string KlassIdFromUrl = adressParts[(int)KlassIDParamPos];
            Debug.WriteLine($"Parametern vi vill se är {KlassIdFromUrl}");
            //Kontrollera nu om studenten går i den här klassen
            AccessRepository accessRepo = new AccessRepository();
            if (accessRepo.IsTeacher(httpContext.User.Identity.GetUserId()))
            {
                Debug.WriteLine("Vi är en lärare");
                return true;
            }
            int KlassId;
            bool successfullyParsed = int.TryParse(KlassIdFromUrl, out KlassId);
            if (!successfullyParsed)
            {
                return false;
            }

            Debug.WriteLine("Parsed to: {0}", KlassId);

            bool UserIsInKlass = accessRepo.UserAttendsKlass(httpContext.User.Identity.GetUserId(), KlassId);

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
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary( new { controller = "Error", action = "NotYourKlass" }));
        }
    }
}