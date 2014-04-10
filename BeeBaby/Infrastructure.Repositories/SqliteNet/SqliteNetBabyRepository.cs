using System;
using Domain.Baby;
using SQLite.Net;
using Skahal.Infrastructure.Framework.Repositories;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Infrastructure.Repositories.SqliteNet.Entities;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetBabyRepository : SqliteNetRepositoryBase<Baby, BabyData>, IBabyRepository
	{
		public SqliteNetBabyRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetBabyMapper(), unitOfWork)
		{
			connection.CreateTable<BabyData>();

			if (CountAll(null) <= 0)
			{
				connection.ExecuteScalar<BabyData>("Insert Into BabyData (Id, Name, BirthDateTime) values ( '1', 'Nome do Bebe', '2014-01-01 01:01:01')");
			}
		}
	}
}

