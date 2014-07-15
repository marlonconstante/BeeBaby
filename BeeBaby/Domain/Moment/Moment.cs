using System;
using System.Collections.Generic;
using Domain.Media;
using Skahal.Infrastructure.Framework.Domain;

namespace Domain.Moment
{
	/// <summary>
	/// Represents a Moment, usually composed of photos and videos.
	/// </summary>
	public class Moment : EntityWithIdBase<string>, IAggregateRoot
	{
		public Moment() : base()
		{
			SelectedMediaNames = new List<string>();
			Babies = new List<Baby.Baby>();
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public Event Event { set; get; }

		/// <summary>
		/// Gets or sets the medias.
		/// </summary>
		/// <value>The medias.</value>
		public IList<MediaBase> Medias { set; get; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { set; get; }

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>The position.</value>
		public Coordinates Position { set; get; }

		/// <summary>
		/// Gets or sets the local.
		/// </summary>
		/// <value>The local.</value>
		public Location Location { set; get; }

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value>
		public DateTime Date { set; get; }

		/// <summary>
		/// Gets or sets the media count.
		/// </summary>
		/// <value>The media count.</value>
		public int MediaCount {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the selected media names.
		/// </summary>
		/// <value>The selected media names.</value>
		public IList<string> SelectedMediaNames { set; get; }

		/// <summary>
		/// Gets or sets the baby.
		/// </summary>
		/// <value>The baby.</value>
		public IList<Baby.Baby> Babies { set; get; }
	}
}