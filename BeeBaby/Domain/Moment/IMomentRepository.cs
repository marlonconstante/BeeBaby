using System;
using Skahal.Infrastructure.Framework.Repositories;
using System.Collections.Generic;

namespace Domain.Moment
{
	/// <summary>
	/// Defines an Interface for a moment repository.
	/// </summary>
	public interface IMomentRepository : IRepository<Moment>
	{
		int RemoveInvalidMoments();

		int CountValidMoments();
	}
}