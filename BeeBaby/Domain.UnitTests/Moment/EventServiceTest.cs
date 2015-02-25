using NUnit.Framework;
using System.Linq;
using Domain.Moment;
using Infrastructure.Repositories.Memory;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using Skahal.Infrastructure.Framework.PCL.Commons;

namespace Domain.UnitTests.Moment
{
	[TestFixture()]
	public class EventServiceTest
	{
		#region Fields
		private EventService m_target;
		private MemoryUnitOfWork m_unitOfWork;
		private MemoryEventRepository m_repository;
		#endregion

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

			m_target = new EventService(m_repository, m_unitOfWork);
		}
		#endregion

		[Test()]
		public void Constructor_NoArgsDIRegistered_DIInstances()
		{
			DependencyService.Register<IUnitOfWork>(new MemoryUnitOfWork());
			DependencyService.Register<IEventRepository>(new MemoryEventRepository(null));
			var target = new EventService();

			Assert.AreEqual(0, target.GetAllEvents().Count());
		}

		[Test()]
		public void GetAllMomentKinds_NoArgs_AllMomentKinds()
		{
			var actual = m_target.GetAllEvents();
			Assert.AreEqual(3, actual.Count());
		}
	}
}

