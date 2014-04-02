using System;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;

namespace BeeBaby
{
	public class ZoomMapViewDelegate : MKMapViewDelegate
	{
		private double zoom;

		public ZoomMapViewDelegate(double zoom)
		{
			this.zoom = zoom;
		}

		public override void WillStartRenderingMap(MKMapView mapView)
		{
			changeZoomMap(mapView);
		}

		/// <summary>
		/// Adjusts the zoom of the map.
		/// </summary>
		/// <param name="mapView">MapView.</param>
		private void changeZoomMap(MKMapView mapView) {
			if (mapView.UserLocation != null) {
				CLLocationCoordinate2D coordinate = mapView.UserLocation.Coordinate;
				if (coordinate.Latitude != 0f || coordinate.Longitude != 0f) {
					MKCoordinateSpan span = new MKCoordinateSpan(zoom, zoom);
					MKCoordinateRegion region = new MKCoordinateRegion(coordinate, span);
					mapView.SetRegion(region, false);
				}
			}
		}
	}
}

