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
		MonoTouch.UIKit.UITableView tblView { get; set; }

		[Action ("SelectTag:")]
		partial void SelectTag (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (schBar != null) {
				schBar.Dispose ();
				schBar = null;
			}

			if (scrView != null) {
				scrView.Dispose ();
				scrView = null;
			}

			if (tblView != null) {
				tblView.Dispose ();
				tblView = null;
			}

			if (pcrPager != null) {
				pcrPager.Dispose ();
				pcrPager = null;
			}
		}
	}
}
