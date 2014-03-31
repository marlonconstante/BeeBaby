using System;
using Domain.Moment;
using SQLite.Net;
using Infrastructure.Framework.Repositories;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Repositories.SqliteNet.Mapper;

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

