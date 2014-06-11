using System;
using Skahal.Infrastructure.Framework.Domain;

namespace Domain.Moment
{
	/// <summary>
	/// Event type.
	/// </summary>
	public enum EventType
	{
		Achivment = 0,
		Everyday = 1
	}

	/// <summary>
	/// Tag type.
	/// </summary>
	public enum TagType
	{
		LittleBody = 0,
		Family = 1,
		Ride = 2,
		Sleepy = 3,
		Bath = 4,
		Smile = 5,
		Lapy = 6,
		Celebrations = 7,
		Birth = 8,
		Pregnancy = 9,
		School = 10
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

		/// <summary>
		/// Gets or sets the tag.
		/// </summary>
		/// <value>The tag.</value>
		public TagType Tag { get; set; }

		/// <summary>
		/// Gets or sets the Order.
		/// </summary>
		/// <value>The Order.</value>
		public int Priority { get; set; }
	}
}