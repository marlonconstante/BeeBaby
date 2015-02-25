using NUnit.Framework;
using System;
using Skahal.Infrastructure.Framework.PCL.Commons;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using Domain.Moment;
using Infrastructure.Repositories.Memory;
using System.Linq;

namespace Domain.UnitTests.Moment
{
	[TestFixture()]
	public class LocationServiceTest
	{
		#region Fields

		private LocationService m_target;
		private MemoryUnitOfWork m_unitOfWork;
		private MemoryLocationRepository m_repository;

		#endregion

		#region Initialize

		[SetUp]
		public void InitializeTest()
		{ 
			m_unitOfWork = new MemoryUnitOfWork();
			m_repository = new MemoryLocationRepository(m_unitOfWork);

			m_repository.Add(new Location()
			{
				Id = "1",
				Name = "Name 1",
				Position = new Coordinates() { Latitude = 1, Longitude = 2 }
			});

			m_repository.Add(new Location()
			{
				Id = "2",
				Name = "Name 2",
				Position = new Coordinates() { Latitude = 1, Longitude = 2 }
			});

			m_repository.Add(new Location()
			{
				Id = "3",
				Name = "Name 3",
				Position = new Coordinates() { Latitude = 1, Longitude = 2 }
			});

			m_unitOfWork.Commit();


			m_target = new LocationService(m_repository, m_unitOfWork);
		}

		#endregion

		[Test()]
		public void Constructor_NoArgsDIRegistered_DIInstances()
		{
			DependencyService.Register<IUnitOfWork>(new MemoryUnitOfWork());
			DependencyService.Register<ILocationRepository>(new MemoryLocationRepository(null));

			var target = new LocationService();

			Assert.AreEqual(0, target.GetAllLocations().Count());
		}

		[Test()]
		public void GetAllLocations_NoArgs_AllMoments()
		{
			var actual = m_target.GetAllLocations();
			Assert.AreEqual(3, actual.Count());
		}

		//			- Não tem nenhum local e cria com localização
		[Test()]
		public void SaveLocation_NewLocation_SavedLocationWithCoord()
		{
			var location = new Location()
			{
				Id = "New 123",
				Name = "Nova Localização",
				Position = new Coordinates() { Latitude = 1, Longitude = 2 }
			};

			m_target.SaveLocation(location);

			var actual = m_target.GetLocation(location.Id);
			Assert.AreEqual(location.Name, actual.Name);
			Assert.AreEqual(location.Position.Latitude, actual.Position.Latitude);
			Assert.AreEqual(location.Position.Longitude, actual.Position.Longitude);
		}

		//			- Seleciona o Local já existente e não atualiza
		[Test()]
		public void SaveLocation_ExistingLocationFalse_SavedLocationWithoutChangingLocation()
		{
			var location = new Location()
			{
				Id = "1",
				Name = "Name 1",
				Position = new Coordinates() { Latitude = 5, Longitude = 5 }
			};

			m_target.SaveLocation(location);

			var actual = m_target.GetLocation(location.Id);
			Assert.AreEqual(location.Name, actual.Name);
			Assert.AreEqual(1, actual.Position.Latitude);
			Assert.AreEqual(2, actual.Position.Longitude);

		}
	}
}

