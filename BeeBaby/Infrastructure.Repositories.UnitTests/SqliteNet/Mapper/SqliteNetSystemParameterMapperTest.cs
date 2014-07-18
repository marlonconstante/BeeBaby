using System;
using NUnit.Framework;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Configuration;

namespace Infrastructure.Repositories.UnitTests.SqliteNet.Mapper
{
	[TestFixture()]
	public class SqliteNetSystemParameterMapperTest
	{
		[Test()]
		public void ToRepository_Null_Null()
		{
			Assert.AreEqual(null, new SqliteNetSystemParameterMapper().ToRepositoryEntity(null));
		}

		[Test()]
		public void ToDomainEntity_Null_Null()
		{
			Assert.AreEqual(null, new SqliteNetSystemParameterMapper().ToDomainEntity(null));
		}

		[Test()]
		public void ToDomainEntity_RepositoryEntity_DomainEntity()
		{
			var repositoryEntity = new SystemParameterData()
			{
				Id = "1",
				Name = "param1",
				Value = "11"
			};

			var actual = new SqliteNetSystemParameterMapper().ToDomainEntity(repositoryEntity);
			Assert.AreEqual(repositoryEntity.Id, actual.Id);
			Assert.AreEqual(repositoryEntity.Name, actual.Name);
			Assert.AreEqual(repositoryEntity.Value, actual.Value);
		}

		[Test()]
		public void ToRepositoryEntity_DomainEntity_RepositoryEntity()
		{
			var domainEntity = new SystemParameter()
			{
				Id = "1",
				Name = "param1",
				Value = "11"
			};

			var actual = new SqliteNetSystemParameterMapper().ToRepositoryEntity(domainEntity);
			Assert.AreEqual(domainEntity.Id, actual.Id);
			Assert.AreEqual(domainEntity.Name, actual.Name);
			Assert.AreEqual(domainEntity.Value, actual.Value);
		}
	}}

