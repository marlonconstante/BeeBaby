using System;
using Skahal.Infrastructure.Framework.Domain;


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
		public GlobalPosition Position { set; get; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { set; get; }
	}
}

