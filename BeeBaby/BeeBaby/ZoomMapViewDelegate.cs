using System;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;

namespace BeeBaby
{
	public class ZoomMapViewDelegate : MKMapViewDelegate
	{
		private double m_zoom;

		public ZoomMapViewDelegate(double zoom)
		{
			this.m_zoom = zoom;
		}

		/// <summary>
		/// Event called when the user location is updated.
		/// </summary>
		/// <param name="mapView">Map view.</param>
		/// <param name="userLocation">User location.</param>
		public override void DidUpdateUserLocation(MKMapView mapView, MKUserLocation userLocation)
		{
			changeZoomMap(mapView);
		}

		/// <summary>
		/// Wills the start rendering map.
		/// </summary>
		/// <param name="mapView">Map view.</param>
		public override void WillStartRenderingMap(MKMapView mapView)
		{
			changeZoomMap(mapView);
		}

		/// <summary>
		/// Adjusts the zoom of the map.
		/// </summary>
		/// <param name="mapView">MapView.</param>
		private void changeZoomMap(MKMapView mapView)
		{
			if (mapView.UserLocation != null) {
				CLLocationCoordinate2D coordinate = mapView.UserLocation.Coordinate;
				if (coordinate.Latitude != 0f || coordinate.Longitude != 0f) {
					MKCoordinateSpan span = new MKCoordinateSpan(m_zoom, m_zoom);
					MKCoordinateRegion region = new MKCoordinateRegion(coordinate, span);
					mapView.SetRegion(region, false);
				}
			}
		}
	}
}

