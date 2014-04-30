using Domain.Moment;
using Skahal.Infrastructure.Framework.Repositories;
using System.Collections.Generic;

namespace Infrastructure.Repositories.Memory
{
	/// <summary>
	/// IMomentRepository memory implementation.
	/// </summary>
	public class MemoryMomentRepository : MemoryRepository<Moment>, IMomentRepository
	{
		#region Fields
		private static long s_lastKey;
		#endregion

		public MemoryMomentRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork, u =>
			{
				++s_lastKey;
				return s_lastKey.ToString();
			})
		{
			s_lastKey = 0;
		}

		public IEnumerable<Moment> FindByBaby(string babyId)
		{
			throw new System.NotImplementedException();
		}
	}
}

