using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models {
	public class KlassDetailsViewModel {
		public Klass Klass;
		public IEnumerable<ApplicationUser> NonMembers;

		public KlassDetailsViewModel(Klass klass, IEnumerable<ApplicationUser> nonMembers) {
			Klass = klass;
			NonMembers = nonMembers;
		}
	}
}