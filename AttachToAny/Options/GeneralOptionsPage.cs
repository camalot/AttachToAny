using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using RyanConrad.AttachToAny.Components;

namespace RyanConrad.AttachToAny.Options {
	public class GeneralOptionsPage : DialogPage {
		public GeneralOptionsPage ( ) {
			Attachables = new List<AttachDescriptor> {
				new AttachDescriptor {
					Name = "IIS",
					ProcessNames = new string[] { "wp3.exe" }
				},

				new AttachDescriptor {
					Name = "IIS Express",
					ProcessNames = new string[] { "iisexpress.exe" }
				},

				new AttachDescriptor {
					Name = "NUnit",
					ProcessNames = new string[] { "nunit-agent.exe", "nunit.exe", "nunit-console.exe", "nunit-agent-x86.exe", "nunit-x86.exe", "nunit-console-x86.exe" }
				}
			};
		}

		[Editor ( typeof ( CollectionEditor<AttachDescriptor> ), typeof ( UITypeEditor ) )]
		[TypeConverter ( typeof ( IListTypeConverter ) )]
		public List<AttachDescriptor> Attachables { get; set; }

		protected override void OnApply ( PageApplyEventArgs e ) {
			if ( e.ApplyBehavior == ApplyKind.Apply ) {
				var menuBuilder = new MenuBuilder ( this );
				var mcs = GetService ( typeof ( IMenuCommandService ) ) as OleMenuCommandService;
				menuBuilder.BuildMenuItems ( mcs );
			}
			base.OnApply ( e );

		}

		protected override void OnClosed ( EventArgs e ) {
			base.OnClosed ( e );
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public DTE DTE {
			get {
				return this.GetService ( typeof ( DTE ) ) as DTE;
			}
		}
	}
}
