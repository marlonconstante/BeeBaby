using System;
using Skahal.Infrastructure.Framework.Domain;

namespace Domain.User
{
	/// <summary>
	/// User.
	/// </summary>
	public class User : EntityWithIdBase<string>, IAggregateRoot
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