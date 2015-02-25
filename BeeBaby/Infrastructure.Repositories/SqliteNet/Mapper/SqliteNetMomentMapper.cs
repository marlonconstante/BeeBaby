using System;
using System.Linq;
using Infrastructure.Repositories.Commons;
using Domain.Moment;
using Infrastructure.Repositories.SqliteNet.Entities;
using Skahal.Infrastructure.Framework.PCL.Commons;

namespace Infrastructure.Repositories.SqliteNet.Mapper
{
	internal class SqliteNetMomentMapper : IMapper<Moment, MomentData>
	{
		internal SqliteNetMomentMapper() : base()
		{
		}

		#region IMapper implementation

		public Moment ToDomainEntity(MomentData source)
		{
			Moment result = null;

			if (source != null)
			{
				result = new Moment();
				result.Id = source.Id;
				result.ObjectId = source.ObjectId;
				result.Description = source.Description;
				result.Event = new SqliteNetEventMapper().ToDomainEntity(source.Event);
				result.Position = new Coordinates()
				{
					Latitude = source.Latitude,
					Longitude = source.Longitude
				};
				result.Location = new SqliteNetLocationMapper().ToDomainEntity(source.Location);
				result.Date = source.Date;
				result.Language = source.Language;
				result.MediaCount = source.MediaCount;
				result.Babies = MapperHelper.ToDomainEntities(source.Babies, new SqliteNetBabyMapper()).ToList();
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
				result.ObjectId = source.ObjectId;
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
				result.Language = source.Language;
				result.MediaCount = source.MediaCount;
				result.Babies = MapperHelper.ToRepositoryEntities(source.Babies, new SqliteNetBabyMapper()).ToList();
			}

			return result;
		}

		#endregion
	}
}

