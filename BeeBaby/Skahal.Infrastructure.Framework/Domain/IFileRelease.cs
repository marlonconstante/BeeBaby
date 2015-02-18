using System;

namespace Skahal.Infrastructure.Framework.Domain
{
	public interface IFileRelease
	{
		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		long Size { get; set; }

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
		long Version { get; set; }
	}
}