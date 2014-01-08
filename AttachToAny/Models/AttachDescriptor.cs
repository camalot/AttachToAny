using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using RyanConrad.AttachToAny.Components;

namespace RyanConrad.AttachToAny.Models {
	[DisplayName("Attach Descriptor")]
	public class AttachDescriptor {
		public AttachDescriptor ( ) {
			Enabled = true;
			ProcessNames = new List<string> ( );
			ChooseProcess = false;
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
		[LocDisplayName("Process")]
		public IEnumerable<string> ProcessNames { get; set; }
		[DefaultValue ( true )]
		[Category ( "General" )]
		[Description("Enable/Disable this menu item.")]
		public bool Enabled { get; set; }
		[Category ( "General" )]
		[LocDisplayName ( "Choose which Process" )]
		[DisplayName("Choose which Process")]
		[Description ( "Where there are multiple instances of a process, show a dialog that will allow you to choose which process to attach to. Setting to false will use a 'best guess' on which process to attach to." )]
		[DefaultValue ( false )]
		public bool ChooseProcess { get; set; }


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
