using System;
using Infrastructure.Framework.Domain;
using Infrastructure.Framework.Repositories;
using System.Collections.Generic;

namespace Domain.Moment
{
	/// <summary>
	/// Domain layer service related to moments.
	/// </summary>
	public class MomentService : ServiceBase<Moment, IMomentRepository, IUnitOfWork>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Moment.MomentService"/> class.
		/// </summary>
		/// <param name="mainRepository">Main repository.</param>
		/// <param name="unitOfWork">Unit of work.</param>
		public MomentService(IMomentRepository mainRepository, IUnitOfWork unitOfWork)
			: base(mainRepository, unitOfWork)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Moment.MomentService"/> class.
		/// </summary>
		public MomentService()
		{
		}

		public IEnumerable<Moment> GetAllMoments()
		{
			return MainRepository.FindAllAscending((o) => o.Id);
		}
	}
}

