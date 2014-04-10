using System;
using Infrastructure.Repositories.Commons;
using Domain.Baby;
using Infrastructure.Repositories.SqliteNet.Entities;

namespace Infrastructure.Repositories.SqliteNet.Mapper
{
	public class SqliteNetBabyMapper : IMapper<Baby, BabyData>
	{
		#region IMapper implementation

		public Baby ToDomainEntity(BabyData source)
		{
			Baby result = null;

			if (source != null)
			{
				result = new Baby();
				result.Id = source.Id;
				result.Name = source.Name;
				result.BirthDateTime = source.BirthDateTime;
			}

			return result;
		}

		public BabyData ToRepositoryEntity(Baby source)
		{
			BabyData result = null;

			if (source != null)
			{
				result = new BabyData();
				result.Id = source.Id;
				result.Name = source.Name;
				result.BirthDateTime = source.BirthDateTime;
			}

			return result;
		}

		#endregion
	}
}

