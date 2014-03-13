using System;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Domain;
using System.Collections.Generic;

namespace Domain.Moment
{
	/// <summary>
	/// Domain layer service related to kind of moments.
	/// </summary>
	public class MomentKindService : ServiceBase<MomentKind, IMomentKindRepository, IUnitOfWork>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Moment.MomentKindService"/> class.
		/// </summary>
		/// <param name="mainRepository">Main repository.</param>
		/// <param name="unitOfWork">Unit of work.</param>
		public MomentKindService(IMomentKindRepository mainRepository, IUnitOfWork unitOfWork)
			: base(mainRepository, unitOfWork)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Moment.MomentKindService"/> class.
		/// </summary>
		public MomentKindService()
		{
		}

		/// <summary>
		/// Gets all moment kinds.
		/// </summary>
		/// <returns>All moment kinds.</returns>
		public IEnumerable<MomentKind> GetAllMomentKinds()
		{
			return MainRepository.FindAllAscending((o) => o.Id);
		}
	}
}

