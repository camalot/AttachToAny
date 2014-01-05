using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using RyanConrad.AttachToAny.Options;


namespace RyanConrad.AttachToAny {
	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	///
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the 
	/// IVsPackage interface and uses the registration attributes defined in the framework to 
	/// register itself and its components with the shell.
	/// </summary>
	// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
	// a package.
	[PackageRegistration ( UseManagedResourcesOnly = true )]
	// This attribute is used to register the information needed to show this package
	// in the Help/About dialog of Visual Studio.
	[InstalledProductRegistration ( "#110", "#112", "1.0", IconResourceID = 400 )]
	// This attribute is needed to let the shell know that this package exposes some menus.
	[ProvideMenuResource ( "Menus.ctmenu", 1 )]
	[ProvideOptionPage ( typeof ( GeneralOptionsPage ), "AttachToAny", "General", 110, 120, false )]
	[Guid ( GuidList.guidAttachToAnyPkgString )]
	[ProvideAutoLoad ( Microsoft.VisualStudio.Shell.Interop.UIContextGuids.NoSolution )]
	[ProvideAutoLoad ( Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionExists )]
	[ProvideAutoLoad ( Microsoft.VisualStudio.Shell.Interop.UIContextGuids.FullScreenMode )]
	[ProvideAutoLoad ( Microsoft.VisualStudio.Shell.Interop.UIContextGuids.DesignMode )]
	[ProvideAutoLoad ( Microsoft.VisualStudio.Shell.Interop.UIContextGuids.Debugging )]
	public sealed class AttachToAnyPackage : Package {

		/// <summary>
		/// Default constructor of the package.
		/// Inside this method you can place any initialization code that does not require 
		/// any Visual Studio service because at this point the package object is created but 
		/// not sited yet inside Visual Studio environment. The place to do all the other 
		/// initialization is the Initialize method.
		/// </summary>
		public AttachToAnyPackage ( ) {
		}

		#region Package Members

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initialization code that rely on services provided by VisualStudio.
		/// </summary>
		protected override void Initialize ( ) {
			base.Initialize ( );
			var mcs = GetService ( typeof ( IMenuCommandService ) ) as OleMenuCommandService;
			if ( null != mcs ) {
				var menuBuilder = new MenuBuilder ( this.GetDialogPage ( typeof ( GeneralOptionsPage ) ) as GeneralOptionsPage );
				menuBuilder.BuildMenuItems ( mcs );
			}
		}

		/// <summary>
		/// Called to ask the package if the shell can be closed.
		/// </summary>
		/// <param name="canClose">[out] Returns true if the shell can be closed, otherwise false.</param>
		/// <returns>
		///   <see cref="F:Microsoft.VisualStudio.VSConstants.S_OK" /> if the method succeeded, otherwise an error code.
		/// </returns>
		protected override int QueryClose ( out bool canClose ) {
			return base.QueryClose ( out canClose );
		}
		/// <summary>
		/// Invoked by the package class when there are options to be read out of the solution file.
		/// </summary>
		/// <param name="key">The name of the option key to load.</param>
		/// <param name="stream">The stream to load the option data from.</param>
		protected override void OnLoadOptions ( string key, System.IO.Stream stream ) {
			Debug.WriteLine ( "OnLoadOptions: {0}", key );
			base.OnLoadOptions ( key, stream );
		}

		/// <summary>
		/// Invoked by the <see cref="T:Microsoft.VisualStudio.Shell.Package" /> class when there are options to be saved to the solution file.
		/// </summary>
		/// <param name="key">The name of the option key to save.</param>
		/// <param name="stream">The stream to save the option data to.</param>
		protected override void OnSaveOptions ( string key, System.IO.Stream stream ) {
			Debug.WriteLine ( "OnSaveOptions: {0}", key );
			base.OnSaveOptions ( key, stream );
		}
		#endregion


		/// <summary>
		/// This function is called when the user clicks the menu item that shows the 
		/// tool window. See the Initialize method to see how the menu item is associated to 
		/// this function using the OleMenuCommandService service and the MenuCommand class.
		/// </summary>
		private void ShowToolWindow ( object sender, EventArgs e ) {
			// Get the instance number 0 of this tool window. This window is single instance so this instance
			// is actually the only one.
			// The last flag is set to true so that if the tool window does not exists it will be created.
			//ToolWindowPane window = this.FindToolWindow(typeof(MyToolWindow), 0, true);
			//if ((null == window) || (null == window.Frame))
			//{
			//		throw new NotSupportedException(Resources.CanNotCreateWindow);
			//}
			//IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
			//Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
		}

	}
}
