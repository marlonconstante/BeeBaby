// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BeeBaby.Menu
{
	[Register ("MenuViewController")]
	partial class MenuViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView tblView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tblView != null) {
				tblView.Dispose ();
				tblView = null;
			}
		}
	}
}
