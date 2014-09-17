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
	[Register ("TimelineMomentCell")]
	partial class TimelineMomentCell
	{
		[Outlet]
		BeeBaby.Button btnDescription { get; set; }

		[Outlet]
		BeeBaby.Button btnOptions { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imgCalendar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imgEventBadge { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imgLocation { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblAge { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblDate { get; set; }

		[Outlet]
		BeeBaby.Label lblEventName { get; set; }

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

			if (imgLocation != null) {
				imgLocation.Dispose ();
				imgLocation = null;
			}

			if (imgCalendar != null) {
				imgCalendar.Dispose ();
				imgCalendar = null;
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
