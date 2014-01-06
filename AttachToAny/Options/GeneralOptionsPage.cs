using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;
using RyanConrad.AttachToAny.Components;
using RyanConrad.AttachToAny.Extensions;

namespace RyanConrad.AttachToAny.Options {
	public class GeneralOptionsPage : DialogPage {


		public GeneralOptionsPage ( ) {

		}

		public event EventHandler<EventArgs> SettingsLoaded;

		[Editor ( typeof ( CollectionEditor<AttachDescriptor> ), typeof ( UITypeEditor ) )]
		[TypeConverter ( typeof ( IListTypeConverter ) )]
		[Category ( "Attach To Any" )]
		[Description ( "The items that can be used to attach to processes for debugging." )]
		public ReadOnlyCollection<AttachDescriptor> Attachables { get; set; }

		protected override void OnApply ( PageApplyEventArgs e ) {
			if ( e.ApplyBehavior == ApplyKind.Apply ) {
				LoadSettingsFromStorage ( );
			}
			base.OnApply ( e );
		}

		protected override void OnClosed ( EventArgs e ) {
			base.OnClosed ( e );
		}

		public override void LoadSettingsFromStorage ( ) {
			var items = new List<AttachDescriptor> ( );
			try {
				var package = GetServiceSafe<AttachToAnyPackage> ();
				Debug.Assert ( package != null, "No package service; we cannot load settings" );
				if ( package != null ) {
					using ( RegistryKey rootKey = package.UserRegistryRoot ) {

						string path = this.SettingsRegistryPath;
						object automationObject = this.AutomationObject;

						RegistryKey key = rootKey.OpenSubKey ( path, false /* writable */);
						if ( key != null ) {
							using ( key ) {

								for ( int i = 0; i < ATAConstants.MaxCommands; i++ ) {
									if ( key.GetValueNames ( ).Any ( x => x.Equals ( ATASettings.Keys.AttachDescriptorName.With ( i ) ) ) ) {
										items.Add ( new AttachDescriptor {
											Name = (string)key.GetValue ( ATASettings.Keys.AttachDescriptorName.With ( i ) ),
											Enabled = bool.Parse ( (string)key.GetValue ( ATASettings.Keys.AttachDescriptorEnabled.With ( i ) ) ),
											ProcessNames = ( (string)key.GetValue ( ATASettings.Keys.AttachDescriptorProcessNames.With ( i ) ) )
														.Split ( new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries )
										} );
									} else {
										// add an empty one
										items.Add ( new AttachDescriptor ( ) );
									}
								}
								
							}
						}
					}
				}

			} catch ( Exception ex) {
				Debug.WriteLine ( ex.ToString ( ) );
			}
			if ( items.Count == 0 ) {
				items = ATASettings.DefaultAttachables ( );
			}

			Attachables = new ReadOnlyCollection<AttachDescriptor> ( items );
			OnSettingsLoaded ( EventArgs.Empty );
			base.LoadSettingsFromStorage ( );

		}

		public override void LoadSettingsFromXml ( Microsoft.VisualStudio.Shell.Interop.IVsSettingsReader reader ) {
			base.LoadSettingsFromXml ( reader );
		}

		public override void SaveSettingsToStorage ( ) {
			var package = GetServiceSafe<AttachToAnyPackage> ( );
			Debug.Assert ( package != null, "No package service; we cannot load settings" );
			if ( package != null ) {
				using ( RegistryKey rootKey = package.UserRegistryRoot ) {

					string path = SettingsRegistryPath;
					object automationObject = this.AutomationObject;
					RegistryKey key = rootKey.OpenSubKey ( path, true /* writable */);
					if ( key == null ) {
						key = rootKey.CreateSubKey ( path );
					}

					using ( key ) {
						for ( int i = 0; i < ATAConstants.MaxCommands; i++ ) {
							AttachDescriptor item;
							if ( i >= Attachables.Count ) {
								item = new AttachDescriptor ( );
							} else {
								item = Attachables[i];
							}
							if ( String.IsNullOrWhiteSpace ( item.Name ) && item.ProcessNames.Count ( ) == 0 ) {
								// this should remove "cleared" items
								key.DeleteValue ( ATASettings.Keys.AttachDescriptorName.With ( i ), false );
								key.DeleteValue ( ATASettings.Keys.AttachDescriptorEnabled.With ( i ), false );
								key.DeleteValue ( ATASettings.Keys.AttachDescriptorProcessNames.With ( i ), false );
							} else {
								key.SetValue ( ATASettings.Keys.AttachDescriptorName.With ( i ), item.Name );
								key.SetValue ( ATASettings.Keys.AttachDescriptorEnabled.With ( i ), item.Enabled.ToString ( ).ToLowerInvariant ( ) );
								key.SetValue ( ATASettings.Keys.AttachDescriptorProcessNames.With ( i ), String.Join ( ";", item.ProcessNames ) );
							}
						}
					}
				}
			}
			base.SaveSettingsToStorage ( );

		}

		public override void SaveSettingsToXml ( Microsoft.VisualStudio.Shell.Interop.IVsSettingsWriter writer ) {
			base.SaveSettingsToXml ( writer );
		}

		[EditorBrowsable ( EditorBrowsableState.Never )]
		[Browsable ( false )]
		public DTE DTE {
			get {
				return this.GetService ( typeof ( DTE ) ) as DTE;
			}
		}

		void OnSettingsLoaded ( EventArgs args ) {
			if ( SettingsLoaded != null ) {
				SettingsLoaded ( this, args );
			}
		}

		private T GetServiceSafe<T> ( ) where T : Package {
			try {
				return (T)this.GetService ( typeof ( T ) );
			} catch ( Exception ) {
				return default ( T );
			}
		}
	}
}
