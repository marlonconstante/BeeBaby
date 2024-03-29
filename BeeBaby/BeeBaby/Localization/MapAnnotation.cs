using System;
using MapKit;
using CoreLocation;

namespace BeeBaby.Localization
{
	public class MapAnnotation : MKAnnotation
	{
		string title;
		string subtitle;

		public MapAnnotation(CLLocationCoordinate2D coordinate, string title, string subtitle)
		{
			this.Coordinate = coordinate;
			this.title = title;
			this.subtitle = subtitle;
		}

		/// <summary>
		/// Gets or sets the coordinate.
		/// </summary>
		/// <value>The coordinate.</value>
		public override CLLocationCoordinate2D Coordinate {
			get;
		}

		/// <summary>
		/// Gets the title.
		/// </summary>
		/// <value>The title.</value>
		public override string Title {
			get {
				return title;
			}
		}

		/// <summary>
		/// Gets the subtitle.
		/// </summary>
		/// <value>The subtitle.</value>
		public override string Subtitle {
			get {
				return subtitle;
			}
		}
	}
}