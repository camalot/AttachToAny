using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.Administration;
using RyanConrad.AttachToAny.Extensions;

namespace RyanConrad.AttachToAny.Models {
	public class ProcessItem {
		public ProcessItem ( EnvDTE.Process baseProcess ) {
			this.BaseProcess = baseProcess;

		}

		public string Name {
			get {
				return BaseProcess.Name;
			}
		}

		public string ShortName {
			get {
				return Path.GetFileName ( Name );
			}
		}

		public string Title {
			get {
				return Process.GetProcessById ( Id ).MainWindowTitle;
			}
		}

		public string DisplayText {
			get {
				return GetDisplayText ( );
			}
		}

		public int Id {
			get {
				return BaseProcess.ProcessID;
			}
		}

		public void Attach ( ) {
			BaseProcess.Attach ( );
		}


		public EnvDTE.Process BaseProcess { get; private set; }

		private string GetDisplayText ( ) {
			return String.IsNullOrWhiteSpace ( Title ) ? GetShortNameFormatted() : Title;
		}

		private string GetShortNameFormatted ( ) {
			if ( String.Compare ( ATAConstants.IIS_PROCESS, ShortName, true ) == 0 ) {
				var serverManager = new ServerManager ( );
				var applicationPoolCollection = serverManager.ApplicationPools;
				var appPool = applicationPoolCollection.FirstOrDefault ( ap => ap.WorkerProcesses.Any ( wp => wp.ProcessId == Id ) );
				if ( appPool == null ) {
					return ShortName;
				}

				return "{0} [{1}]".With ( ShortName, appPool.Name );

			} 
				return ShortName;
			
		}
	}
}
