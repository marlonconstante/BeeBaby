using System;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Domain;

namespace Domain.System
{
	public class SystemParameterService: ServiceBase<SystemParameter, ISystemParameterRepository, IUnitOfWork>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.System.SystemParameterService"/> class.
		/// </summary>
		/// <param name="mainRepository">Main repository.</param>
		/// <param name="unitOfWork">Unit of work.</param>
		public SystemParameterService(ISystemParameterRepository mainRepository, IUnitOfWork unitOfWork)
			: base(mainRepository, unitOfWork)
		{
		}

		public SystemParameterService()
		{
		}
	}
}

