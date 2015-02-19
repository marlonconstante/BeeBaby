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
		/// Removes the moment.
		/// </summary>
		/// <param name="moment">Moment.</param>
		public void RemoveMoment(Moment moment)
		{
			MainRepository.Remove(moment);
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
		public IEnumerable<IMoment> GetAllMoments(Baby.Baby baby)
		{
			MainRepository.RemoveInvalidMoments();

			return MainRepository.FindAllDescending(
				(m) => m.Babies.Count(b => b.Id.Equals(baby.Id)) > 0,
				(o) => o.Date);
		}

		/// <summary>
		/// Finds all moments.
		/// </summary>
		/// <returns>The all moments.</returns>
		public IEnumerable<Moment> FindAllMoments()
		{
			return MainRepository.FindAll();
		}

		/// <summary>
		/// Gets the first moment.
		/// </summary>
		/// <returns>The first moment.</returns>
		public Moment GetFirstMoment()
		{
			return MainRepository.FindFirst();
		}

		/// <summary>
		/// Gets the last moment.
		/// </summary>
		/// <returns>The last moment.</returns>
		public Moment GetLastMoment()
		{
			return MainRepository.FindLast();
		}

		/// <summary>
		/// Determines whether this instance has valid moments.
		/// </summary>
		/// <returns><c>true</c> if this instance has valid moments; otherwise, <c>false</c>.</returns>
		public bool HasValidMoments()
		{
			return MainRepository.CountValidMoments() > 0;
		}

		/// <summary>
		/// Determines whether this instance is changed moments the specified size.
		/// </summary>
		/// <returns><c>true</c> if this instance is changed moments the specified size; otherwise, <c>false</c>.</returns>
		/// <param name="size">Size.</param>
		public bool IsChangedMoments(int size)
		{
			return MainRepository.CountValidMoments() != size;
		}
	}
}