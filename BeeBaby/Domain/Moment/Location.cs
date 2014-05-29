using System;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Globalization;


namespace Domain.Moment
{
	/// <summary>
	/// Class that represents a location for a moment.
	/// </summary>
	public class Location : EntityWithIdBase<string>, IAggregateRoot
	{
		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>The position.</value>
		public Coordinates Position { set; get; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { set; get; }

		/// <summary>
		/// Gets the name of the place.
		/// </summary>
		/// <value>The name of the place.</value>
		public string PlaceName {
			get {
				return string.IsNullOrEmpty(Name) ? "AnyPlace".Translate() : Name;
			}
		}
	}
}