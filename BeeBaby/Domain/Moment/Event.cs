using System;
using Infrastructure.Framework.Domain;

namespace Domain.Moment
{
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

	}
}

