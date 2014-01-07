using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualStudio.PlatformUI;
using RyanConrad.AttachToAny.Extensions;
using RyanConrad.AttachToAny.Models;

namespace RyanConrad.AttachToAny.Dialog {
	/// <summary>
	/// Interaction logic for ProcessSelectionWindow.xaml
	/// </summary>
	[SuppressMessage ( "Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling" )]
	public partial class ProcessSelectionWindow : DialogWindow {
		public ProcessSelectionWindow ( IEnumerable<EnvDTE.Process> processes ) : base( ) {
			Processes = new List<ProcessItem> ();
			if ( processes.Count() == 0 ) {
				throw new ArgumentException ( "processes must contain items to show this dialog." );
			}
			foreach ( var p in processes ) {
				Processes.Add ( new ProcessItem ( p ) );
			}
			InitializeComponent ( );
			DataContext = this;
			this.Title = "{0} - {1}".With ( this.Title, Processes.First ( ).ShortName );
		}

		public ICollection<ProcessItem> Processes { get; set; }

		private void CloseButton_Click ( object sender, RoutedEventArgs e ) {
			this.Close ( );
		}

		private void ProcessesListView_DoubleClick ( object sender, RoutedEventArgs e ) {
			var item = ( sender as ListViewItem ).Content as ProcessItem;
			item.Attach ( );
			this.Close ( );
		}
	}
}
