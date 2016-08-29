using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Telephone")]
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
		[Display (Name = "Password")]
		public string NewPassword { get; set; }
        public string Roles { get; set; }

		public UserViewModel() { }
		public UserViewModel(ApplicationUser user) {
			Id = user.Id;
			FirstName = user.FirstName;
			LastName = user.LastName;
			Email = user.Email;
			PhoneNumber = user.PhoneNumber;
			UserName = user.UserName;
			//Roles = user.Roles.ToString();
		}
    }
}