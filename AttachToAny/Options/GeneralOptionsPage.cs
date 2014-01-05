using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
			var items = CreateInitList ( );
			Attachables = new ReadOnlyCollection<AttachDescriptor> ( items );
		}

		private IList<AttachDescriptor> CreateInitList ( ) {
			var list = new List<AttachDescriptor> ( (int)ATAConstants.MaxCommands )  {
				new AttachDescriptor {
					Name = "IIS",
					ProcessNames = new string[] { "wp3.exe" },
					IsRemovable = false
				},

				new AttachDescriptor {
					Name = "IIS Express",
					ProcessNames = new string[] { "iisexpress.exe" },
					IsRemovable = false
				},

				new AttachDescriptor {
					Name = "NUnit",
					ProcessNames = new string[] { "nunit-agent.exe", "nunit.exe", "nunit-console.exe", "nunit-agent-x86.exe", "nunit-x86.exe", "nunit-console-x86.exe" },
					IsRemovable = false
				}
			};
			var initCount = list.Count;
			for ( int i = initCount; i < (int)ATAConstants.MaxCommands; ++i ) {
				list.Add ( new AttachDescriptor ( ) );
			}
			return list;
		}

		[Editor ( typeof ( CollectionEditor<AttachDescriptor> ), typeof ( UITypeEditor ) )]
		[TypeConverter ( typeof ( IListTypeConverter ) )]
		[Category("Attach To Any")]
		[Description("The items that can be used to attach to processes for debugging.")]
		public ReadOnlyCollection<AttachDescriptor> Attachables { get; set; }

		protected override void OnApply ( PageApplyEventArgs e ) {
			if ( e.ApplyBehavior == ApplyKind.Apply ) {
			}
			base.OnApply ( e );
		}

		protected override void OnClosed ( EventArgs e ) {
			base.OnClosed ( e );
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		[Browsable ( false )]
		public DTE DTE {
			get {
				return this.GetService ( typeof ( DTE ) ) as DTE;
			}
		}
	}
}
