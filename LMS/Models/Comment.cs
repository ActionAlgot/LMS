using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models {
	public class Comment {
		[Key]
		public int ID { get; set; }

		public string Text { get; set; }

		public string CommenterID { get; set; }
		public int SubmissionID { get; set; }

		[ForeignKey("CommenterID")]
		public virtual ApplicationUser Commenter { get; set; }
		[ForeignKey("SubmissionID")]
		public virtual SubmissionFile Submission { get; set; }
	}
}