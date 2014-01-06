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
		private int baseAttactListId = (int)ATAConstants.cmdidAttachToAnyDynamicStart;

		public MenuBuilder ( GeneralOptionsPage optionsPage ) {
			OptionsPage = optionsPage;
		}

		private GeneralOptionsPage OptionsPage { get; set; }
		public void BuildMenuItems ( OleMenuCommandService mcs ) {
			if ( OptionsPage.Attachables == null ) {
				Debug.WriteLine ( "ATA: No Attachables found." );
				throw new ArgumentNullException ( "attachables" );
			}
			var items = OptionsPage.Attachables.Where ( f => f.Enabled );
			if ( items != null ) {
				var count = items.Count ( );
				for ( var i = 0; i < count; ++i ) {
					int id = baseAttactListId + i;
					AddAttachCommand ( mcs, id, x => x.Attachables[i] );
				}
			} 
		}

		/// <summary>
		/// Adds the attach command.
		/// </summary>
		/// <param name="mcs">The Menu Command Service.</param>
		/// <param name="getDescriptor">The get descriptor.</param>
		private void AddAttachCommand ( OleMenuCommandService mcs, int commandId, Func<GeneralOptionsPage, AttachDescriptor> getDescriptor ) {
			if ( mcs != null ) {
				var commandIdentifier = new CommandID ( ATAGuids.guidAttachToAnyCmdSet, commandId );
				if ( mcs.FindCommand ( commandIdentifier ) != null ) {
					return;
				}
				var descriptor = getDescriptor ( OptionsPage );
				var menuItem = new OleMenuCommand (
					delegate ( object s, EventArgs e ) {
						if ( OptionsPage.DTE != null ) {
							// todo: multiple matches dialog.
							foreach ( EnvDTE.Process process in OptionsPage.DTE.Debugger.LocalProcesses ) {
								if ( descriptor.ProcessNames.Any ( p => process.Name.EndsWith ( p ) ) ) {
									process.Attach ( );
								}
							}
						} else {
							Debug.WriteLine ( "DTE is NULL" );
						}
					},
					commandIdentifier,
					 String.Format ( "{0}{1}", descriptor.PrependAttachTo ? "Attach To " : "", descriptor.ToString ( ) )
				);
				menuItem.BeforeQueryStatus += ( s, e ) => {
					menuItem.Visible = descriptor.Enabled && descriptor.ProcessNames.Count ( ) > 0;
					menuItem.Text = String.Format ( "{0}{1}", descriptor.PrependAttachTo ? "Attach To " : "", descriptor.ToString ( ) );
				};
				mcs.AddCommand ( menuItem );
			}
		}
	}
}
