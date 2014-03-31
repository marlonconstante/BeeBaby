using System;
using Infrastructure.Repositories.Commons;
using Domain.Moment;
using Infrastructure.Repositories.SqliteNet.Entities;

namespace Infrastructure.Repositories.SqliteNet.Mapper
{
	internal class SqliteNetEventMapper: IMapper<Event, EventData>
	{
		#region IMapper implementation

		public Event ToDomainEntity(EventData source)
		{
			Event result = null;

			if (source != null)
			{
				result = new Event();
				result.Id = source.Id;
				result.Description = source.Description;
			}

			return result;
		}

		public EventData ToRepositoryEntity(Event source)
		{
			EventData result = null;

			if (source != null)
			{
				result = new EventData();
				result.Id = source.Id;
				result.Description = source.Description;
			}

			return result;
		}

		#endregion
	}
}

