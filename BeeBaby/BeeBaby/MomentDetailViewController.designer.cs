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
		MonoTouch.UIKit.UIButton btnSave { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSelectEvent { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblMomentAbout { get; set; }

		[Outlet]
		MonoTouch.MapKit.MKMapView mapView { get; set; }

		[Outlet]
		BeeBaby.TextView txtDescription { get; set; }

		[Outlet]
		BeeBaby.TextField txtLocalName { get; set; }

		[Outlet]
		BeeBaby.ViewDatePicker vwDate { get; set; }

		[Action ("GoBackToEvents:")]
		partial void GoBackToEvents (MonoTouch.UIKit.UIButton sender);

		[Action ("Save:")]
		partial void Save (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}

			if (btnSelectEvent != null) {
				btnSelectEvent.Dispose ();
				btnSelectEvent = null;
			}

			if (lblMomentAbout != null) {
				lblMomentAbout.Dispose ();
				lblMomentAbout = null;
			}

			if (mapView != null) {
				mapView.Dispose ();
				mapView = null;
			}

			if (txtDescription != null) {
				txtDescription.Dispose ();
				txtDescription = null;
			}

			if (txtLocalName != null) {
				txtLocalName.Dispose ();
				txtLocalName = null;
			}

			if (vwDate != null) {
				vwDate.Dispose ();
				vwDate = null;
			}
		}
	}
}
