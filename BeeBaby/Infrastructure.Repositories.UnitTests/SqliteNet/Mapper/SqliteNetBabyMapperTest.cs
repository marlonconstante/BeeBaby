using NUnit.Framework;
using System;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Infrastructure.Repositories.SqliteNet.Entities;
using Domain.Baby;

namespace Infrastructure.Repositories.UnitTests.SqliteNet.Mapper
{
	[TestFixture()]
	public class SqliteNetBabyMapperTest
	{
		[Test()]
		public void ToRepository_Null_Null()
		{
			Assert.AreEqual(null,  new SqliteNetBabyMapper().ToRepositoryEntity(null));
		}

		[Test()]
		public void ToDomainEntity_Null_Null()
		{
			Assert.AreEqual(null, new SqliteNetBabyMapper().ToDomainEntity(null));
		}

		[Test()]
		public void ToDomainEntity_RepositoryEntity_DomainEntity()
		{
			var date = DateTime.Now;

			var repositoryEntity = new BabyData()
			{
				Id = "1",
				Name = "Nome",
				BirthDateTime = date,
				Gender = 1
			};

			var actual = new SqliteNetBabyMapper().ToDomainEntity(repositoryEntity);
			Assert.AreEqual(repositoryEntity.Id, actual.Id);
			Assert.AreEqual(repositoryEntity.Name, actual.Name);
			Assert.AreEqual(repositoryEntity.BirthDateTime, actual.BirthDateTime);
			Assert.AreEqual(Gender.Male, actual.Gender);

		}

		[Test()]
		public void ToRepositoryEntity_DomainEntity_RepositoryEntity()
		{
			var date = DateTime.Now;

			var domainEntity = new Baby()
			{
				Id = "1",
				Name = "Nome",
				BirthDateTime = date,
				Gender =  Gender.Female
			};

			var actual = new SqliteNetBabyMapper().ToRepositoryEntity(domainEntity);
			Assert.AreEqual(domainEntity.Id, actual.Id);
			Assert.AreEqual(domainEntity.Name, actual.Name);
			Assert.AreEqual(domainEntity.BirthDateTime, actual.BirthDateTime);
			Assert.AreEqual(2, actual.Gender);
		}
	}
}

