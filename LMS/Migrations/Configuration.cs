namespace LMS.Migrations
{
	using LMS.Models;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LMS.Models.ApplicationDbContext context){

			var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(context));
			if (!roleManager.RoleExists("Teacher")) {
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Teacher";
				roleManager.Create(role);
			}
			if (!roleManager.RoleExists("Student")) {
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Student";
				roleManager.Create(role);
			}

			UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(context));

			var sUser1 = new ApplicationUser { UserName = "test1@test.com", Email = "test1@test.com" };
			var sUser2 = new ApplicationUser { UserName = "test2@test.com", Email = "test2@test.com" };

			var result1 = userManager.Create(sUser1, "Password@123");
			var result2 = userManager.Create(sUser2, "Password@123");
			if (result1.Succeeded) {
				var user = userManager.FindByName(sUser1.UserName);
				userManager.AddToRole(user.Id, "Teacher");
			}

			if (result2.Succeeded) {
				var user = userManager.FindByName(sUser2.UserName);
				userManager.AddToRole(user.Id, "Student");
			}
        }
    }
}
