using System;
using Skahal.Infrastructure.Framework.Repositories;
using System.Collections.Generic;

namespace Domain.Moment
{
	/// <summary>
	/// I event repository.
	/// </summary>
	public interface IEventRepository : IRepository<Event>
	{
		IEnumerable<Event> FindEventsWithNonUsedAchivments();
	}
}

