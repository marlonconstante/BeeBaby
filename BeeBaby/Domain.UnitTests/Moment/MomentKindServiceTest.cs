using NUnit.Framework;
using System;
using System.Linq;
using Skahal.Infrastructure.Framework.Repositories;
using Infrastructure.Repositories.Memory;
using Domain.Moment;
using Skahal.Infrastructure.Framework.Commons;

namespace Domain.UnitTests.Moment
{
	[TestFixture()]
	public class MomentKindServiceTest
	{
		#region Fields
		private MomentKindService m_target;
		private MemoryUnitOfWork m_unitOfWork;
		private MemoryMomentKindRepository m_repository;
		#endregion

		#region Initialize
		[TestFixtureSetUp]
		public void InitializeTest()
		{ 
			m_unitOfWork = new MemoryUnitOfWork();
			m_repository = new MemoryMomentKindRepository(m_unitOfWork);

			m_repository.Add(new MomentKind()
			{
				Description = "Primeiro Sorriso"
			
			});
			m_repository.Add(new MomentKind()
			{
				Description = "Primeiro Banho"

			});
			m_repository.Add(new MomentKind()
			{
				Description = "Primeiro Passeio"

			});

			m_unitOfWork.Commit();

			m_target = new MomentKindService(m_repository, m_unitOfWork);
		}
		#endregion

		[Test()]
		public void Constructor_NoArgsDIRegistered_DIInstances()
		{
			DependencyService.Register<IUnitOfWork>(new MemoryUnitOfWork());
			DependencyService.Register<IMomentKindRepository>(new MemoryMomentKindRepository(null));
			var target = new MomentKindService();

			Assert.AreEqual(0, target.GetAllMomentKinds().Count());
		}

		[Test()]
		public void GetAllMomentKinds_NoArgs_AllMomentKinds()
		{
			var actual = m_target.GetAllMomentKinds();
			Assert.AreEqual(3, actual.Count());
		}
	}
}

