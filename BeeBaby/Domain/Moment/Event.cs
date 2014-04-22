using System;
using Skahal.Infrastructure.Framework.Domain;

namespace Domain.Moment
{
	public enum EventType
	{
		Achivment = 0,
		Everyday = 1
	}

	/// <summary>
	/// Class that represents the kind of moment.
	/// </summary>
	public class Event: EntityWithIdBase<string>, IAggregateRoot
	{
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { set; get; }

		/// <summary>
		/// Gets or sets the start age in days.
		/// </summary>
		/// <value>The start age.</value>
		public int StartAge	{ get; set; }

		/// <summary>
		/// Gets or sets the end age in days.
		/// </summary>
		/// <value>The end age.</value>
		public int EndAge { get; set; }

		/// <summary>
		/// Gets or sets the kind.
		/// </summary>
		/// <value>The kind.</value>
		public EventType Kind { get; set; }
	}
}