using System;
using Domain.Moment;
using SQLite.Net;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Skahal.Infrastructure.Framework.Repositories;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetEventRepository: SqliteNetRepositoryBase<Event, EventData>, IEventRepository 
	{
		public SqliteNetEventRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetEventMapper(), unitOfWork)
		{
			connection.CreateTable<EventData>();
		}
	}
}

