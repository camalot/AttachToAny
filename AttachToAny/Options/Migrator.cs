using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RyanConrad.AttachToAny.Extensions;

namespace RyanConrad.AttachToAny.Options {

	/// <summary>
	/// This is a "fix" for screw ups...
	/// </summary>
	internal static class Migrator {


		/// <summary>
		/// This fixes my mistake for naming IIS process "wp3.exe" and not "w3wp.exe" like it should be.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="descriptorIndex"></param>
		internal static void IISFix ( Microsoft.Win32.RegistryKey key, int descriptorIndex ) {
			try {
				// get the name.
				var name = (string)key.GetValue ( ATASettings.Keys.AttachDescriptorName.With ( descriptorIndex ) );
				var processGroup = ATASettings.Keys.AttachDescriptorProcessNames.With ( descriptorIndex );
				var allProcesses = ( (string)key.GetValue ( processGroup ) )
														.Split ( new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries );
				// does it have the fucked up process name?
				var hasWp3 = allProcesses.Any ( s => string.Compare ( s, "wp3.exe", true ) == 0 );
				// if it is iis, and it has the wrong process, fix that shit.
				if ( string.Compare ( name, "iis", true ) == 0 && hasWp3 ) {
					var newList = allProcesses.Where ( s => string.Compare ( s, "wp3.exe", true ) != 0 ).Concat ( new string[] { "w3wp.exe" } );
					key.SetValue ( processGroup, string.Join ( ";", newList ) );
				}
			} catch ( Exception ) {
				// if we dont have access to write, there is a problem.
			}
		}
	}
}
