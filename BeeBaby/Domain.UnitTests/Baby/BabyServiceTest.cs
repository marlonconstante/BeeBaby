using NUnit.Framework;
using System;
using System.Linq;
using Domain.Baby;
using Skahal.Infrastructure.Framework.Repositories;
using Infrastructure.Repositories.Memory;
using Skahal.Infrastructure.Framework.Commons;

namespace Domain.UnitTests.Baby
{
	[TestFixture()]
	public class BabyServiceTest
	{
		#region Fields
		private BabyService m_target;
		private MemoryUnitOfWork m_unitOfWork;
		private MemoryBabyRepository m_repository;
		#endregion

		#region Initialize
		[SetUp]
		public void InitializeTest()
		{ 
			m_unitOfWork = new MemoryUnitOfWork();
			m_repository = new MemoryBabyRepository(m_unitOfWork);


			m_repository.Add(new Domain.Baby.Baby()
			{
				BirthDateTime = new DateTime(2014, 01, 01, 01, 01, 01),
				Name = "Bebê inicio 2014"
			});


			m_unitOfWork.Commit();

			m_target = new BabyService(m_repository, m_unitOfWork);
		}
		#endregion

		[Test()]
		public void Constructor_NoArgsDIRegistered_DIInstances()
		{
			DependencyService.Register<IUnitOfWork>(new MemoryUnitOfWork());
			DependencyService.Register<IBabyRepository>(new MemoryBabyRepository(null));
			var target = new BabyService();

			var actual = target.GetBaby();
			 
			Assert.IsNull(actual);
		}

		[Test()]
		public void GetBaby_NoArgs_ABaby()
		{
			var actual = m_target.GetBaby();
			Assert.IsNotNull(actual);
		}

		[Test()]
		public void SaveBaby_NoArgs_BabySaved()
		{
			var date = DateTime.Now;
			var baby = new Domain.Baby.Baby()
			{
				Name = "Novo Bebê",
				BirthDateTime = date
			};

			var actual = m_target.SaveBaby(baby);

			var savedBaby = m_repository.FindAll(b => b.Name.Equals(baby.Name)).First();

			Assert.AreEqual(savedBaby.Name, actual.Name);
			Assert.AreEqual(savedBaby.BirthDateTime, actual.BirthDateTime);
			Assert.AreEqual(savedBaby.Id, actual.Id);
		}
	}
}

