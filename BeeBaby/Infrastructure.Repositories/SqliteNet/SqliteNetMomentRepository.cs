using System;
using Domain.Moment;
using SQLite.Net;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Skahal.Infrastructure.Framework.Repositories;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetMomentRepository : SqliteNetRepositoryBase<Moment, MomentData>, IMomentRepository 
	{
		public SqliteNetMomentRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetMomentMapper(), unitOfWork)
		{
			connection.CreateTable<MomentData>();
		}
	}
}

