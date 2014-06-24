using Domain.Moment;
using Skahal.Infrastructure.Framework.Repositories;
using System.Collections.Generic;

namespace Infrastructure.Repositories.Memory
{
	/// <summary>
	/// IMomentKindRepository memory implementation.
	/// </summary>
	public class MemoryEventRepository : MemoryRepository<Event>, IEventRepository
	{
		#region Fields
		private static long s_lastKey;
		#endregion

		public MemoryEventRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork, u =>
			{
				++s_lastKey;
				return s_lastKey.ToString();
			})
		{
			s_lastKey = 0;
		}

		public IEnumerable<Event> FindEventsWithNonUsedAchievements()
		{
			return null;
		}
	}
}

