using Domain.Moment;
using Infrastructure.Framework.Repositories;

namespace Infrastructure.Repositories.Memory
{
	/// <summary>
	/// IMomentKindRepository memory implementation.
	/// </summary>
	public class MemoryMomentKindRepository : MemoryRepository<MomentKind>, IMomentKindRepository
	{
		#region Fields
		private static long s_lastKey;
		#endregion

		public MemoryMomentKindRepository(IUnitOfWork unitOfWork)
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

