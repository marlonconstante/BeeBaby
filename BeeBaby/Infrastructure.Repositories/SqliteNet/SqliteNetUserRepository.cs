using System;
using Infrastructure.Repositories.SqliteNet.Entities;
using SQLite.Net;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Domain.User;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetUserRepository : SqliteNetRepositoryBase<User, UserData>, IUserRepository
	{
		public SqliteNetUserRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetUserMapper(), unitOfWork)
		{
			connection.CreateTable<UserData>();
		}
	}
}