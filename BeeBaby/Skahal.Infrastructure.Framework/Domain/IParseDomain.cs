using System;

namespace Skahal.Infrastructure.Framework.Domain
{
	/// <summary>
	/// Parse domain.
	/// </summary>
	public interface IParseDomain
	{
		/// <summary>
		/// Gets or sets the object identifier.
		/// </summary>
		/// <value>The object identifier.</value>
		string ObjectId { get; set; }

		/// <summary>
		/// Gets or sets the created at.
		/// </summary>
		/// <value>The created at.</value>
		DateTime? CreatedAt { get; set; }

		/// <summary>
		/// Gets or sets the updated at.
		/// </summary>
		/// <value>The updated at.</value>
		DateTime? UpdatedAt { get; set; }
	}
}