using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Media;
using Domain.Moment;
using Infrastructure.Repositories.Memory;
using NUnit.Framework;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Repositories;
using Infrastructure.Framework.Resources;

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
		[TestFixtureSetUp]
		public void InitializeTest()
		{ 
			m_unitOfWork = new MemoryUnitOfWork();
			m_repository = new MemoryMomentRepository(m_unitOfWork);
		
			var momentKind = new MomentKind()
			{
				Id = "1",
				Description = "Primeiro qualquer coisa"
			};

			m_repository.Add(new Domain.Moment.Moment()
			{
				Description = "Descrição livre momento 1",
				Local = new Location(),
				Medias = new List<MediaBase>()
				{
					new Photo() 
				},
				Title = momentKind
			});
			m_repository.Add(new Domain.Moment.Moment()
			{
				Description = "Descrição livre momento 2",
				Local = new Location(),
				Medias = new List<MediaBase>()
				{
					new Photo() 
				},
				Title = momentKind
			});
			m_repository.Add(new Domain.Moment.Moment()
			{
				Description = "Descrição livre momento 2",
				Local = new Location(),
				Medias = new List<MediaBase>()
				{
					new Photo() 
				},
				Title = momentKind
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
			var target = new MomentService();

			Assert.AreEqual(0, target.GetAllMoments().Count());
		}

		[Test()]
		public void GetAllMoments_NoArgs_AllMoments()
		{
			var actual = m_target.GetAllMoments();
			Assert.AreEqual(3, actual.Count());
		}
	}
}

