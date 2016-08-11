using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models {
	public class Klass {
		[Key]
		public int ID { get; set; }

		public string Name { get; set; }

		//public virtual Schedule Schedule { get; set; }
		//private virtual ICollection<FileObject> Files { get; set; }
		//public ICollection<FileObject> Shared { get; set; }
		//public ICollection<FileObject> Submission { get; set; }

		public virtual ICollection<ApplicationUser> Members { get; set; }
	}
}