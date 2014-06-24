using System.Linq;
using NUnit.Framework;
using Skahal.Infrastructure.Framework.Repositories;
using Domain.Moment;
using Infrastructure.Repositories.Dropbox;
using Domain.Baby;
using System;
using System.Collections.Generic;
using Infrastructure.Repositories.Dropbox.Entities;
using Infrastructure.Repositories.Dropbox.Mapper;
using Infrastructure.Repositories.Memory;
using Skahal.Infrastructure.Framework.Commons;

namespace Infrastructure.Repositories.FunctionalTest.Repositories
{
	[TestFixture]
	public class DropboxRepositoryBaseTest
	{
		private MemoryUnitOfWork m_unitOfWork;
		private MemoryEventRepository m_repository;
		private MemoryLocationRepository m_LocationRepository;
		private MemoryBabyRepository m_babyRepository;


		[TestFixtureSetUp]
		public void InitializeTest()
		{ 
			m_unitOfWork = new MemoryUnitOfWork();
			m_repository = new MemoryEventRepository(m_unitOfWork);
			m_LocationRepository = new MemoryLocationRepository(m_unitOfWork);
			m_babyRepository = new MemoryBabyRepository(m_unitOfWork);

			m_repository.Add(new Event()
			{
				Id = "1",
				Description = "Primeiro Sorriso"

			});
			m_repository.Add(new Event()
			{
				Id = "2",
				Description = "Primeiro Banho"

			});
			m_repository.Add(new Event()
			{
				Id = "3",
				Description = "Primeiro Passeio"

			});

			var baby = new Baby
			{
				Id = "1",
				Name = "Baby 12"
			};

			m_babyRepository.Add(baby);

			m_unitOfWork.Commit();

			DependencyService.Register<IUnitOfWork>(m_unitOfWork);
			DependencyService.Register<IEventRepository>(m_repository);
			DependencyService.Register<ILocationRepository>(m_LocationRepository);
			DependencyService.Register<IBabyRepository>(m_babyRepository);
		}

		[Test]
		public void PersistNewItem_Item_PersistedOnDropbox()
		{
			var target = new DropboxRepositoryBase<Moment, MomentData>("test2", new DropboxMomentMapper());
			target.ClearAll();
			target.SetUnitOfWork(m_unitOfWork);

			var eventData = new Event
			{
				Id = "1",
				Description = "Descrição"
			};

			var local = new Coordinates
			{
				Latitude = 12,
				Longitude = 34
			};

			var baby = new Baby
			{
				Id = "1",
				Name = "Baby 12"
			};

			var moment = new Moment
			{
				Id = "1",
				Description = "Descrição",
				Event = eventData,
				Position = local,
				Date = DateTime.Now,
				Babies = new List<Baby> { baby }
			};
				
			target.Add(moment);

			var originalCount = target.CountAll();
			m_unitOfWork.Commit();

			Assert.AreEqual(originalCount + 1, target.CountAll());
		}

		[Test]
		public void CountAll_Filter_FiltedResults()
		{
			var target = new DropboxRepositoryBase<Moment, MomentData>("test2", new DropboxMomentMapper());
			target.ClearAll();
			target.SetUnitOfWork(m_unitOfWork);

			var eventData = new Event
			{
				Id = "1",
				Description = "Descrição"
			};

			var local = new Coordinates
			{
				Latitude = 12,
				Longitude = 34
			};

			var baby = new Baby
			{
				Id = "1",
				Name = "Baby 12"
			};

			var moment = new Moment
			{
				Id = "1",
				Description = "Descrição",
				Event = eventData,
				Position = local,
				Date = DateTime.Now,
				Babies = new List<Baby> { baby }
			};

			target.Add(moment);

			var moment2 = new Moment
			{
				Id = "2",
				Description = "Descrição",
				Event = eventData,
				Position = local,
				Date = DateTime.Now,
				Babies = new List<Baby> { baby }
			};

			target.Add(moment2);

			m_unitOfWork.Commit();

			Assert.AreEqual(2, target.CountAll());
			Assert.AreEqual(2, target.CountAll(c => c.Description.StartsWith("Desc", StringComparison.InvariantCultureIgnoreCase)));
			Assert.AreEqual(2, target.CountAll(c => c.Description.Contains("scr")));
			Assert.AreEqual(1, target.CountAll(c => c.Description.EndsWith(" 1", StringComparison.InvariantCultureIgnoreCase)));
			Assert.AreEqual(1, target.CountAll(c => c.Description.EndsWith(" 2", StringComparison.InvariantCultureIgnoreCase)));
		}

		//
		//        [Test]
		//        public void FindAll_Filter_FiltedResults()
		//        {
		//            var unitOfWork = new MemoryUnitOfWork();
		//            var target = new DropboxRepositoryBase<ContainsTextRule>("TEST");
		//            target.ClearAll();
		//            target.SetUnitOfWork(unitOfWork);
		//
		//            var rule = new ContainsTextRule("TEST_1");
		//            target.Add(rule);
		//
		//            rule = new ContainsTextRule("TEST_2");
		//            target.Add(rule);
		//
		//            unitOfWork.Commit();
		//
		//            Assert.AreEqual(2, target.FindAll().Count());
		//            Assert.AreEqual(2, target.FindAll(0, 2).Count());
		//            Assert.AreEqual(1, target.FindAll(0, 1).Count());
		//            Assert.AreEqual(1, target.FindAll(1, 2).Count());
		//            Assert.AreEqual(2, target.FindAll(c => c.Text.StartsWith("TEST")).Count());
		//            Assert.AreEqual(2, target.FindAll(c => c.Text.Contains("_")).Count());
		//            Assert.AreEqual(1, target.FindAll(c => c.Text.EndsWith("_1")).Count());
		//            Assert.AreEqual(1, target.FindAll(c => c.Text.EndsWith("_2")).Count());
		//        }
		//

		[Test]
		public void Pesists_Item_Persisted()
		{
			var target = new DropboxRepositoryBase<Moment, MomentData>("test2", new DropboxMomentMapper());
			target.ClearAll();
			target.SetUnitOfWork(m_unitOfWork);

			var eventData = new Event
			{
				Id = "1",
				Description = "Descrição"
			};

			var local = new Coordinates
			{
				Latitude = 12,
				Longitude = 34
			};

			var baby = new Baby
			{
				Id = "1",
				Name = "Baby 12"
			};

			var moment = new Moment
			{
				Id = "1",
				Description = "Descrição",
				Event = eventData,
				Position = local,
				Date = DateTime.Now,
				Babies = new List<Baby> { baby }
			};

			target.Add(moment);

			var moment2 = new Moment
			{
				Id = "2",
				Description = "Descrição",
				Event = eventData,
				Position = local,
				Date = DateTime.Now,
				Babies = new List<Baby> { baby }
			};

			target.Add(moment2);

			m_unitOfWork.Commit();
		
			var actual = target.FindAll().ToList();
			Assert.AreEqual(2, actual.Count);
		}


		[Test]
		public void Pesists_Item_Persisted2()
		{
			var target = new DropboxRepositoryBase<Moment, MomentData>("atest", new DropboxMomentMapper());
			target.SetUnitOfWork(m_unitOfWork);

			var count = target.CountAll();


			var eventData = new Event
			{
				Id = "1",
				Description = "Descrição"
			};

			var local = new Coordinates
			{
				Latitude = 12,
				Longitude = 34
			};

			var baby = new Baby
			{
				Id = "1",
				Name = "Baby 12"
			};

			var moment = new Moment
			{
				Id = Guid.NewGuid().ToString(),
				Description = "Descrição 1",
				Event = eventData,
				Position = local,
				Date = DateTime.Now,
				Babies = new List<Baby> { baby }
			};

			target.Add(moment);

			var moment2 = new Moment
			{
				Id = Guid.NewGuid().ToString(),
				Description = "Descrição 2",
				Event = eventData,
				Position = local,
				Date = DateTime.Now,
				Babies = new List<Baby> { baby }
			};

			target.Add(moment2);

			m_unitOfWork.Commit();

			var actual = target.FindAll().ToList();
			Assert.AreEqual(count + 2, actual.Count);
		}
	}
}
