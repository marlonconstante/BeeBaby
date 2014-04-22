using Infrastructure.Repositories.Commons;
using Domain.Moment;
using Infrastructure.Repositories.SqliteNet.Entities;

namespace Infrastructure.Repositories.SqliteNet.Mapper
{
	internal class SqliteNetLocationMapper : IMapper<Location, LocationData>
	{
		#region IMapper implementation

		public Location ToDomainEntity(LocationData source)
		{
			Location result = null;

			if (source != null)
			{
				result = new Location();
				result.Id = source.Id;
				result.Name = source.Name;
				result.Position = new GlobalPosition()
				{
					Latitude = source.Latitude,
					Longitude = source.Longitude
				};
			}

			return result;
		}

		public LocationData ToRepositoryEntity(Location source)
		{
			LocationData result = null;

			if (source != null)
			{
				result = new LocationData();
				result.Id = source.Id;
				result.Name = source.Name;
				result.Latitude = source.Position.Latitude;
				result.Longitude = source.Position.Longitude;
			}

			return result;
		}

		#endregion
	}
}

