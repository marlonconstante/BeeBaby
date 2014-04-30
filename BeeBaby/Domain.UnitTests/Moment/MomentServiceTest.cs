using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Media;
using Domain.Baby;
using Domain.Moment;
using Infrastructure.Repositories.Memory;
using NUnit.Framework;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Commons;

namespace Domain.UnitTests.Moment
{
	[TestFixture()]
	public class MomentServiceTest
	{
		#region Fields
		private MomentService m_target;
		private MemoryUnitOfWork m_unitOfWork;
		private MemoryMomentRepository m_repository;
		#endregion

		#region Initialize
		[SetUp]
		public void InitializeTest()
		{ 
			m_unitOfWork = new MemoryUnitOfWork();
			m_repository = new MemoryMomentRepository(m_unitOfWork);
		
			var momentKind = new Event()
			{
				Id = "1",
				Description = "Primeiro qualquer coisa"
			};

			m_repository.Add(new Domain.Moment.Moment()
			{
				Description = "Descrição livre momento 1",
				Location = new Location(),
				Medias = new List<MediaBase>()
				{
					new Photo() 
				},
				Event = momentKind
			});
			m_repository.Add(new Domain.Moment.Moment()
			{
				Description = "Descrição livre momento 2",
				Location = new Location(),
				Medias = new List<MediaBase>()
				{
					new Photo() 
				},
				Event = momentKind
			});
			m_repository.Add(new Domain.Moment.Moment()
			{
				Description = "Descrição livre momento 3",
				Location = new Location(),
				Medias = new List<MediaBase>()
				{
					new Photo() 
				},
				Event = momentKind
			});

			m_unitOfWork.Commit();

			m_target = new MomentService(m_repository, m_unitOfWork);
		}
		#endregion

		[Test()]
		public void Constructor_NoArgsDIRegistered_DIInstances()
		{
			DependencyService.Register<IUnitOfWork>(new MemoryUnitOfWork());
			DependencyService.Register<IMomentRepository>(new MemoryMomentRepository(null));

			var baby = new Domain.Baby.Baby
			{
				Id = "1"
			};

			var target = new MomentService();

			Assert.AreEqual(0, target.GetAllMoments(baby).Count());
		}

		[Test()]
		public void GetAllMoments_NoArgs_AllMoments()
		{
			var baby = new Domain.Baby.Baby
			{
				Id = "1"
			};

			var actual = m_target.GetAllMoments(baby);
			Assert.AreEqual(3, actual.Count());
		}

		[Test()]
		public void CreateMoment_NoArgs_MomentCreated()
		{
			var actual = m_target.CreateMoment();

			Assert.AreEqual(4, m_repository.CountAll());
			Assert.IsNotNull(actual.Id);
		}
	}
}

