using System;
using NUnit.Framework;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Infrastructure.Repositories.SqliteNet.Entities;
using Domain.Moment;

namespace Infrastructure.Repositories.UnitTests.SqliteNet.Mapper
{
	[TestFixture()]
	public class SqliteNetLocationMapperTest
	{
		[Test()]
		public void ToRepository_Null_Null()
		{
			Assert.AreEqual(null, new SqliteNetLocationMapper().ToRepositoryEntity(null));
		}

		[Test()]
		public void ToDomainEntity_Null_Null()
		{
			Assert.AreEqual(null, new SqliteNetLocationMapper().ToDomainEntity(null));
		}

		[Test()]
		public void ToDomainEntity_RepositoryEntity_DomainEntity()
		{
			var repositoryEntity = new LocationData()
			{
				Id = "1",
				Name = "Nome",
				Longitude = 1.1f,
				Latitude = 1.2f
			};

			var actual = new SqliteNetLocationMapper().ToDomainEntity(repositoryEntity);
			Assert.AreEqual(repositoryEntity.Id, actual.Id);
			Assert.AreEqual(repositoryEntity.Name, actual.Name);
			Assert.AreEqual(repositoryEntity.Longitude, actual.Position.Longitude);
			Assert.AreEqual(repositoryEntity.Latitude, actual.Position.Latitude);
		}

		[Test()]
		public void ToRepositoryEntity_DomainEntity_RepositoryEntity()
		{
			var domainEntity = new Location()
			{
				Id = "1",
				Name = "Nome",
				Position = new GlobalPosition()
				{
					Longitude = 1.1f,
					Latitude = 1.2f
				}
			};

			var actual = new SqliteNetLocationMapper().ToRepositoryEntity(domainEntity);
			Assert.AreEqual(domainEntity.Id, actual.Id);
			Assert.AreEqual(domainEntity.Name, actual.Name);
			Assert.AreEqual(domainEntity.Position.Longitude, actual.Longitude);
			Assert.AreEqual(domainEntity.Position.Latitude, actual.Latitude);
		}
	}
}

