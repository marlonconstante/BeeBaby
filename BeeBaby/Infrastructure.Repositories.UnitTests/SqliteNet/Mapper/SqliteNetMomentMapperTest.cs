﻿using System;
using NUnit.Framework;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Infrastructure.Repositories.SqliteNet.Entities;
using Domain.Moment;

namespace Infrastructure.Repositories.UnitTests.SqliteNet.Mapper
{
	[TestFixture()]
	public class SqliteNetMomentMapperTest
	{
		[Test()]
		public void ToRepository_Null_Null()
		{
			Assert.AreEqual(null,  new SqliteNetMomentMapper().ToRepositoryEntity(null));
		}

		[Test()]
		public void ToDomainEntity_Null_Null()
		{
			Assert.AreEqual(null, new SqliteNetMomentMapper().ToDomainEntity(null));
		}

		[Test()]
		public void ToDomainEntity_RepositoryEntity_DomainEntity()
		{
			var eventData =  new EventData()
			{
				Id = "1",
				Description = "Descrição"
			};

			var repositoryEntity = new MomentData()
			{
				Id = "1",
				Description = "Descrição",
				Event = eventData,
				EventId = eventData.Id
			};

			var actual = new SqliteNetMomentMapper().ToDomainEntity(repositoryEntity);
			Assert.AreEqual(repositoryEntity.Id, actual.Id);
			Assert.AreEqual(repositoryEntity.Description, actual.Description);

			Assert.IsNotNull(actual.Event);
			Assert.AreEqual(eventData.Id, actual.Event.Id);
			Assert.AreEqual(eventData.Description, actual.Event.Description);
		}

		[Test()]
		public void ToRepositoryEntity_DomainEntity_RepositoryEntity()
		{
			var eventData =  new Event()
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
