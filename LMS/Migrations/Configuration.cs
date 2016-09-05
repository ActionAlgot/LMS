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
	using System.Web;

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
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(context));

            var admins = new List<ApplicationUser>(){
                new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com", LastName = "Administratör", FirstName = "(ej namngiven)", PhoneNumber ="070-3416061" }
            };

            var studs = new List<ApplicationUser>(){
				new ApplicationUser { UserName = "test2@test.com", Email = "test2@test.com", LastName = "Andersson", FirstName = "Bengt", PhoneNumber ="070-1234567"},
				new ApplicationUser { UserName = "student@test.com", Email = "stud2@test.com", LastName = "Bengtsson", FirstName = "Sandra", PhoneNumber ="070-1234568"},
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
				new ApplicationUser { UserName = "teacher@test.com", Email = "teach3@test.com", LastName = "Österberg", FirstName = "Monica", PhoneNumber ="070-1234578"}
			};

            foreach (var a in admins)
            {
                var result = userManager.Create(a, "Admin@123");
                if (result.Succeeded)
                {
                    var user = userManager.FindByName(a.UserName);
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            foreach (var t in techs){
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

			List<SharedFile> SharedFilesSeed = new List<SharedFile>(){
				new SharedFile{ ID = 1, FileName = "sharedfile1.txt", ContentType = MimeMapping.GetMimeMapping("sharedfile1.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the quick brown"), Uploader = userManager.FindByName(studs[0].UserName)},
				new SharedFile{ ID = 2, FileName = "sharedfile2.txt", ContentType = MimeMapping.GetMimeMapping("sharedfile2.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the quick brown the lazy"), Uploader = userManager.FindByName(studs[2].UserName)},
				new SharedFile{ ID = 3, FileName = "sharedfile3.txt", ContentType = MimeMapping.GetMimeMapping("sharedfile3.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the third brown"), Uploader = userManager.FindByName(studs[1].UserName)},
				new SharedFile{ ID = 4, FileName = "sharedfile4.txt", ContentType = MimeMapping.GetMimeMapping("sharedfile4.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the fourth brown"), Uploader = userManager.FindByName(studs[1].UserName)},
				new SharedFile{ ID = 5, FileName = "sharedfile5.txt", ContentType = MimeMapping.GetMimeMapping("sharedfile5.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the something brown"), Uploader = userManager.FindByName(studs[0].UserName)},
				new SharedFile{ ID = 6, FileName = "sharedfile6.txt", ContentType = MimeMapping.GetMimeMapping("sharedfile6.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the sixth brown"), Uploader = userManager.FindByName(techs[0].UserName)}
			};

			List<SubmissionFile> SubmissionFilesSeed = new List<SubmissionFile>(){
				new SubmissionFile{ ID = 1, FileName = "assignment1.txt", ContentType = MimeMapping.GetMimeMapping("assignment1.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the quick brown"), Uploader = userManager.FindByName(studs[2].UserName)},
				new SubmissionFile{ ID = 2, FileName = "assignment2.txt", ContentType = MimeMapping.GetMimeMapping("assignment2.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the quick brown"), Uploader = userManager.FindByName(studs[3].UserName)},
				new SubmissionFile{ ID = 3, FileName = "assignment3.txt", ContentType = MimeMapping.GetMimeMapping("assignment3.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the third brown"), Uploader = userManager.FindByName(studs[0].UserName)},
				new SubmissionFile{ ID = 4, FileName = "assignment4.txt", ContentType = MimeMapping.GetMimeMapping("assignment4.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the fourth brown"), Uploader = userManager.FindByName(studs[0].UserName)},
				new SubmissionFile{ ID = 5, FileName = "assignment5.txt", ContentType = MimeMapping.GetMimeMapping("assignment5.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the something brown"), Uploader = userManager.FindByName(studs[1].UserName)},
				new SubmissionFile{ ID = 6, FileName = "assignment6.txt", ContentType = MimeMapping.GetMimeMapping("assignment6.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the sixth brown"), Uploader = userManager.FindByName(studs[1].UserName)}
			};

			var schedules = new List<KlassSchedule>() {
				new KlassSchedule{ Lectures = new List<Lecture>(){
					new Lecture{ID = 1, Start = new DateTime(2016, 9, 5, 8, 0, 0), End = new DateTime(2016, 9, 5, 8, 50, 0), Location = "hell"},
					new Lecture{ID = 2, Start = new DateTime(2016, 9, 5, 9, 30, 0), End = new DateTime(2016, 9, 5, 11, 30, 0), Location = "hell"},
					new Lecture{ID = 3, Start = new DateTime(2016, 9, 5, 13, 50, 0), End = new DateTime(2016, 9, 5, 14, 0, 0), Location = "hell"},
					new Lecture{ID = 4, Start = new DateTime(2016, 9, 6, 14, 5, 0), End = new DateTime(2016, 9, 6, 15, 30, 0), Location = "hell"},
					new Lecture{ID = 33, Start = new DateTime(2016, 9, 6, 14, 50, 0), End = new DateTime(2016, 9, 6, 15, 40, 0), Location = "hell"},
					new Lecture{ID = 34, Start = new DateTime(2016, 9, 6, 14, 5, 0), End = new DateTime(2016, 9, 6, 15, 30, 0), Location = "hell"},
					new Lecture{ID = 5, Start = new DateTime(2016, 9, 6, 15, 30, 0), End = new DateTime(2016, 9, 6, 16, 0, 0), Location = "hell 2.0"},
					new Lecture{ID = 6, Start = new DateTime(2016, 9, 7, 10, 15, 0), End = new DateTime(2016, 9, 7, 11, 30, 0), Location = "hell"},
					new Lecture{ID = 7, Start = new DateTime(2016, 9, 7, 12, 30, 0), End = new DateTime(2016, 9, 7, 13, 45, 0), Location = "hell"},
					new Lecture{ID = 8, Start = new DateTime(2016, 9, 7, 13, 50, 0), End = new DateTime(2016, 9, 7, 14, 0, 0), Location = "hell"},
					new Lecture{ID = 9, Start = new DateTime(2016, 9, 8, 14, 5, 0), End = new DateTime(2016, 9, 8, 15, 30, 0), Location = "hell"},
					new Lecture{ID = 10, Start = new DateTime(2016, 9, 8, 15, 30, 0), End = new DateTime(2016, 9, 8, 16, 0, 0), Location = "hell 2.0"},
					new Lecture{ID = 31, Start = new DateTime(2016, 9, 8, 10, 10, 0), End = new DateTime(2016, 9, 8, 16, 0, 0), Location = "hell 2.0"}
				}},
				new KlassSchedule{ Lectures = new List<Lecture>(){
					new Lecture{ID = 11, Start = new DateTime(2016, 9, 5, 10, 15, 0), End = new DateTime(2016, 9, 5, 11, 30, 0), Location = "hell"},
					new Lecture{ID = 13, Start = new DateTime(2016, 9, 5, 13, 50, 0), End = new DateTime(2016, 9, 5, 14, 0, 0), Location = "hell"},
					new Lecture{ID = 14, Start = new DateTime(2016, 9, 5, 14, 5, 0), End = new DateTime(2016, 9, 5, 15, 30, 0), Location = "hell"},
					new Lecture{ID = 15, Start = new DateTime(2016, 9, 5, 15, 30, 0), End = new DateTime(2016, 9, 5, 16, 0, 0), Location = "hell 2.0"},
					new Lecture{ID = 16, Start = new DateTime(2016, 9, 6, 10, 15, 0), End = new DateTime(2016, 9, 6, 11, 30, 0), Location = "hell"},
					new Lecture{ID = 17, Start = new DateTime(2016, 9, 6, 12, 30, 0), End = new DateTime(2016, 9, 6, 13, 45, 0), Location = "hell"},
					new Lecture{ID = 18, Start = new DateTime(2016, 9, 6, 13, 50, 0), End = new DateTime(2016, 9, 6, 14, 0, 0), Location = "hell"},
					new Lecture{ID = 19, Start = new DateTime(2016, 9, 7, 14, 5, 0), End = new DateTime(2016, 9, 7, 15, 30, 0), Location = "hell"},
					new Lecture{ID = 32, Start = new DateTime(2016, 9, 7, 14, 5, 0), End = new DateTime(2016, 9, 7, 15, 30, 0), Location = "hell"},
					new Lecture{ID = 20, Start = new DateTime(2016, 9, 7, 15, 30, 0), End = new DateTime(2016, 9, 7, 16, 0, 0), Location = "hell 2.0"}
				}},
				new KlassSchedule{ Lectures = new List<Lecture>(){
					new Lecture{ID = 21, Start = new DateTime(2016, 9, 5, 10, 0, 0), End = new DateTime(2016, 9, 5, 13, 30, 0), Location = "hell"},
					new Lecture{ID = 22, Start = new DateTime(2016, 9, 5, 12, 30, 0), End = new DateTime(2016, 9, 5, 13, 45, 0), Location = "hell"},
					new Lecture{ID = 23, Start = new DateTime(2016, 9, 5, 13, 50, 0), End = new DateTime(2016, 9, 5, 14, 0, 0), Location = "hell"},
					new Lecture{ID = 24, Start = new DateTime(2016, 9, 5, 14, 5, 0), End = new DateTime(2016, 9, 5, 15, 30, 0), Location = "hell"},
					new Lecture{ID = 25, Start = new DateTime(2016, 9, 6, 15, 30, 0), End = new DateTime(2016, 9, 6, 16, 0, 0), Location = "hell 2.0"},
					new Lecture{ID = 26, Start = new DateTime(2016, 9, 6, 10, 15, 0), End = new DateTime(2016, 9, 6, 11, 30, 0), Location = "hell"},
					new Lecture{ID = 27, Start = new DateTime(2016, 9, 7, 12, 30, 0), End = new DateTime(2016, 9, 7, 13, 45, 0), Location = "hell"},
					new Lecture{ID = 28, Start = new DateTime(2016, 9, 8, 13, 50, 0), End = new DateTime(2016, 9, 8, 14, 0, 0), Location = "hell"},
					new Lecture{ID = 29, Start = new DateTime(2016, 9, 8, 14, 5, 0), End = new DateTime(2016, 9, 8, 15, 30, 0), Location = "hell"},
					new Lecture{ID = 30, Start = new DateTime(2016, 9, 9, 15, 30, 0), End = new DateTime(2016, 9, 9, 16, 0, 0), Location = "hell 2.0"}
				}}
			};

			var klasses = new List<Klass>() {
				new Klass{ID = 1, Name = "Astronomy",
					Members = new List<ApplicationUser>(){
						userManager.FindByName(techs[0].UserName),
						userManager.FindByName(studs[2].UserName),
                        userManager.FindByName(studs[1].UserName),
                        userManager.FindByName(studs[0].UserName),
						userManager.FindByName(studs[5].UserName),
						userManager.FindByName(techs[1].UserName),
						userManager.FindByName(studs[7].UserName)
					},
					Schedule = schedules[0],
					Shared = new List<SharedFile>(){SharedFilesSeed[1], SharedFilesSeed[4]},
					Submission = new List<SubmissionFile>(){SubmissionFilesSeed[4], SubmissionFilesSeed[3]}
				},
				new Klass{ID = 2, Name = "English",
					Members = new List<ApplicationUser>(){
						userManager.FindByName(techs[1].UserName),
                        userManager.FindByName(studs[1].UserName),
                        userManager.FindByName(studs[2].UserName),
						userManager.FindByName(studs[7].UserName)
					},
					Schedule = schedules[1],
					Shared = new List<SharedFile>(){SharedFilesSeed[2], SharedFilesSeed[0]},
					Submission = new List<SubmissionFile>(){SubmissionFilesSeed[1], SubmissionFilesSeed[5]}
				},
				new Klass{ID = 3, Name = "Algebra",
					Members = new List<ApplicationUser>(){
						userManager.FindByName(studs[4].UserName),
						userManager.FindByName(studs[0].UserName),
                        userManager.FindByName(studs[1].UserName),
                        userManager.FindByName(studs[3].UserName),
						userManager.FindByName(studs[7].UserName),
                        userManager.FindByName(techs[2].UserName)
                    },
					Schedule = schedules[2],
					Shared = new List<SharedFile>(){SharedFilesSeed[3], SharedFilesSeed[5]},
					Submission = new List<SubmissionFile>(){SubmissionFilesSeed[0], SubmissionFilesSeed[2]}
				}
			};

			var comments = new List<Comment>() {
				new Comment{ID = 1, Text = "Well done", Date = new DateTime(2010, 11, 10), Read = false, Commenter = userManager.FindByName(techs[0].UserName), Submission = SubmissionFilesSeed[4]},
				new Comment{ID = 2, Text = "This is awful", Date = new DateTime(1999, 12, 23), Read = false, Commenter = userManager.FindByName(techs[0].UserName), Submission = SubmissionFilesSeed[3]},
				new Comment{ID = 3, Text = "Too short", Date = new DateTime(2001, 1, 9), Read = false, Commenter = userManager.FindByName(techs[1].UserName), Submission = SubmissionFilesSeed[1]},
				new Comment{ID = 4, Text = "Too long", Date = new DateTime(1812, 4, 6), Read = false, Commenter = userManager.FindByName(techs[1].UserName), Submission = SubmissionFilesSeed[5]},
				new Comment{ID = 5, Text = "Good job", Date = new DateTime(2016, 5, 19), Read = false, Commenter = userManager.FindByName(techs[1].UserName), Submission = SubmissionFilesSeed[3]},
			};


			foreach (var k in klasses) context.Klasses.AddOrUpdate(k);
			foreach (var c in comments) context.Comments.AddOrUpdate(c);

			context.SaveChanges();
        }
    }
}
