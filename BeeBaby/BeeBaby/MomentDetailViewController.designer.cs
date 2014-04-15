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
	[Register ("MomentDetailViewController")]
	partial class MomentDetailViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnDate { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSelectEvent { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblTime { get; set; }

		[Outlet]
		MonoTouch.MapKit.MKMapView mapView { get; set; }

		[Outlet]
		MonoTouch.UIKit.NSLayoutConstraint mapViewConstraint { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIDatePicker pckDate { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView txtDescription { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView vwDate { get; set; }

		[Action ("LocationChanged:")]
		partial void LocationChanged (MonoTouch.UIKit.UISwitch sender);

		[Action ("Save:")]
		partial void Save (MonoTouch.UIKit.UIButton sender);

		[Action ("SelectDate:")]
		partial void SelectDate (MonoTouch.UIKit.UIButton sender);

		[Action ("SelectEvent:")]
		partial void SelectEvent (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnSelectEvent != null) {
				btnSelectEvent.Dispose ();
				btnSelectEvent = null;
			}

			if (mapView != null) {
				mapView.Dispose ();
				mapView = null;
			}

			if (mapViewConstraint != null) {
				mapViewConstraint.Dispose ();
				mapViewConstraint = null;
			}

			if (pckDate != null) {
				pckDate.Dispose ();
				pckDate = null;
			}

			if (txtDescription != null) {
				txtDescription.Dispose ();
				txtDescription = null;
			}

			if (vwDate != null) {
				vwDate.Dispose ();
				vwDate = null;
			}

			if (btnDate != null) {
				btnDate.Dispose ();
				btnDate = null;
			}

			if (lblTime != null) {
				lblTime.Dispose ();
				lblTime = null;
			}
		}
	}
}
