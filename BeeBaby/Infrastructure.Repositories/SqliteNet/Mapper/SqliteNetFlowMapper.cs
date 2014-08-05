using System;
using Infrastructure.Repositories.Commons;
using Domain.Log;
using Infrastructure.Repositories.SqliteNet.Entities;

namespace Infrastructure.Repositories.SqliteNet.Mapper
{
	public class SqliteNetFlowMapper : IMapper<Flow, FlowData>
	{
		#region IMapper implementation

		public Flow ToDomainEntity(FlowData source)
		{
			Flow result = null;

			if (source != null)
			{
				result = new Flow();
				result.Id = source.Id;
				result.DeviceId = source.DeviceId;
				result.SessionId = source.SessionId;
				result.Name = source.Name;
				result.Date = source.Date;
			}

			return result;
		}

		public FlowData ToRepositoryEntity(Flow source)
		{
			FlowData result = null;

			if (source != null)
			{
				result = new FlowData();
				result.Id = source.Id;
				result.DeviceId = source.DeviceId;
				result.SessionId = source.SessionId;
				result.Name = source.Name;
				result.Date = source.Date;
			}

			return result;
		}

		#endregion
	}
}