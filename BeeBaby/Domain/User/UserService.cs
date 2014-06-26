using System;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;

namespace Domain.User
{
	/// <summary>
	/// User service.
	/// </summary>
	public class UserService : ServiceBase<User, IUserRepository, IUnitOfWork>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.User.UserService"/> class.
		/// </summary>
		/// <param name="mainRepository">Main repository.</param>
		/// <param name="unitOfWork">Unit of work.</param>
		public UserService(IUserRepository mainRepository, IUnitOfWork unitOfWork)
			: base(mainRepository, unitOfWork)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.User.UserService"/> class.
		/// </summary>
		public UserService()
		{
		}

		/// <summary>
		/// Saves the user.
		/// </summary>
		/// <param name="user">User.</param>
		public void SaveUser(User user)
		{
			MainRepository[user.Id] = user;
			UnitOfWork.Commit();
		}

		/// <summary>
		/// Gets the user.
		/// </summary>
		/// <returns>The user.</returns>
		public User GetUser()
		{
			return MainRepository.FindFirst();
		}
	}
}