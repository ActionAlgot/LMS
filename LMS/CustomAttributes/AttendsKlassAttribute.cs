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

    public class AttendsKlassAttribute : AuthorizeAttribute
    {
        //custom
        //public int KlassIDParamPos { get; set; }
        public string ParamFrom { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            //Kolla om vi är lärare - fler tester behövs inte då
            AccessRepository accessRepo = new AccessRepository();
            if (accessRepo.IsTeacher(httpContext.User.Identity.GetUserId()))
            {
                Debug.WriteLine("Vi är en lärare så vi skiter i alltihopa");
                return true;
            }

            //var init
            int paramPos = 0;
            string strKlassId;

            //Kan vi parsa ParamFrom till en int så vill vi plocka ur routen
            bool intParsable = int.TryParse(ParamFrom, out paramPos);
            if (intParsable)
            {
                //ex: /Klass/Details/{id} splitta URL så vi har varje del i en array - ex: /Klass/Details/3 blir { 'Klass', 'Details', '3' }
                var requestedAdress = httpContext.Request.RawUrl;
                char[] charsToTrim = { '/', ' ' };
                string trimmedAdress = requestedAdress.Trim(charsToTrim);
                char[] delimiter = { '/' };
                string[] adressParts = trimmedAdress.Split(delimiter);
                if (paramPos >= adressParts.Count()) return false; //Fick ingen giltig parampos - defaulta till false
                strKlassId = adressParts[paramPos];
            }
            else
            {
                //ex: SharedFiles?KlassID={id} 
                throw new NotImplementedException();
            }

            //Förvandla KlassId till en int
            int KlassId;
            bool successfullyParsed = int.TryParse(strKlassId, out KlassId);
            if (!successfullyParsed)
            {
                return false; //Vi kunde inte parsa den till en int - defaulta till false
            }

            Debug.WriteLine("Den färdiga parametern är {0}", KlassId);

            //Kontrollera nu om studenten går i den här klassen
            bool UserIsInKlass = accessRepo.UserAttendsKlass(httpContext.User.Identity.GetUserId(), KlassId);
            if (UserIsInKlass)
            {
                    Debug.Write("Ja studenten går i den här klassen!");
                    return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary( new { controller = "Error", action = "NotYourKlass" }));
        }
    }
}