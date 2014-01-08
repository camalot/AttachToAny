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
using RyanConrad.AttachToAny.Components;
using RyanConrad.AttachToAny.Dialog;
using RyanConrad.AttachToAny.Models;
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
				var commandIdentifier = new CommandID ( ATAGuids.guidAttachToAnyCmdGroup, commandId );
				var existing = mcs.FindCommand(commandIdentifier);
				var descriptor = getDescriptor ( OptionsPage );
				if ( existing != null ) {
					( (DescriptorMenuCommand)existing ).Descriptor = descriptor;
					return;
				}
				var menuItem = new DescriptorMenuCommand (
					( s, e ) => {
						var menu = (DescriptorMenuCommand)s;
						if ( OptionsPage.DTE != null ) {
							var procList = new List<EnvDTE.Process> ( );

							foreach ( EnvDTE.Process process in OptionsPage.DTE.Debugger.LocalProcesses ) {
								if ( menu.Descriptor.ProcessNames.Any ( p => process.Name.EndsWith ( p ) ) ) {
									procList.Add ( process );
								}
							}

							if ( procList.Count == 0 ) {
								return;
							}

							// Where there is only 1, or "best choice"
							if ( procList.Count == 1 || !menu.Descriptor.ChooseProcess ) {
								procList.First ( ).Attach ( );
								return;
							}

							AttachToAnyPackage.ShowProcessManagerDialog ( procList );
						}
					}, commandId, descriptor );
				mcs.AddCommand ( menuItem );
			}
		}
	}
}
