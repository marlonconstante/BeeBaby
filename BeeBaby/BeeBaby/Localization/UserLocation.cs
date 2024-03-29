using System;
using CoreLocation;
using Domain.Moment;
using Foundation;
using BeeBaby.Util;
using ObjCRuntime;

namespace BeeBaby.Localization
{
	public class UserLocation : CLLocationManagerDelegate
	{
		public UserLocation()
		{
			LocationManager = new CLLocationManager();
			LocationManager.Delegate = this;

			if (LocationManager.RespondsToSelector(new Selector("requestWhenInUseAuthorization")))
			{
				LocationManager.RequestWhenInUseAuthorization();
			}
		}

		/// <summary>
		/// Starts the updating location.
		/// </summary>
		public void StartUpdatingLocation()
		{
			LocationManager.StartUpdatingLocation();
		}

		/// <summary>
		/// Stops the updating location.
		/// </summary>
		public void StopUpdatingLocation()
		{
			LocationManager.StopUpdatingLocation();
			UpdatedPosition = null;
		}

		/// <summary>
		/// Updated locations.
		/// </summary>
		/// <param name="manager">Manager.</param>
		/// <param name="locations">Locations.</param>
		public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
		{
			var location = locations[locations.Length - 1];
			var coordinate = location.Coordinate;
			Position = new Coordinates(coordinate.Latitude, coordinate.Longitude);
			if (UpdatedPosition != null)
			{
				UpdatedPosition();
			}
		}

		/// <summary>
		/// Gets or sets the location manager.
		/// </summary>
		/// <value>The location manager.</value>
		CLLocationManager LocationManager {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the updated position.
		/// </summary>
		/// <value>The updated position.</value>
		public Action UpdatedPosition {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>The position.</value>
		public Coordinates Position {
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
				StopUpdatingLocation();
				Discard.ReleaseProperties(this);
			}

			base.Dispose(disposing);
		}
	}
}