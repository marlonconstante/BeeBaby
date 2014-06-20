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
		MonoTouch.UIKit.UIPageControl pcrPager { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISearchBar schBar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView scrView { get; set; }

		[Outlet]
		MonoTouch.UIKit.NSLayoutConstraint tagsHeightConstraint { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView tblView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (pcrPager != null) {
				pcrPager.Dispose ();
				pcrPager = null;
			}

			if (schBar != null) {
				schBar.Dispose ();
				schBar = null;
			}

			if (scrView != null) {
				scrView.Dispose ();
				scrView = null;
			}

			if (tagsHeightConstraint != null) {
				tagsHeightConstraint.Dispose ();
				tagsHeightConstraint = null;
			}

			if (tblView != null) {
				tblView.Dispose ();
				tblView = null;
			}
		}
	}
}
