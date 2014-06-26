using System;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;

namespace BeeBaby
{
	public class ZoomMapViewDelegate : MKMapViewDelegate
	{
		double m_zoom;
		MomentDetailViewController m_controller;

		public ZoomMapViewDelegate(double zoom, MomentDetailViewController controller)
		{
			m_zoom = zoom;
			m_controller = controller;
			UpdateUserLocation = true;
		}

		/// <summary>
		/// Event called when the user location is updated.
		/// </summary>
		/// <param name="mapView">Map view.</param>
		/// <param name="userLocation">User location.</param>
		public override void DidUpdateUserLocation(MKMapView mapView, MKUserLocation userLocation)
		{
			if (UpdateUserLocation)
			{
				mapView.CenterCoordinate = userLocation.Coordinate;
				m_controller.LoadNearLocation();
				changeZoomMap(mapView);
			}
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
			var coordinate = mapView.CenterCoordinate;
			if (coordinate.Latitude != 0f || coordinate.Longitude != 0f)
			{
				var span = new MKCoordinateSpan(m_zoom, m_zoom);
				var region = new MKCoordinateRegion(coordinate, span);
				mapView.Region = region;
				mapView.ClipsToBounds = true;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BeeBaby.ZoomMapViewDelegate"/> update user location.
		/// </summary>
		/// <value><c>true</c> if update user location; otherwise, <c>false</c>.</value>
		public bool UpdateUserLocation {
			get;
			set;
		}

		/// <summary>
		/// Dispose the specified disposing.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Discard.ReleaseProperties(this);
				Discard.ReleaseFields(this);
			}

			base.Dispose(disposing);
		}
	}
}