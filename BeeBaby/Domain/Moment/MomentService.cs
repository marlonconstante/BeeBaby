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

		/// <summary>
		/// Saves the moment.
		/// </summary>
		/// <param name="moment">Moment.</param>
		public void SaveMoment(Moment moment)
		{
			MainRepository[moment.Id] = moment;
			UnitOfWork.Commit();
		}

		/// <summary>
		/// Creates the moment.
		/// </summary>
		/// <returns>The moment.</returns>
		public Moment CreateMoment()
		{
			var moment = new Moment();
			SaveMoment(moment);

			return moment;
		}
			
		/// <summary>
		/// Gets all moments.
		/// </summary>
		/// <returns>The all moments.</returns>
		public IEnumerable<Moment> GetAllMoments()
		{
			return MainRepository.FindAllAscending((o) => o.Id);
		}
	}
}

