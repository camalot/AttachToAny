using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RyanConrad.AttachToAny.Components;

namespace RyanConrad.AttachToAny {
	[DisplayName("Attach Descriptor")]
	public class AttachDescriptor {
		public AttachDescriptor ( ) {
			Enabled = true;
			ProcessNames = new List<string> ( );
			PrependAttachTo = true;
		}

		[DisplayName ( "(Name)" )]
		[Category ( "General" )]
		[Description("The name of the command.")]
		public String Name { get; set; }
		[DisplayName("Processes")]
		[Category ( "General" )]
		[Editor ( typeof ( StringListUIEditor ), typeof ( UITypeEditor ) )]
		[TypeConverter ( typeof ( StringListTypeConverter ) )]
		[Description("A list of process names to look for. It will attach to the first one that is found.")]
		public IEnumerable<string> ProcessNames { get; set; }
		[DefaultValue ( true )]
		[Category ( "General" )]
		[Description("Enable/Disable this menu item.")]
		public bool Enabled { get; set; }
		[DefaultValue ( true )]
		[Category ( "General" )]
		[Description ( "Should 'Attach To' preface the item name in the menu?" )]
		public bool PrependAttachTo { get; set; }

		public override string ToString ( ) {
			var text =	String.IsNullOrWhiteSpace ( Name ) ? 
					( ProcessNames == null || ProcessNames.Count() == 0 ? 
							"[Unused]" :
							String.Join(",", ProcessNames)
					)
					:  Name;
			return text;
		}
	}
}
