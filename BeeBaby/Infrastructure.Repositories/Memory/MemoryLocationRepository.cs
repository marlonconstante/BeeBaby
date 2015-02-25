using System;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using Domain.Moment;

namespace Infrastructure.Repositories.Memory
{
	public class MemoryLocationRepository : MemoryRepository<Location>, ILocationRepository
	{
		#region Fields
		private static long s_lastKey;
		#endregion

		public MemoryLocationRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork, u =>
			{
				++s_lastKey;
				return s_lastKey.ToString();
			})
		{
			s_lastKey = 0;
		}
	}
}

