using NUnit.Framework;
using System;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Framework.Domain;
using Domain.Moment;

namespace Infrastructure.Repositories.UnitTests.SqliteNet.Mapper
{
	[TestFixture()]
	public class SqliteNetEventMapperTest
	{
		[Test()]
		public void ToRepository_Null_Null()
		{
			Assert.AreEqual(null,  new SqliteNetEventMapper().ToRepositoryEntity(null));
		}

		[Test()]
		public void ToDomainEntity_Null_Null()
		{
			Assert.AreEqual(null, new SqliteNetEventMapper().ToDomainEntity(null));
		}

		[Test()]
		public void ToDomainEntity_RepositoryEntity_DomainEntity()
		{
			var repositoryEntity = new EventData()
			{
				Id = "1",
				Description = "Descrição"
			};

			var actual = new SqliteNetEventMapper().ToDomainEntity(repositoryEntity);
			Assert.AreEqual(repositoryEntity.Id, actual.Id);
			Assert.AreEqual(repositoryEntity.Description, actual.Description);
		}

		[Test()]
		public void ToRepositoryEntity_DomainEntity_RepositoryEntity()
		{
			var domainEntity = new Event()
			{
				Id = "1",
				Description = "Descrição"
			};

			var actual = new SqliteNetEventMapper().ToRepositoryEntity(domainEntity);
			Assert.AreEqual(domainEntity.Id, actual.Id);
			Assert.AreEqual(domainEntity.Description, actual.Description);
		}
	}
}

