using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Models
{
	public class UploadFileViewModel
	{
		public string Name { get; set; }

		public string ID { get; set; }

		public IEnumerable<SharedFile> Files { get; set; }

		public int SelectedKlassId { get; set; }
		public IEnumerable<SelectListItem> KlassList { get; set; }
	}
}