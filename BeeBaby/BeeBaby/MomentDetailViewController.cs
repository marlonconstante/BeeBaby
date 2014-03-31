using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;
using Domain.Moment;
using Application;

namespace BeBabby
{
	public partial class MomentDetailViewController : UIViewController
	{
		public MomentDetailViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			new KeyboardNotification(View);
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			DefineZoomMap(mapView);

			if (CurrentContext.Instance.SelectedEvent != null)
			{
				CurrentContext.Instance.Moment.Event = CurrentContext.Instance.SelectedEvent;
				btnSelectEvent.SetTitle(CurrentContext.Instance.SelectedEvent.Description, UIControlState.Normal);
			}
		}

		/// <summary>
		/// Adjusts the zoom of the map.
		/// </summary>
		/// <param name="mapView">MapView.</param>
		private void DefineZoomMap(MKMapView mapView) {
			mapView.DidUpdateUserLocation += (sender, e) => {
				if (mapView.UserLocation != null) {
					CLLocationCoordinate2D coordinate = mapView.UserLocation.Coordinate;
					MKCoordinateSpan span = new MKCoordinateSpan(0.001, 0.001);
					MKCoordinateRegion region = new MKCoordinateRegion(coordinate, span);
					mapView.SetRegion(region, false);
				}
			};
		}

		/// <summary>
		/// Selects the event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void SelectEvent(UIButton sender)
		{
			PerformSegue("segueSelectEvent", sender);
		}

		/// <summary>
		/// Save the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Save(UIButton sender)
		{
			var momentService = new MomentService();
			momentService.SaveMoment(CurrentContext.Instance.Moment);
			PerformSegue("segueSave", sender);
		}
	}
}
