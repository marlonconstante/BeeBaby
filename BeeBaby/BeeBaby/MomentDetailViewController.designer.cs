// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BeBabby
{
	[Register ("MomentDetailViewController")]
	partial class MomentDetailViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnSelectEvent { get; set; }

		[Outlet]
		MonoTouch.MapKit.MKMapView mapView { get; set; }

		[Action ("LocationChanged:")]
		partial void LocationChanged (MonoTouch.UIKit.UISwitch sender);

		[Action ("Save:")]
		partial void Save (MonoTouch.UIKit.UIButton sender);

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
		}
	}
}
