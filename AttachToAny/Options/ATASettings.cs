using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RyanConrad.AttachToAny.Extensions;

namespace RyanConrad.AttachToAny.Options {
	static class ATASettings {

		public static class Keys {
			public const String AttachDescriptorName = "AttachDescriptorName{0}";
			public const String AttachDescriptorProcessNames = "AttachDescriptorProcessNames{0}";
			public const String AttachDescriptorEnabled = "AttachDescriptorEnabled{0}";
			public const String AttachDescriptorPrependAttachTo = "AttachDescriptorPrependAttachTo{0}";
		}

		public static List<AttachDescriptor> DefaultAttachables ( ) {
			var items = new List<AttachDescriptor> ( ) {
					new AttachDescriptor {
						Name = "IIS",
						ProcessNames = new string[] { "wp3.exe" },
					},

					new AttachDescriptor {
						Name = "IIS Express",
						ProcessNames = new string[] { "iisexpress.exe" },
					},

					new AttachDescriptor {
						Name = "NUnit",
						ProcessNames = new string[] { "nunit-agent.exe", "nunit.exe", "nunit-console.exe", "nunit-agent-x86.exe", "nunit-x86.exe", "nunit-console-x86.exe" },
					}
			};
			var start = items.Count;
			for ( int i = start; i < ATAConstants.MaxCommands; ++i ) {
				items.Add ( new AttachDescriptor ( ) );
			}
			return items;
		}
	}
}
