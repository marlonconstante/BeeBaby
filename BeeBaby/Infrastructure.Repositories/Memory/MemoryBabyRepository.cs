using System;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using Domain.Baby;

namespace Infrastructure.Repositories.Memory
{
	public class MemoryBabyRepository: MemoryRepository<Baby>, IBabyRepository
	{
		#region Fields

		private static long s_lastKey;

		#endregion

		public MemoryBabyRepository(IUnitOfWork unitOfWork)
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

