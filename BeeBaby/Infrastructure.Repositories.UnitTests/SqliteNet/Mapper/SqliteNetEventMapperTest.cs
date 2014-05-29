using NUnit.Framework;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Infrastructure.Repositories.SqliteNet.Entities;
using Domain.Moment;

namespace Infrastructure.Repositories.UnitTests.SqliteNet.Mapper
{
	[TestFixture()]
	public class SqliteNetEventMapperTest
	{
		[Test()]
		public void ToRepository_Null_Null()
		{
			Assert.AreEqual(null, new SqliteNetEventMapper().ToRepositoryEntity(null));
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
				Description = "Descrição",
				StartAge = 1,
				EndAge = 2,
				Kind = 1,
				Tag = 2
			};

			var actual = new SqliteNetEventMapper().ToDomainEntity(repositoryEntity);
			Assert.AreEqual(repositoryEntity.Id, actual.Id);
			Assert.AreEqual(repositoryEntity.Description, actual.Description);
			Assert.AreEqual(repositoryEntity.StartAge, actual.StartAge);
			Assert.AreEqual(repositoryEntity.EndAge, actual.EndAge);
			Assert.AreEqual(TagType.Fraldas, actual.Tag);
			Assert.AreEqual(EventType.Everyday, actual.Kind);
		}

		[Test()]
		public void ToRepositoryEntity_DomainEntity_RepositoryEntity()
		{
			var domainEntity = new Event()
			{
				Id = "1",
				Description = "Descrição",
				StartAge = 1,
				EndAge = 2,
				Kind = EventType.Everyday,
				Tag = TagType.Eventos
			};

			var actual = new SqliteNetEventMapper().ToRepositoryEntity(domainEntity);
			Assert.AreEqual(domainEntity.Id, actual.Id);
			Assert.AreEqual(domainEntity.Description, actual.Description);
			Assert.AreEqual(domainEntity.StartAge, actual.StartAge);
			Assert.AreEqual(domainEntity.EndAge, actual.EndAge);
			Assert.AreEqual(1, actual.Kind);
			Assert.AreEqual(13, actual.Tag);
		}
	}
}

