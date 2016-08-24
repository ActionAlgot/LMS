using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models {
	public class CommentingViewModel {
		public Comment Comment { get; set; }
		public int SubmissionID { get; set; }
	}
}