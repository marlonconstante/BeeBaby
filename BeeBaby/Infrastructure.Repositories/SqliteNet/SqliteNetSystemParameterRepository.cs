using System;
using SQLite.Net;
using Infrastructure.Configuration;
using Infrastructure.Repositories.SqliteNet.Entities;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using Infrastructure.Repositories.SqliteNet.Mapper;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetSystemParameterRepository: SqliteNetRepositoryBase<SystemParameter, SystemParameterData>, ISystemParameterRepository
	{
		public SqliteNetSystemParameterRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetSystemParameterMapper(), unitOfWork)
		{
			connection.CreateTable<SystemParameter>();
		}
	}
}

