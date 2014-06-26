using System;

namespace Infrastructure.Repositories.SqliteNet.Entities
{
	public class UserData : DataBase
	{
		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>The email.</value>
		public string Email
		{
			get;
			set;
		}
	}
}