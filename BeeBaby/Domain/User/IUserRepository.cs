using System;
using Skahal.Infrastructure.Framework.PCL.Repositories;

namespace Domain.User
{
	/// <summary>
	/// I user repository.
	/// </summary>
	public interface IUserRepository : IRepository<User>
	{
	}
}