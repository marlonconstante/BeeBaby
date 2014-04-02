using System;
using Skahal.Infrastructure.Framework.Repositories;

namespace Domain.Moment
{
	/// <summary>
	/// Defines an Interface for a moment kind repository.
	/// </summary>
	public interface IEventRepository : IRepository<Event>
	{
	}
}

