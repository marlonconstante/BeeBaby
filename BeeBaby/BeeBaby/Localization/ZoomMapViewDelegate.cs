using System;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;
using BeeBaby.Util;

namespace BeeBaby.Localization
{
	public class ZoomMapViewDelegate : MKMapViewDelegate
	{
		MomentDetailViewController m_controller;
		double m_zoom;

		public ZoomMapViewDelegate(MomentDetailViewController controller, double zoom, bool updateUserLocation)
		{
			m_controller = controller;
			m_zoom = zoom;
			UpdateUserLocation = updateUserLocation;
		}

		/// <Docs>To be added.</Docs>
		/// <summary>
		/// To be added.
		/// </summary>
		/// <param name="mapView">Map view.</param>
		/// <param name="userLocation">User location.</param>
		public override void DidUpdateUserLocation(MKMapView mapView, MKUserLocation userLocation)
		{
			WillStartRenderingMap(mapView);
		}

		/// <summary>
		/// Wills the start rendering map.
		/// </summary>
		/// <param name="mapView">Map view.</param>
		public override void WillStartRenderingMap(MKMapView mapView)
		{
			if (UpdateUserLocation)
			{
				LoadUserLocation(mapView);
				m_controller.LoadNearLocation();
			}
			changeZoomMap(mapView);
		}

		/// <summary>
		/// Loads the user location.
		/// </summary>
		/// <param name="mapView">Map view.</param>
		public void LoadUserLocation(MKMapView mapView)
		{
			var coordinate = mapView.UserLocation.Coordinate;
			if (IsValidCoordinate(coordinate))
			{
				mapView.CenterCoordinate = coordinate;
			}
		}

		/// <summary>
		/// Adjusts the zoom of the map.
		/// </summary>
		/// <param name="mapView">MapView.</param>
		void changeZoomMap(MKMapView mapView)
		{
			var coordinate = mapView.CenterCoordinate;
			if (IsValidCoordinate(coordinate))
			{
				var span = new MKCoordinateSpan(m_zoom, m_zoom);
				var region = new MKCoordinateRegion(coordinate, span);
				mapView.Region = region;
				mapView.ClipsToBounds = true;
			}
		}

		/// <summary>
		/// Determines whether this instance is valid coordinate the specified coordinate.
		/// </summary>
		/// <returns><c>true</c> if this instance is valid coordinate the specified coordinate; otherwise, <c>false</c>.</returns>
		/// <param name="coordinate">Coordinate.</param>
		bool IsValidCoordinate(CLLocationCoordinate2D coordinate)
		{
			return coordinate.Latitude != 0f && coordinate.Longitude != 0f;
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