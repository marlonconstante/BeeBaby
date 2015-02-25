using System;
using Domain.Moment;
using Infrastructure.Repositories.SqliteNet.Entities;
using SQLite.Net;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using Infrastructure.Repositories.SqliteNet.Mapper;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetLocationRepository : SqliteNetRepositoryBase<Location, LocationData>, ILocationRepository
	{
		public SqliteNetLocationRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetLocationMapper(), unitOfWork)
		{
			connection.CreateTable<LocationData>();
		}
	}
}

