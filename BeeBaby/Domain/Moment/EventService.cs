﻿using System;
using System.Linq;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using Skahal.Infrastructure.Framework.PCL.Domain;
using System.Collections.Generic;

namespace Domain.Moment
{
	/// <summary>
	/// Domain layer service related to kind of moments.
	/// </summary>
	public class EventService : ServiceBase<Event, IEventRepository, IUnitOfWork>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Moment.EventService"/> class.
		/// </summary>
		/// <param name="mainRepository">Main repository.</param>
		/// <param name="unitOfWork">Unit of work.</param>
		public EventService(IEventRepository mainRepository, IUnitOfWork unitOfWork)
			: base(mainRepository, unitOfWork)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Moment.EventService"/> class.
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

		/// <summary>
		/// Gets all events.
		/// </summary>
		/// <returns>The all events.</returns>
		/// <param name="moments">Moments.</param>
		public IEnumerable<Event> GetAllEvents(IEnumerable<Moment> moments)
		{
			return MainRepository.FindAll((e) => moments.Count(m => m.Event.Id == e.Id && e.Kind == EventType.Achievement) <= 0);
		}

		/// <summary>
		/// Gets all events with non used achivments.
		/// </summary>
		/// <returns>The all events with non used achivments.</returns>
		public IEnumerable<Event> GetAllEventsWithNonUsedAchivments()
		{
			return MainRepository.FindEventsWithNonUsedAchievements();
		}
	}
}

