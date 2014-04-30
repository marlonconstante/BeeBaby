using System;
using Domain.Moment;
using SQLite.Net;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Skahal.Infrastructure.Framework.Repositories;
using System.Collections.Generic;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetMomentRepository : SqliteNetRepositoryBase<Moment, MomentData>, IMomentRepository
	{
		public SqliteNetMomentRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetMomentMapper(), unitOfWork)
		{
			connection.CreateTable<MomentData>();
		}

		public IEnumerable<Moment> FindByBaby(string babyId)
		{
//			var momentsData = m_connection.Table<MomentData>().Where(m => m.Babies.Count(b => b.Id.Equals(babyId)) > 0);
			var momentsData = m_connection.Table<MomentData>();//.Where(m => m.Babies.Count(b => b.Id.Equals(babyId)) > 0);

			IList<Moment> result = new List<Moment>();

			foreach (var item in momentsData)
			{
				result.Add(Mapper.ToDomainEntity(item));
			}

			return result;
		}
	}
}

