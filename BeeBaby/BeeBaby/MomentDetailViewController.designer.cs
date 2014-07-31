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
		MonoTouch.MapKit.MKMapView mapView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView scrView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView tblView { get; set; }

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

			if (mapView != null) {
				mapView.Dispose ();
				mapView = null;
			}

			if (scrView != null) {
				scrView.Dispose ();
				scrView = null;
			}

			if (tblView != null) {
				tblView.Dispose ();
				tblView = null;
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
