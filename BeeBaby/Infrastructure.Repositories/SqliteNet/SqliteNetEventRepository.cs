using System;
using Domain.Moment;
using SQLite.Net;
using Infrastructure.Framework.Repositories;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Repositories.SqliteNet.Mapper;

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

