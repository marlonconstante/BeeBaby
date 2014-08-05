using System;
using Domain.Moment;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Infrastructure.Repositories.SqliteNet.Entities
{
	public class MomentData : DataBase
	{
		/// <summary>
		/// Gets or sets the event identifier.
		/// </summary>
		/// <value>The event identifier.</value>
		[ForeignKey(typeof(EventData))] 
		public string EventId { set; get; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		[ManyToOne]
		public EventData Event { set; get; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { set; get; }

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude.</value>
		public double Longitude { set; get; }

		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>The latitude.</value>
		public double Latitude { set; get; }

		/// <summary>
		/// Gets or sets the location identifier.
		/// </summary>
		/// <value>The location identifier.</value>
		[ForeignKey(typeof(LocationData))] 
		public string LocationId { set; get; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		[ManyToOne]
		public LocationData Location { set; get; }

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value>
		public DateTime Date { set; get; }

		/// <summary>
		/// Gets or sets the language.
		/// </summary>
		/// <value>The language.</value>
		public string Language {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the media count.
		/// </summary>
		/// <value>The media count.</value>
		public int MediaCount {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the babies.
		/// </summary>
		/// <value>The babies.</value>
		[ManyToMany(typeof(MomentsBabies))]
		public List<BabyData> Babies { set; get; }
	}
}