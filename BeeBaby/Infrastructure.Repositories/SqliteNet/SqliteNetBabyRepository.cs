﻿using System;
using Domain.Baby;
using SQLite.Net;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Infrastructure.Repositories.SqliteNet.Entities;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetBabyRepository : SqliteNetRepositoryBase<Baby, BabyData>, IBabyRepository
	{
		public SqliteNetBabyRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetBabyMapper(), unitOfWork)
		{
			connection.CreateTable<BabyData>();
			connection.CreateTable<MomentsBabies>();
		}
	}
}