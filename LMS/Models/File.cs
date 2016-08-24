using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models {
	public abstract class File {
		public File() { }
		[Key]
		public int ID { get; set; }

		public string FileName { get; set; }
		public string ContentType { get; set; }
		public Byte[] Content { get; set; }
		
		public string UploaderID { get; set; }
		[ForeignKey("UploaderID")]
		public virtual ApplicationUser Uploader { get; set; }

		public int KlassID { get; set; }
		[ForeignKey("KlassID")]
		public virtual Klass Klass { get; set; }
	}

	public class SharedFile : File { }

	public class SubmissionFile : File {
		public virtual ICollection<Comment> Comments {get; set; }
	}
}