using System;
using Infrastructure.Repositories.Commons;
using Domain.Moment;
using Skahal.Infrastructure.Framework.Commons;
using Infrastructure.Repositories.Dropbox.Entities;
using Domain.Baby;
using System.Collections.Generic;

namespace Infrastructure.Repositories.Dropbox.Mapper
{
	internal class DropboxMomentMapper : IMapper<Moment, MomentData>
	{
		private IEventRepository m_eventRepository;

		internal DropboxMomentMapper() : base()
		{
			m_eventRepository = DependencyService.Create<IEventRepository>();
		}

		#region IMapper implementation

		public Moment ToDomainEntity(MomentData source)
		{
			Moment result = null;

			if (source != null)
			{
				result = new Moment();
				result.Id = source.Id;
				result.Description = source.Description;
				result.Event = m_eventRepository.FindBy(source.EventId);
				result.Position = new Coordinates()
				{
					Latitude = source.Latitude,
					Longitude = source.Longitude
				};
				result.Location = new LocationService().GetLocation(source.LocationId);
				result.Date = source.Date;
				result.Babies = new List<Baby>(){ new BabyService().GetBaby(source.BabyId) };
			}

			return result;
		}

		public MomentData ToRepositoryEntity(Moment source)
		{
			MomentData result = null;

			if (source != null)
			{
				result = new MomentData();
				result.Id = source.Id;
				result.Description = source.Description;
				result.EventId = source.Event.Id;
				result.EventDescription = source.Event.Description;

				if (source.Location != null)
				{
					result.LocationId = source.Location.Id;
					result.LocationDescription = source.Location.Name;
				}

				if (source.Position != null)
				{
					result.Latitude = source.Position.Latitude;
					result.Longitude = source.Position.Longitude;
				}

				result.Date = source.Date;
				result.BabyId = source.Babies[0].Id;
				result.BabyName = source.Babies[0].Name;
				result.BabyBirhDate = source.Babies[0].BirthDateTime;
			}

			return result;
		}

		#endregion
	}
}

