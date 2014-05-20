using System;
using Domain.Moment;
using Infrastructure.Repositories.Memory;
using SQLite.Net;
using Infrastructure.Repositories.SqliteNet;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Globalization;
using Infrastructure.Globalization;
using Domain.Baby;

namespace Application
{
	public static class DomainConfig
	{
		/// <summary>
		/// Registers the dependencies.
		/// </summary>
		public static void RegisterDependencies(SQLiteConnection connection)
		{
//			var unitOfWork = new MemoryUnitOfWork();
//			DependencyService.Register<IUnitOfWork>(unitOfWork);
//			DependencyService.Register<IMomentRepository>(new MemoryMomentRepository(unitOfWork));
//
//			var eventRepository = new MemoryEventRepository(unitOfWork);
//			MockEvents(unitOfWork, eventRepository);
//			DependencyService.Register<IEventRepository>(eventRepository);
//			var locationRepository = new MemoryLocationRepository(unitOfWork);
//			MockLocation(unitOfWork, locationRepository);
//			DependencyService.Register<ILocationRepository>(locationRepository);


			var unitOfWork = new MemoryUnitOfWork();
			DependencyService.Register<IUnitOfWork>(unitOfWork);
			DependencyService.Register<IEventRepository>(new SqliteNetEventRepository(connection, unitOfWork));
			DependencyService.Register<IMomentRepository>(new SqliteNetMomentRepository(connection, unitOfWork));
			DependencyService.Register<IBabyRepository>(new SqliteNetBabyRepository(connection, unitOfWork));
			DependencyService.Register<ILocationRepository>(new SqliteNetLocationRepository(connection, unitOfWork));
		}

		static void MockEvents(MemoryUnitOfWork unitOfWork, MemoryEventRepository eventRepository)
		{
			eventRepository.Add(new Event()
			{
				Description = "Primeiro Sorriso"
			});
			eventRepository.Add(new Event()
			{
				Description = "Primeiro Banho"
			});
			eventRepository.Add(new Event()
			{
				Description = "Primeiro Passeio"
			});
			unitOfWork.Commit();
		}

		static void MockLocation(MemoryUnitOfWork unitOfWork, MemoryLocationRepository locationRepository)
		{
			locationRepository.Add(new Location()
			{
				Id = "1",
				Name = "Casa da Vovó",
				Position = new GlobalPosition() { Latitude = 1, Longitude = 1 }
			});

			locationRepository.Add(new Location()
			{
				Id = "2",
				Name = "Minha Casa",
				Position = new GlobalPosition() { Latitude = 1, Longitude = 1 }
			});

			locationRepository.Add(new Location()
			{
				Id = "3",
				Name = "Escolinha",
				Position = new GlobalPosition() { Latitude = 1, Longitude = 1 }
			});

			unitOfWork.Commit();

		}

		public static void InitializeGlobalization()
		{
			GlobalizationService.Initialize(new GlobalizationLabelRepository());
		}
	}
}

