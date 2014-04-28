using System;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Domain;
using System.Collections.Generic;

namespace Domain.Moment
{
	/// <summary>
	/// Domain layer service related to kind of moments.
	/// </summary>
	public class EventService : ServiceBase<Event, IEventRepository, IUnitOfWork>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Moment.MomentKindService"/> class.
		/// </summary>
		/// <param name="mainRepository">Main repository.</param>
		/// <param name="unitOfWork">Unit of work.</param>
		public EventService(IEventRepository mainRepository, IUnitOfWork unitOfWork)
			: base(mainRepository, unitOfWork)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Moment.MomentKindService"/> class.
		/// </summary>
		public EventService()
		{
		}

		/// <summary>
		/// Gets all moment kinds.
		/// </summary>
		/// <returns>All moment kinds.</returns>
		public IEnumerable<Event> GetAllEvents()
		{
			return MainRepository.FindAllAscending((o) => o.Id);
		}

		public IEnumerable<Event> GetSuggestedEvents(Baby.Baby baby)
		{
			return MainRepository.FindAllAscending(0, 5, (e) => baby.AgeInDays >= e.StartAge && baby.AgeInDays <= e.EndAge,
				(o) => o.EndAge);
		}
	}
}

