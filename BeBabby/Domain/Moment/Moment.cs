using System;
using Skahal.Infrastructure.Framework.Domain;
using System.Collections.Generic;
using Domain.Media;

namespace Domain.Moment
{
	/// <summary>
	/// Represents a Moment, usually composed of photos and videos.
	/// </summary>
	public class Moment : EntityWithIdBase<string>, IAggregateRoot
	{
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public MomentKind Title { set; get; }

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
		/// Gets or sets the local.
		/// </summary>
		/// <value>The local.</value>
		public Location Local { set; get; }	}
}

