using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Models {
	public class Klass {
		[Key]
		public int ID { get; set; }

		public string Name { get; set; }

		public virtual KlassSchedule Schedule { get; set; }
		public virtual ICollection<SharedFile> Shared { get; set; }
		public virtual ICollection<SubmissionFile> Submission { get; set; }
		
		public virtual ICollection<ApplicationUser> Members { get; set; }

	}

}