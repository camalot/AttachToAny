using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using RyanConrad.AttachToAny.Options;

namespace RyanConrad.AttachToAny {
	class MenuBuilder {
		private int baseAttactListId = (int)PkgCmdIDList.cmdidAttachList;

		public MenuBuilder ( GeneralOptionsPage optionsPage ) {
			OptionsPage = optionsPage;
		}

		private GeneralOptionsPage OptionsPage { get; set; }
		public void BuildMenuItems ( OleMenuCommandService mcs ) {
			var items = OptionsPage.Attachables.Where ( f => f.Enabled );

			ClearItems ( mcs );


			if ( items != null ) {
				var count = items.Count ( );
				for ( var i = 0; i < count; ++i ) {
					uint id = (uint)( ( baseAttactListId + i ) );
					AddAttachCommand ( mcs, id, x => x.Attachables[i] );
				}
			} else {
				AddNoneCommand ( mcs );
			}
		}

		private void ClearItems ( OleMenuCommandService mcs ) {
			var found = false;
			var idx = 0;
			do {
				CommandID cid = new CommandID ( GuidList.guidAttachToAnyCmdSet, baseAttactListId + idx );
				var cmd = mcs.FindCommand ( cid );
				found = cmd != null;
				if ( found ) {
					Debug.WriteLine ( "Remove: {0}", cmd.CommandID.ID );
					mcs.RemoveCommand ( cmd );
				}
				++idx;
			} while ( found );

		}

		/// <summary>
		/// Adds the attach command.
		/// </summary>
		/// <param name="mcs">The MCS.</param>
		/// <param name="getDescriptor">The get descriptor.</param>
		private void AddAttachCommand ( OleMenuCommandService mcs, uint commandId, Func<GeneralOptionsPage, AttachDescriptor> getDescriptor ) {
			var descriptor = getDescriptor ( OptionsPage );
			var menuItem = new OleMenuCommand (
				delegate ( object s, EventArgs e ) {
					if ( OptionsPage.DTE != null ) {
						foreach ( EnvDTE.Process process in OptionsPage.DTE.Debugger.LocalProcesses ) {
							if ( descriptor.ProcessNames.Any ( p => process.Name.EndsWith ( p ) ) ) {
								process.Attach ( );
							}
						}
					} else {
						Debug.WriteLine ( "DTE is NULL" );
					}
				},
				new CommandID ( GuidList.guidAttachToAnyCmdSet, (int)commandId ),
				descriptor.ToString ( )
			);
			menuItem.BeforeQueryStatus += ( s, e ) => {
				menuItem.Visible = descriptor.Enabled;
			};

			mcs.AddCommand ( menuItem );
		}

		private void AddNoneCommand ( OleMenuCommandService mcs ) {
			var descriptor = new AttachDescriptor {
				Enabled = true,
				Name = "None",
				ProcessNames = new string[] { "none-here-to.attach" }
			};
			var menuItem = new OleMenuCommand (
				delegate ( object s, EventArgs e ) {

				},
				new CommandID ( GuidList.guidAttachToAnyCmdSet, (int)baseAttactListId ),
				descriptor.ToString ( )
			);
			menuItem.BeforeQueryStatus += ( s, e ) => {
				menuItem.Enabled = false;
			};

			mcs.AddCommand ( menuItem );
		}
	}
}
