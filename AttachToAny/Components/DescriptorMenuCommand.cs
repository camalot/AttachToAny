using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace RyanConrad.AttachToAny.Components {
	public class DescriptorMenuCommand : OleMenuCommand {
		private Predicate<int> matches;
		public DescriptorMenuCommand ( CommandID rootId, Predicate<int> matches, EventHandler invokeHandler, EventHandler beforeQueryStatusHandler )
			: base ( invokeHandler, null, beforeQueryStatusHandler, rootId ) {
			if ( matches == null ) {
				throw new ArgumentNullException ( "matches" );
			}

			this.matches = matches;
		}

		public override bool DynamicItemMatch ( int cmdId ) {
			if ( this.matches ( cmdId ) ) {
				this.MatchedCommandId = cmdId;
				return true;
			}

			this.MatchedCommandId = 0;
			return false;
		}
	}
}
