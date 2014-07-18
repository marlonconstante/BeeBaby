using System;
using Infrastructure.Repositories.Commons;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Configuration;

namespace Infrastructure.Repositories.SqliteNet.Mapper
{
	public class SqliteNetSystemParameterMapper: IMapper<SystemParameter, SystemParameterData>
	{
		public SqliteNetSystemParameterMapper()
		{
		}


		#region IMapper implementation

		public SystemParameter ToDomainEntity(SystemParameterData source)
		{
			SystemParameter result = null;

			if (source != null)
			{
				result = new SystemParameter();
				result.Id = source.Id;
				result.Name = source.Name;
				result.Value = source.Value;
			}

			return result;
		}

		public SystemParameterData ToRepositoryEntity(SystemParameter source)
		{
			SystemParameterData result = null;

			if (source != null)
			{
				result = new SystemParameterData();
				result.Id = source.Id;
				result.Name = source.Name;
				result.Value = source.Value;
			}

			return result;
		}

		#endregion
	}
}

