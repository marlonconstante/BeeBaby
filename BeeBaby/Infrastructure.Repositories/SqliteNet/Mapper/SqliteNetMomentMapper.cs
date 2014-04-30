using System;
using Infrastructure.Repositories.Commons;
using Domain.Moment;
using Infrastructure.Repositories.SqliteNet.Entities;
using Skahal.Infrastructure.Framework.Commons;

namespace Infrastructure.Repositories.SqliteNet.Mapper
{
	internal class SqliteNetMomentMapper : IMapper<Moment, MomentData>
	{
		private IEventRepository m_eventRepository;

		internal SqliteNetMomentMapper() : base()
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
//				result.Event = new SqliteNetEventMapper().ToDomainEntity(source.Event);
				result.Event = m_eventRepository.FindBy(source.EventId);
				result.Position = new GlobalPosition()
				{
					Latitude = source.Latitude,
					Longitude = source.Longitude
				};
				result.Location = new SqliteNetLocationMapper().ToDomainEntity(source.Location);
				result.Date = source.Date;
				result.Babies = MapperHelper.ToDomainEntities(source.Babies, new SqliteNetBabyMapper());
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
				result.Event = new SqliteNetEventMapper().ToRepositoryEntity(source.Event);
				result.EventId = source.Event != null ? source.Event.Id : null;
				result.Location = new SqliteNetLocationMapper().ToRepositoryEntity(source.Location);
				result.LocationId = source.Location != null ? source.Location.Id : null;
				if (source.Position != null)
				{
					result.Latitude = source.Position.Latitude;
					result.Longitude = source.Position.Longitude;
				}
				result.Date = source.Date;
				result.Babies = MapperHelper.ToRepositoryEntities(source.Babies, new SqliteNetBabyMapper());
			}

			return result;
		}

		#endregion
	}
}

