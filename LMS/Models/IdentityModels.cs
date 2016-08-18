using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace LMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
		public virtual ICollection<Klass> Klasses { get; set; }
		public virtual ICollection<SubmissionFile> SubmittedFiles { get; set; }
		public virtual ICollection<SharedFile> SharedFiles { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

		public string GetFullName()
		{
			return string.Format("{0} {1}",FirstName,LastName);
		}
	}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }

		public DbSet<Klass> Klasses { get; set; }
		public DbSet<SharedFile> SharedFiles { get; set; }
		public DbSet<SubmissionFile> SubmissionFiles { get; set; }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }
    }
}