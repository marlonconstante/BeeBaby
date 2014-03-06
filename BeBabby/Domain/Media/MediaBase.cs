using System;
using Skahal.Infrastructure.Framework.Domain;

namespace Domain.Media
{
	/// <summary>
	/// Class that represents the kind of moment.
	/// </summary>
	public class MediaBase: EntityWithIdBase<string>, IAggregateRoot
	{
		public MediaBase()
		{
		}
	}
}

