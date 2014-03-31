using System;
using Domain.Moment;
using SQLiteNetExtensions.Attributes;

namespace Infrastructure.Repositories.SqliteNet.Entities
{
	public class MomentData: DataBase
	{
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		[ManyToOne]
		public EventData Event { set; get; }

		/// <summary>
		/// Gets or sets the event identifier.
		/// </summary>
		/// <value>The event identifier.</value>
		[ForeignKey(typeof(Event))] 
		public string EventId { set; get; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { set; get; }
	}
}