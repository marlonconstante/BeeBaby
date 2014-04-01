﻿using System;
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
				result.Position = new GlobalPosition()
				{
					Latitude = source.Latitude,
					Longitude = source.Longitude
				};
				result.Location = new SqliteNetLocationMapper().ToDomainEntity(source.Location);
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
				result.LocationId = source.Event != null ? source.Location.Id : null;
			}

			return result;
		}

		#endregion
	}
}

