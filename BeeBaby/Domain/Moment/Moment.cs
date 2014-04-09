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
		public GlobalPosition Position { set; get; }

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
		/// Gets or sets the selected media names.
		/// </summary>
		/// <value>The selected media names.</value>
		public IList<string> SelectedMediaNames { set; get; }
	}
}

