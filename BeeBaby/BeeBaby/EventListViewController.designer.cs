// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BeeBaby
{
	[Register ("EventListViewController")]
	partial class EventListViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnTag1 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTag2 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTag3 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTag4 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTag5 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTag6 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTag7 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTag8 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTag9 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISearchBar schBar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView tblView { get; set; }

		[Action ("SelectTag:")]
		partial void SelectTag (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnTag1 != null) {
				btnTag1.Dispose ();
				btnTag1 = null;
			}

			if (btnTag2 != null) {
				btnTag2.Dispose ();
				btnTag2 = null;
			}

			if (btnTag3 != null) {
				btnTag3.Dispose ();
				btnTag3 = null;
			}

			if (btnTag4 != null) {
				btnTag4.Dispose ();
				btnTag4 = null;
			}

			if (btnTag5 != null) {
				btnTag5.Dispose ();
				btnTag5 = null;
			}

			if (btnTag6 != null) {
				btnTag6.Dispose ();
				btnTag6 = null;
			}

			if (btnTag7 != null) {
				btnTag7.Dispose ();
				btnTag7 = null;
			}

			if (btnTag8 != null) {
				btnTag8.Dispose ();
				btnTag8 = null;
			}

			if (btnTag9 != null) {
				btnTag9.Dispose ();
				btnTag9 = null;
			}

			if (schBar != null) {
				schBar.Dispose ();
				schBar = null;
			}

			if (tblView != null) {
				tblView.Dispose ();
				tblView = null;
			}
		}
	}
}
