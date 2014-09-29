using System;
using Parse;

namespace Infrastructure.Systems.Domain
{
	public class FriendshipShared : ParseShared
	{
		public FriendshipShared() : base("FriendshipShared")
		{
		}

		public FriendshipShared(ParseObject parseObject) : base(parseObject)
		{
		}

		/// <summary>
		/// Gets or sets the user email.
		/// </summary>
		/// <value>The user email.</value>
		public string UserEmail {
			set {
				Add("UserEmail", value);
			}
			get {
				return Get<string>("UserEmail");
			}
		}

		/// <summary>
		/// Gets or sets the friend user email.
		/// </summary>
		/// <value>The friend user email.</value>
		public string FriendUserEmail {
			set {
				Add("FriendUserEmail", value);
			}
			get {
				return Get<string>("FriendUserEmail");
			}
		}
	}
}