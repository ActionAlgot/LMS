namespace LMS.Migrations
{
	using LMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
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

			var studs = new List<ApplicationUser>(){
				new ApplicationUser { UserName = "test2@test.com", Email = "test2@test.com", LastName = "Andersson", FirstName = "Bengt", PhoneNumber ="070-1234567"},
				new ApplicationUser { UserName = "stud2@test.com", Email = "stud2@test.com", LastName = "Bengtsson", FirstName = "Sandra", PhoneNumber ="070-1234568"},
				new ApplicationUser { UserName = "stud3@test.com", Email = "stud3@test.com", LastName = "Dahl", FirstName = "Lena", PhoneNumber ="070-1234569"},
				new ApplicationUser { UserName = "stud4@test.com", Email = "stud4@test.com", LastName = "Nilsson", FirstName = "Magnus", PhoneNumber ="070-1234570"},
				new ApplicationUser { UserName = "stud5@test.com", Email = "stud5@test.com", LastName = "Steen", FirstName = "Fredrik", PhoneNumber ="070-1234571"},
				new ApplicationUser { UserName = "stud6@test.com", Email = "stud6@test.com", LastName = "Åkerberg", FirstName = "Annika", PhoneNumber ="070-1234572"},
				new ApplicationUser { UserName = "stud7@test.com", Email = "stud7@test.com", LastName = "Hansson", FirstName = "Elin", PhoneNumber ="070-1234573"},
				new ApplicationUser { UserName = "stud8@test.com", Email = "stud8@test.com", LastName = "Miller", FirstName = "Petter", PhoneNumber ="070-1234574"}
			};

			var techs = new List<ApplicationUser>(){
				new ApplicationUser { UserName = "test1@test.com", Email = "test1@test.com", LastName = "Bergdahl", FirstName = "Calle", PhoneNumber ="070-1234575"},
				new ApplicationUser { UserName = "teach2@test.com", Email = "teach2@test.com", LastName = "Mickelsson", FirstName = "Margareta", PhoneNumber ="070-1234576"},
				new ApplicationUser { UserName = "teach3@test.com", Email = "teach3@test.com", LastName = "Österberg", FirstName = "Monica", PhoneNumber ="070-1234578"}
			};

			foreach(var t in techs){
				var result = userManager.Create(t, "Password@123");
				if (result.Succeeded) {
					var user = userManager.FindByName(t.UserName);
					userManager.AddToRole(user.Id, "Teacher");
				}
			}

			foreach(var s in studs){
				var result = userManager.Create(s, "Password@123");
				if (result.Succeeded) {
					var user = userManager.FindByName(s.UserName);
					userManager.AddToRole(user.Id, "Student");
				}
			}

			var klasses = new List<Klass>() {
				new Klass{Name = "k1", Members = new List<ApplicationUser>(){
					userManager.FindByName(techs[0].UserName),
					userManager.FindByName(studs[2].UserName),
					userManager.FindByName(studs[0].UserName),
					userManager.FindByName(studs[5].UserName),
					userManager.FindByName(techs[1].UserName),
					userManager.FindByName(studs[7].UserName)
				}},
				new Klass{Name = "k2", Members = new List<ApplicationUser>(){
					userManager.FindByName(techs[1].UserName),
					userManager.FindByName(studs[2].UserName),
					userManager.FindByName(studs[7].UserName)
				}},
				new Klass{Name = "k3", Members = new List<ApplicationUser>(){
					userManager.FindByName(studs[4].UserName),
					userManager.FindByName(studs[0].UserName),
					userManager.FindByName(studs[3].UserName),
					userManager.FindByName(studs[7].UserName)
				}}
			};

			foreach (var k in klasses) context.Klasses.AddOrUpdate(k);

			context.SaveChanges();
        }
    }
}
