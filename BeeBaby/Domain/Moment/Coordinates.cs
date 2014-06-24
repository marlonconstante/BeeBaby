using System;

namespace Domain.Moment
{
	/// <summary>
	/// The coordinates class.
	/// </summary>
	public class Coordinates
	{
		public Coordinates()
		{
		}

		public Coordinates(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		public double Latitude { get; set; }

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		public double Longitude { get; set; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		public override string ToString()
		{
			return string.Format("({0:0.0000}, {1:0.0000})", Latitude, Longitude);
		}
	}
}