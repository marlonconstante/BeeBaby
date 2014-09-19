// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BeeBaby.Controllers
{
	[Register ("TimelineMomentCell")]
	partial class TimelineMomentCell
	{
		[Outlet]
		BeeBaby.VisualElements.Button btnDescription { get; set; }

		[Outlet]
		BeeBaby.VisualElements.Button btnOptions { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imgEventBadge { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblAge { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblDate { get; set; }

		[Outlet]
		BeeBaby.VisualElements.Label lblEventName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblWhere { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView vwPhotos { get; set; }

		[Action ("OpenOptions:")]
		partial void OpenOptions (MonoTouch.UIKit.UIButton sender);

		[Action ("ShowDescription:")]
		partial void ShowDescription (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnDescription != null) {
				btnDescription.Dispose ();
				btnDescription = null;
			}

			if (btnOptions != null) {
				btnOptions.Dispose ();
				btnOptions = null;
			}

			if (imgEventBadge != null) {
				imgEventBadge.Dispose ();
				imgEventBadge = null;
			}

			if (lblAge != null) {
				lblAge.Dispose ();
				lblAge = null;
			}

			if (lblDate != null) {
				lblDate.Dispose ();
				lblDate = null;
			}

			if (lblEventName != null) {
				lblEventName.Dispose ();
				lblEventName = null;
			}

			if (lblWhere != null) {
				lblWhere.Dispose ();
				lblWhere = null;
			}

			if (vwPhotos != null) {
				vwPhotos.Dispose ();
				vwPhotos = null;
			}
		}
	}
}
