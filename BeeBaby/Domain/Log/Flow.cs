using System;
using Skahal.Infrastructure.Framework.Domain;

namespace Domain.Log
{
	/// <summary>
	/// Flow.
	/// </summary>
	public class Flow : EntityWithIdBase<string>, IAggregateRoot
	{
		/// <summary>
		/// Gets or sets the device identifier.
		/// </summary>
		/// <value>The device identifier.</value>
		public string DeviceId {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the session identifier.
		/// </summary>
		/// <value>The session identifier.</value>
		public string SessionId {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value>
		public DateTime Date {
			get;
			set;
		}
	}
}