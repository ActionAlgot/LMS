using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models {
	public class Klass {
		[Key]
		public int ID { get; set; }

		public string Name { get; set; }

		//public virtual Schedule Schedule { get; set; }
		//private virtual ICollection<FileObject> Files { get; set; }

		public ICollection<SharedFile> Shared { get; set; }
		public ICollection<SubmissionFile> Submission { get; set; }
		
		public virtual ICollection<ApplicationUser> Members { get; set; }
	}
}