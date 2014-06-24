using System;
using NUnit.Framework;
using Domain.Moment;
using Infrastructure.Repositories.Memory;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Commons;
using System.Collections.Generic;
using Domain.Baby;
using Infrastructure.Repositories.Dropbox.Mapper;
using Infrastructure.Repositories.Dropbox.Entities;

namespace Infrastructure.Repositories.UnitTests.Dropbox.Mapper
{
	[TestFixture()]
	public class DropboxMomentMapperTest
	{
		MemoryEventRepository m_repository;
		MemoryUnitOfWork m_unitOfWork;
		MemoryLocationRepository m_locationRepository;
		MemoryBabyRepository m_babyRepository;

		DateTime m_birthDate;


		#region Initialize

		[TestFixtureSetUp]
		public void InitializeTest()
		{ 
			m_unitOfWork = new MemoryUnitOfWork();
			m_repository = new MemoryEventRepository(m_unitOfWork);
			m_locationRepository = new MemoryLocationRepository(m_unitOfWork);
			m_babyRepository = new MemoryBabyRepository(m_unitOfWork);
			m_birthDate = DateTime.Now.AddDays(-5);


			m_repository.Add(new Event()
			{
				Description = "Primeiro Sorriso"

			});
			m_repository.Add(new Event()
			{
				Description = "Primeiro Banho"

			});
			m_repository.Add(new Event()
			{
				Description = "Primeiro Passeio"

			});

			m_locationRepository.Add(new Location()
			{
				Id = "22",
				Name  = "Nome do Lugar"
			});

			m_babyRepository.Add(new Baby()
			{
				Id = "11",
				Name = "Nome Bebe",
				BirthDateTime = m_birthDate
			});

			m_unitOfWork.Commit();

			DependencyService.Register<IUnitOfWork>(m_unitOfWork);
			DependencyService.Register<IEventRepository>(m_repository);
			DependencyService.Register<ILocationRepository>(m_locationRepository);
			DependencyService.Register<IBabyRepository>(m_babyRepository);


		}

		#endregion

		[Test()]
		public void ToRepository_Null_Null()
		{
			Assert.AreEqual(null, new DropboxMomentMapper().ToRepositoryEntity(null));
		}

		[Test()]
		public void ToDomainEntity_Null_Null()
		{
			Assert.AreEqual(null, new DropboxMomentMapper().ToDomainEntity(null));
		}

		[Test()]
		public void ToDomainEntity_RepositoryEntity_DomainEntity()
		{

			var repositoryEntity = new MomentData()
			{
				Id = "1",
				Description = "Descrição",
				EventId = "1",
				EventDescription = "Primeiro Sorriso",
				Latitude = 12,
				Longitude = 34,
				Date = DateTime.Now,
				BabyId = "11",
				BabyBirhDate = m_birthDate,
				BabyName = "Nome Bebe",
				LocationId = "22",
				LocationDescription = "Nome do Lugar"
			};

			var actual = new DropboxMomentMapper().ToDomainEntity(repositoryEntity);
			Assert.AreEqual(repositoryEntity.Id, actual.Id);
			Assert.AreEqual(repositoryEntity.Description, actual.Description);
			Assert.IsNotNull(actual.Event);
			Assert.AreEqual(repositoryEntity.EventId, actual.Event.Id);
			Assert.AreEqual(repositoryEntity.EventDescription, actual.Event.Description);
			Assert.AreEqual(repositoryEntity.Latitude, actual.Position.Latitude);
			Assert.AreEqual(repositoryEntity.Longitude, actual.Position.Longitude);
			Assert.AreEqual(repositoryEntity.Date, actual.Date);
			Assert.IsNotNull(actual.Babies);
			Assert.AreEqual(repositoryEntity.BabyId, actual.Babies[0].Id);
			Assert.AreEqual(repositoryEntity.BabyName, actual.Babies[0].Name);
			Assert.AreEqual(repositoryEntity.BabyBirhDate, actual.Babies[0].BirthDateTime);
			Assert.AreEqual(repositoryEntity.LocationId, actual.Location.Id);
			Assert.AreEqual(repositoryEntity.LocationDescription, actual.Location.Name);
		}

		[Test()]
		public void ToRepositoryEntity_DomainEntity_RepositoryEntity()
		{
			var eventData = new Event
			{
				Id = "1",
				Description = "Descrição"
			};

			var local = new Coordinates
			{
				Latitude = 12,
				Longitude = 34
			};

			var baby = new Baby
			{
				Id = "1",
				Name = "Baby 12"
			};

			var location = new Location
			{
				Id = "1",
				Name = "Local",
				Position = new Coordinates
				{
					Longitude = 1,
					Latitude = 2
				}
			};


			var domainEntity = new Moment
			{
				Id = "1",
				Description = "Descrição",
				Event = eventData,
				Position = local,
				Date = DateTime.Now,
				Location = location,
				Babies = new List<Baby> { baby }
			};


			var actual = new DropboxMomentMapper().ToRepositoryEntity(domainEntity);
			Assert.AreEqual(domainEntity.Id, actual.Id);
			Assert.AreEqual(domainEntity.Description, actual.Description);

			Assert.AreEqual(domainEntity.Babies[0].Id, actual.BabyId);
			Assert.AreEqual(domainEntity.Babies[0].Name, actual.BabyName);
			Assert.AreEqual(domainEntity.Babies[0].BirthDateTime, actual.BabyBirhDate);
			Assert.AreEqual(domainEntity.Date, actual.Date);
			Assert.AreEqual(domainEntity.Event.Id, actual.EventId);
			Assert.AreEqual(domainEntity.Event.Description, actual.EventDescription);
			Assert.AreEqual(domainEntity.Location.Id, actual.LocationId);
			Assert.AreEqual(domainEntity.Location.Name, actual.LocationDescription);

			Assert.AreEqual(domainEntity.Position.Longitude, actual.Longitude);
			Assert.AreEqual(domainEntity.Position.Latitude, actual.Latitude);
		}

		[Test()]
		public void ToRepositoryEntity_PositionNull_RepositoryEntity()
		{
			var eventData = new Event
			{
				Id = "1",
				Description = "Descrição"
			};
					
			var baby = new Baby
			{
				Id = "1",
				Name = "Baby 12"
			};

			var domainEntity = new Moment
			{
				Id = "1",
				Description = "Descrição",
				Event = eventData,
				Date = DateTime.Now,
				Babies = new List<Baby> { baby }
			};

			var actual = new DropboxMomentMapper().ToRepositoryEntity(domainEntity);
			Assert.AreEqual(domainEntity.Id, actual.Id);
			Assert.AreEqual(domainEntity.Description, actual.Description);

			Assert.AreEqual(domainEntity.Babies[0].Id, actual.BabyId);
			Assert.AreEqual(domainEntity.Babies[0].Name, actual.BabyName);
			Assert.AreEqual(domainEntity.Babies[0].BirthDateTime, actual.BabyBirhDate);
			Assert.AreEqual(domainEntity.Date, actual.Date);
			Assert.AreEqual(domainEntity.Event.Id, actual.EventId);
			Assert.AreEqual(domainEntity.Event.Description, actual.EventDescription);
			Assert.IsNull(actual.LocationId);
			Assert.IsNull(actual.LocationDescription);
		}
	}
}

