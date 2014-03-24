using System;
using Infrastructure.Framework.Repositories;
using Infrastructure.Framework.Commons;
using Domain.Moment;
using Infrastructure.Repositories.Memory;

namespace Application
{
	public static class DomainConfig
	{
		/// <summary>
		/// Registers the dependencies.
		/// </summary>
		public static void RegisterDependencies()
		{
			var unitOfWork = new MemoryUnitOfWork();
			DependencyService.Register<IUnitOfWork>(unitOfWork);
			DependencyService.Register<IMomentRepository>(new MemoryMomentRepository(unitOfWork));

			var eventRepository = new MemoryEventRepository(unitOfWork);
			MockEvents(unitOfWork, eventRepository);
			DependencyService.Register<IEventRepository>(eventRepository);
		}

		static void MockEvents(MemoryUnitOfWork unitOfWork, MemoryEventRepository eventRepository)
		{
			eventRepository.Add(new Event() {
				Description = "Primeiro Sorriso"
			});
			eventRepository.Add(new Event() {
				Description = "Primeiro Banho"
			});
			eventRepository.Add(new Event() {
				Description = "Primeiro Passeio"
			});
			unitOfWork.Commit();
		}
	}
}

