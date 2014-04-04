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

			if (CountAll(null) <= 0)
			{
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description) values ( '1', 'Primeiro Evento')");
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description) values ( '2', 'Segundo Evento')");

			}
		}
	}
}

