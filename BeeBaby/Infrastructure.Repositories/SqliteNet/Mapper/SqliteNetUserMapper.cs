using System;
using Domain.User;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Repositories.Commons;

namespace Infrastructure.Repositories.SqliteNet.Mapper
{
	public class SqliteNetUserMapper : IMapper<User, UserData>
	{
		#region IMapper implementation

		public User ToDomainEntity(UserData source)
		{
			User result = null;

			if (source != null)
			{
				result = new User();
				result.Id = source.Id;
				result.Email = source.Email;
			}

			return result;
		}

		public UserData ToRepositoryEntity(User source)
		{
			UserData result = null;

			if (source != null)
			{
				result = new UserData();
				result.Id = source.Id;
				result.Email = source.Email;
			}

			return result;
		}

		#endregion
	}
}