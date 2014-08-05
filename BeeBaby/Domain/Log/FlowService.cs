using System;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Domain;

namespace Domain.Log
{
	/// <summary>
	/// Flow service.
	/// </summary>
	public class FlowService : ServiceBase<Flow, IFlowRepository, IUnitOfWork>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Log.FlowService"/> class.
		/// </summary>
		/// <param name="mainRepository">Main repository.</param>
		/// <param name="unitOfWork">Unit of work.</param>
		public FlowService(IFlowRepository mainRepository, IUnitOfWork unitOfWork)
			: base(mainRepository, unitOfWork)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Log.FlowService"/> class.
		/// </summary>
		public FlowService()
		{
		}

		/// <summary>
		/// Log the specified flow.
		/// </summary>
		/// <param name="flow">Flow.</param>
		public void Log(Flow flow)
		{
		}
	}
}