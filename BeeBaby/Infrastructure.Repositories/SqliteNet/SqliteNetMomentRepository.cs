using System;
using Domain.Moment;
using SQLite.Net;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetMomentRepository : SqliteNetRepositoryBase<Moment, MomentData>, IMomentRepository
	{
		public SqliteNetMomentRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetMomentMapper(), unitOfWork)
		{
			connection.CreateTable<MomentData>();
		}

		protected override void PersistNewItem(Moment item)
		{
			base.PersistNewItem(item);

			foreach (var baby in item.Babies)
			{
				m_connection.Insert(new MomentsBabies
				{
					BabyId = baby.Id,
					MomentId = item.Id
				});
			}
		}

		/// <summary>
		/// Removes the invalid moments.
		/// </summary>
		/// <returns>The invalid moments.</returns>
		public int RemoveInvalidMoments()
		{
			return m_connection.Execute("DELETE FROM MomentData WHERE EventId IS NULL OR LocationId IS NULL");
		}

		/// <summary>
		/// Counts the valid moments.
		/// </summary>
		/// <returns>The valid moments.</returns>
		public int CountValidMoments()
		{
			return m_connection.ExecuteScalar<int>("SELECT COUNT(*) FROM MomentData WHERE EventId IS NOT NULL");
		}
	}
}