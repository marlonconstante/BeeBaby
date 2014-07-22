﻿using System;
using Domain.Moment;
using Domain.User;
using Infrastructure.Repositories.Memory;
using SQLite.Net;
using Infrastructure.Repositories.SqliteNet;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Globalization;
using Infrastructure.Globalization;
using Domain.Baby;
using System.Globalization;
using Infrastructure.Configuration;

namespace Application
{
	public static class DomainConfig
	{
		/// <summary>
		/// Registers the dependencies.
		/// </summary>
		public static void RegisterDependencies(SQLiteConnection connection, CultureInfo currentCultureInfo)
		{
			var unitOfWork = new MemoryUnitOfWork();

			SystemParameterService.Initialize(new SqliteNetSystemParameterRepository(connection, unitOfWork), unitOfWork);

			DependencyService.Register<IUnitOfWork>(unitOfWork);
			DependencyService.Register<IMomentRepository>(new SqliteNetMomentRepository(connection, unitOfWork));
			DependencyService.Register<IEventRepository>(new SqliteNetEventRepository(connection, unitOfWork, currentCultureInfo.Name));
			DependencyService.Register<IUserRepository>(new SqliteNetUserRepository(connection, unitOfWork));
			DependencyService.Register<IBabyRepository>(new SqliteNetBabyRepository(connection, unitOfWork));
			DependencyService.Register<ILocationRepository>(new SqliteNetLocationRepository(connection, unitOfWork));
		}

		/// <summary>
		/// Initializes the globalization.
		/// </summary>
		public static void InitializeGlobalization(CultureInfo currentCultureInfo)
		{
			GlobalizationService.Initialize(new GlobalizationLabelRepository(), currentCultureInfo);
		}
	}
}

