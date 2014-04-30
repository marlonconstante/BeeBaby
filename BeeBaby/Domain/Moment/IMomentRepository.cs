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
		/// <summary>
		/// Finds the moment by baby.
		/// </summary>
		/// <returns>The by baby.</returns>
		/// <param name="babyId">Baby identifier.</param>
		IEnumerable<Moment> FindByBaby(string babyId);
	}

}