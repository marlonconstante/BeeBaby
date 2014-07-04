using Domain.Moment;
using Skahal.Infrastructure.Framework.Repositories;
using System.Collections.Generic;
using Domain.Baby;

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

		/// <summary>
		/// Removes the invalid moments.
		/// </summary>
		/// <returns>The invalid moments.</returns>
		public int RemoveInvalidMoments()
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Counts the valid moments.
		/// </summary>
		/// <returns>The valid moments.</returns>
		public int CountValidMoments()
		{
			throw new System.NotImplementedException();
		}
	}
}