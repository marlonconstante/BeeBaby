using System;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using System.Collections.Generic;

namespace Domain.Moment
{
	/// <summary>
	/// I event repository.
	/// </summary>
	public interface IEventRepository : IRepository<Event>
	{
		IEnumerable<Event> FindEventsWithNonUsedAchievements();
	}
}