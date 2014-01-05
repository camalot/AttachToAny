using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RyanConrad.AttachToAny.Components;

namespace RyanConrad.AttachToAny {
	public class AttachDescriptor {
		public AttachDescriptor ( ) {
			Enabled = true;
			ProcessNames = new List<string> ( );
		}


		[DisplayName ( "(Name)" )]
		[Category ( "General" )]
		public String Name { get; set; }
		[DisplayName("Processes")]
		[Category ( "General" )]
		[Editor ( typeof ( StringListUIEditor ), typeof ( UITypeEditor ) )]
		[TypeConverter ( typeof ( StringListTypeConverter ) )]
		public IEnumerable<string> ProcessNames { get; set; }
		[DefaultValue ( true )]
		[Category ( "General" )]
		public bool Enabled { get; set; }


		public override string ToString ( ) {
			return	String.IsNullOrWhiteSpace ( Name ) ? 
					( ProcessNames == null || ProcessNames.Count() == 0 ? 
							"Unnamed" :
							String.Join(",", ProcessNames)
					)
				: Name;
		}
	}
}
