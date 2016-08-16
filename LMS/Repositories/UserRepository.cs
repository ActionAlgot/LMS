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
            return users.OrderBy(o => o.LastName).ToList();
        }

		//Lista alla elever
		public List<UserViewModel> GetAllStudents()
		{
			//Flytta den data vi vill ha från usermanager till en viewmodel.
			List<UserViewModel> users = new List<UserViewModel>();
			var userList = UserManager.Users.ToList().Where(u => UserManager.IsInRole(u.Id,"Student")).ToList<ApplicationUser>();
			foreach (ApplicationUser AppUser in userList)
			{
				var user = new UserViewModel
				{
					Id = AppUser.Id,
					FirstName = AppUser.FirstName,
					LastName = AppUser.LastName,
					Email = AppUser.Email,
					PhoneNumber = AppUser.PhoneNumber,
					UserName = AppUser.UserName
				};
				users.Add(user);
			}
			return users.OrderBy(o => o.LastName).ToList();
		}

        public bool CreateNewUser(UserViewModel newUser)
        {
            var user = new ApplicationUser {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                UserName = newUser.Email,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
				PasswordHash = UserManager.PasswordHasher.HashPassword(newUser.NewPassword)
            };
			//todo kolla att create lyckas
			//todo sätt rollen till student eller teacher
			//todo token för att tvinga passwordbyte vid första login - wish bara
			//todo varför blir lockedout enabled ?
            var result = UserManager.Create(user);
            if(result.Succeeded)
            {
                UserManager.AddToRole(user.Id, newUser.Roles);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        //Hämta fram en specifik elev
        public UserViewModel GetSpecific(string Id)
        {
            var AppUser = UserManager.Users.SingleOrDefault(k => k.Id == Id);
            var user = new UserViewModel
            {
                FirstName = AppUser.FirstName,
                LastName = AppUser.LastName,
                PhoneNumber = AppUser.PhoneNumber,
                Email = AppUser.Email,
                UserName = AppUser.UserName
            };

            return user;
        }

        //Ta bort en elev
        public void Remove(string Id)
        {
            //Här ska vi ta bort en användare
            //ctx.SaveChanges();
        }

        //Uppdatera en elev
        public void Update(ApplicationUser user)
        {
            //Här ska vi kunna uppdatera en användares information
            //ctx.SaveChanges();
        }
    }
}