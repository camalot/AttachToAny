using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanConrad.AttachToAny.Extensions {
	public static partial class AttachToAnyExtensions {
		public static String With ( this String format, params object[] args ) {
			return String.Format ( format, args );
		}
	}
}
