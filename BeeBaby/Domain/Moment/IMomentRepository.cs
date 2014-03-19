using System;
using Infrastructure.Framework.Repositories;

namespace Domain.Moment
{
	/// <summary>
	/// Defines an Interface for a moment repository.
	/// </summary>
	public interface IMomentRepository : IRepository<Moment>
	{
	}
}