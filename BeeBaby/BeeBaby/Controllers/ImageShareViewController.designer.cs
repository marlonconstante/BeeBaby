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
	[Register ("ImageShareViewController")]
	partial class ImageShareViewController
	{
		[Outlet]
		UIKit.UIImageView imgEventBadge { get; set; }

		[Outlet]
		UIKit.UIImageView ivwBackgroundImage { get; set; }

		[Outlet]
		UIKit.UILabel lblAge { get; set; }

		[Outlet]
		UIKit.UILabel lblDay { get; set; }

		[Outlet]
		BeeBaby.VisualElements.Label lblEvent { get; set; }

		[Outlet]
		UIKit.UILabel lblMonth { get; set; }

		[Outlet]
		UIKit.UILabel lblWhere { get; set; }

		[Outlet]
		UIKit.UILabel lblYear { get; set; }

		[Outlet]
		UIKit.UIView vwImageBadge { get; set; }

		[Outlet]
		UIKit.UIView vwLowerBackground { get; set; }

		[Outlet]
		UIKit.UIView vwLowerBAckground { get; set; }

		[Outlet]
		UIKit.UIView vwMainView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgEventBadge != null) {
				imgEventBadge.Dispose ();
				imgEventBadge = null;
			}

			if (ivwBackgroundImage != null) {
				ivwBackgroundImage.Dispose ();
				ivwBackgroundImage = null;
			}

			if (lblAge != null) {
				lblAge.Dispose ();
				lblAge = null;
			}

			if (lblDay != null) {
				lblDay.Dispose ();
				lblDay = null;
			}

			if (lblEvent != null) {
				lblEvent.Dispose ();
				lblEvent = null;
			}

			if (lblMonth != null) {
				lblMonth.Dispose ();
				lblMonth = null;
			}

			if (lblWhere != null) {
				lblWhere.Dispose ();
				lblWhere = null;
			}

			if (lblYear != null) {
				lblYear.Dispose ();
				lblYear = null;
			}

			if (vwImageBadge != null) {
				vwImageBadge.Dispose ();
				vwImageBadge = null;
			}

			if (vwLowerBackground != null) {
				vwLowerBackground.Dispose ();
				vwLowerBackground = null;
			}

			if (vwLowerBAckground != null) {
				vwLowerBAckground.Dispose ();
				vwLowerBAckground = null;
			}

			if (vwMainView != null) {
				vwMainView.Dispose ();
				vwMainView = null;
			}
		}
	}
}
