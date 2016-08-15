using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace LMS.Repositories
{
    public class UserRepository
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public UserRepository()
        {
        }

        public UserRepository(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        //Lista alla elever
        public List<UserViewModel> GetAll()
        {
            //Flytta den data vi vill ha från usermanager till en viewmodel.
            List<UserViewModel> users = new List<UserViewModel>();
            var userList = UserManager.Users.ToList<ApplicationUser>();
            foreach (ApplicationUser AppUser in userList)
            {
                var user = new UserViewModel {
                    Id = AppUser.Id,
                    FirstName = AppUser.FirstName,
                    LastName = AppUser.LastName,
                    Email = AppUser.Email,
                    PhoneNumber = AppUser.PhoneNumber,
                    UserName = AppUser.UserName
                };
                users.Add(user);
            }
            return users;
        }

        public bool CreateNewUser(UserViewModel newUser)
        {
            var user = new ApplicationUser {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                UserName = newUser.Email,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber
            };

            var result = UserManager.Create(user);/*, model.Password);*/
            ctx.SaveChanges();
            return true;
        }


        //Hämta fram en specifik elev
        /*public ApplicationUser GetSpecific(int Id)
        {
            //todo: hämta en user med en annan metod
            return ctx.Users.SingleOrDefault(k => k.ID == Id);
        }*/




        //Lägg till en elev
        public void Add(ApplicationUser user)
        {
            //ctx.Klasses.Add(klass);
            //ctx.SaveChanges();
        }

        //Ta bort en elev
        public void Remove(ApplicationUser user)
        {
            //ctx.Klasses.Add(klass);
            //ctx.SaveChanges();
        }

        //Uppdatera en elev
        public void Update(ApplicationUser user)
        {
            //ctx.Klasses.Add(klass);
            //ctx.SaveChanges();
        }
    }
}