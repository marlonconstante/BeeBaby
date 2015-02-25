using System;
using Infrastructure.Repositories.SqliteNet.Entities;
using SQLite.Net;
using Domain.Log;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using Infrastructure.Repositories.SqliteNet.Mapper;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetFlowRepository : SqliteNetRepositoryBase<Flow, FlowData>, IFlowRepository
	{
		public SqliteNetFlowRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetFlowMapper(), unitOfWork)
		{
			connection.CreateTable<FlowData>();
		}
	}
}