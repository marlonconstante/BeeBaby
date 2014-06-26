using System;
using System.Linq;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using System.Collections.Generic;
using System.Diagnostics;
using Skahal.Infrastructure.Framework.Commons;

namespace Domain.Moment
{
	/// <summary>
	/// Domain layer service related to moments.
	/// </summary>
	public class MomentService : ServiceBase<Moment, IMomentRepository, IUnitOfWork>
	{
		IMomentRepository m_remoteRepository;

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
		/// Syncs the moments.
		/// </summary>
		public void SyncMoments()
		{
			var moment = this.MainRepository.FindAllDescending((o) => o.Id).First();

			// Só salva o momento depois de ter um evento atrelado, assim evita lixo na base remota
			if (moment.Event != null)
			{
				try
				{
					m_remoteRepository = DependencyService.Create<IMomentRepository>("REMOTE");
					m_remoteRepository.SetUnitOfWork(UnitOfWork);
					m_remoteRepository.Add(moment);
					UnitOfWork.Commit();
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.StackTrace);
				}
			}
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
		public IEnumerable<Moment> GetAllMoments(Baby.Baby baby)
		{
			return MainRepository.FindAllDescending(
				(m) => m.Event != null
				&& m.Location != null
				&& m.Babies.Count(b => b.Id.Equals(baby.Id)) > 0,
				(o) => o.Date);
		}

		/// <summary>
		/// Gets the first moment.
		/// </summary>
		/// <returns>The first moment.</returns>
		public Moment GetFirstMoment()
		{
			return MainRepository.FindFirst();
		}
	}
}