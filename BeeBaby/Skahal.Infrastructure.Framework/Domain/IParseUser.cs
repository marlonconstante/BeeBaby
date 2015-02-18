using System;

namespace Skahal.Infrastructure.Framework.Domain
{
	/// <summary>
	/// Parse user.
	/// </summary>
	public interface IParseUser
	{
		/// <summary>
		/// Gets or sets the device identifier.
		/// </summary>
		/// <value>The device identifier.</value>
		string DeviceId { get; set; }
	}
}