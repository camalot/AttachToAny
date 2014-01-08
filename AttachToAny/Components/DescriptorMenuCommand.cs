using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using RyanConrad.AttachToAny.Models;

namespace RyanConrad.AttachToAny.Components {
	internal class DescriptorMenuCommand : OleMenuCommand {
		public DescriptorMenuCommand ( EventHandler invokeHandler, int commandId, AttachDescriptor descriptor )
			: base ( invokeHandler, new CommandID ( ATAGuids.guidAttachToAnyCmdGroup, commandId ), descriptor.ToString() ) {
				Descriptor = descriptor;
				this.BeforeQueryStatus += ( s, e ) => {
					this.Visible = Descriptor.Enabled && Descriptor.ProcessNames.Count ( ) > 0;
					this.Text = Descriptor.ToString ( );
				};
		}

		public AttachDescriptor Descriptor { get; set; }
	}
}
