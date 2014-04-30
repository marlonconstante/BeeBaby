using System;
using Domain.Moment;
using SQLite.Net;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Skahal.Infrastructure.Framework.Repositories;
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
	}
}

