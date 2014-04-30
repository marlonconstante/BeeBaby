using System;
using NUnit.Framework;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Infrastructure.Repositories.SqliteNet.Entities;
using Domain.Moment;
using Infrastructure.Repositories.Memory;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Commons;
using System.Collections.Generic;
using Domain.Baby;

namespace Infrastructure.Repositories.UnitTests.SqliteNet.Mapper
{
	[TestFixture()]
	public class SqliteNetMomentMapperTest
	{
		MemoryEventRepository m_repository;
		MemoryUnitOfWork m_unitOfWork;

		#region Initialize

		[TestFixtureSetUp]
		public void InitializeTest()
		{ 
			m_unitOfWork = new MemoryUnitOfWork();
			m_repository = new MemoryEventRepository(m_unitOfWork);

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

			m_unitOfWork.Commit();

			DependencyService.Register<IUnitOfWork>(m_unitOfWork);
			DependencyService.Register<IEventRepository>(m_repository);
		}

		#endregion

		[Test()]
		public void ToRepository_Null_Null()
		{
			Assert.AreEqual(null, new SqliteNetMomentMapper().ToRepositoryEntity(null));
		}

		[Test()]
		public void ToDomainEntity_Null_Null()
		{
			Assert.AreEqual(null, new SqliteNetMomentMapper().ToDomainEntity(null));
		}

		[Test()]
		public void ToDomainEntity_RepositoryEntity_DomainEntity()
		{
			var eventData = new EventData()
			{
				Id = "1",
				Description = "Primeiro Sorriso"
			};

			var babyData = new BabyData()
			{
				Id = "1",
				Name = "Baby 1"
			};

			var repositoryEntity = new MomentData()
			{
				Id = "1",
				Description = "Descrição",
				Event = eventData,
				EventId = eventData.Id,
				Latitude = 12,
				Longitude = 34,
				Date = DateTime.Now,
				Babies = new List<BabyData> { babyData }
			};

			var actual = new SqliteNetMomentMapper().ToDomainEntity(repositoryEntity);
			Assert.AreEqual(repositoryEntity.Id, actual.Id);
			Assert.AreEqual(repositoryEntity.Description, actual.Description);
			Assert.IsNotNull(actual.Event);
			Assert.AreEqual(eventData.Id, actual.Event.Id);
			Assert.AreEqual(repositoryEntity.Date, actual.Date);
			Assert.AreEqual(eventData.Description, actual.Event.Description);
			Assert.AreEqual(repositoryEntity.Latitude, actual.Position.Latitude);
			Assert.AreEqual(repositoryEntity.Longitude, actual.Position.Longitude);
			Assert.IsNotNull(actual.Babies);
			Assert.AreEqual(babyData.Id, actual.Babies[0].Id);
			Assert.AreEqual(babyData.Name, actual.Babies[0].Name);
		}

		[Test()]
		public void ToRepositoryEntity_DomainEntity_RepositoryEntity()
		{
			var eventData = new Event
			{
				Id = "1",
				Description = "Descrição"
			};

			var local = new GlobalPosition
			{
				Latitude = 12,
				Longitude = 34
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
				Position = local,
				Date = DateTime.Now,
				Babies = new List<Baby> { baby }
			};

			var actual = new SqliteNetMomentMapper().ToRepositoryEntity(domainEntity);
			Assert.AreEqual(domainEntity.Id, actual.Id);
			Assert.AreEqual(domainEntity.Description, actual.Description);

			Assert.IsNotNull(actual.Event);
			Assert.AreEqual(domainEntity.Id, actual.Event.Id);
			Assert.AreEqual(domainEntity.Date, actual.Date);
			Assert.AreEqual(domainEntity.Description, actual.Event.Description);
			Assert.AreEqual(domainEntity.Position.Latitude, actual.Latitude);
			Assert.AreEqual(domainEntity.Position.Longitude, actual.Longitude);

			Assert.IsNotNull(actual.Babies);
			Assert.AreEqual(baby.Id, actual.Babies[0].Id);
			Assert.AreEqual(baby.Name, actual.Babies[0].Name);

		}

		[Test()]
		public void ToRepositoryEntity_PositionNull_RepositoryEntity()
		{
			var eventData = new Event()
			{
				Id = "1",
				Description = "Descrição"
			};
					
			var domainEntity = new Moment()
			{
				Id = "1",
				Description = "Descrição",
				Event = eventData
			};

			var actual = new SqliteNetMomentMapper().ToRepositoryEntity(domainEntity);
			Assert.AreEqual(domainEntity.Id, actual.Id);
			Assert.AreEqual(domainEntity.Description, actual.Description);

			Assert.IsNotNull(actual.Event);
			Assert.AreEqual(domainEntity.Id, actual.Event.Id);
			Assert.AreEqual(domainEntity.Description, actual.Event.Description);
		}
	}
}

