// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace BeeBaby.Controllers
{
	[Register ("TimelineViewController")]
	partial class TimelineViewController
	{
		[Outlet]
		UIKit.UILabel lblBabyAge { get; set; }

		[Outlet]
		UIKit.UILabel lblBabyName { get; set; }

		[Outlet]
		UIKit.UITableView tblView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tblView != null) {
				tblView.Dispose ();
				tblView = null;
			}

			if (lblBabyName != null) {
				lblBabyName.Dispose ();
				lblBabyName = null;
			}

			if (lblBabyAge != null) {
				lblBabyAge.Dispose ();
				lblBabyAge = null;
			}
		}
	}
}
