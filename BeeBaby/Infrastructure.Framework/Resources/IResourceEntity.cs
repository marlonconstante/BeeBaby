using System;
using Skahal.Infrastructure.Framework.Domain;

namespace Infrastructure.Framework.Resources
{
	/// <summary>
	/// Defines an Entity that contains resources.
	/// </summary>
	public interface IResourceEntity : IEntity
	{
		/// <summary>
		/// Gets or sets the temporary key.
		/// </summary>
		/// <value>The temporary key.</value>
		string TemporaryKey { get; set; }

		/// <summary>
		/// Gets the width of the resource.
		/// </summary>
		/// <value>The width of the resource.</value>
		int ResourceWidth { get; }

		/// <summary>
		/// Gets the height of the resource.
		/// </summary>
		/// <value>The height of the resource.</value>
		int ResourceHeight { get; }
	}
}

