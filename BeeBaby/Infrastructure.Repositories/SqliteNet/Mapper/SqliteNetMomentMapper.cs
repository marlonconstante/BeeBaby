using System;
using Infrastructure.Repositories.Commons;
using Domain.Moment;
using Infrastructure.Repositories.SqliteNet.Entities;

namespace Infrastructure.Repositories.SqliteNet.Mapper
{
	internal class SqliteNetMomentMapper : IMapper<Moment, MomentData>
	{
		#region IMapper implementation

		public Moment ToDomainEntity(MomentData source)
		{
			Moment result = null;

			if (source != null)
			{
				result = new Moment();
				result.Id = source.Id;
				result.Description = source.Description;
				result.Event = new SqliteNetEventMapper().ToDomainEntity(source.Event);
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
			}

			return result;
		}

		#endregion
	}
}

