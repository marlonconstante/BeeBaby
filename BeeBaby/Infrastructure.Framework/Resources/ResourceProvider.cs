using System;
using Skahal.Infrastructure.Framework.Commons;

namespace Infrastructure.Framework.Resources
{
	/// <summary>
	/// Resource provider.
	/// </summary>
	public static class ResourceProvider
	{
		#region Properties
		/// <summary>
		/// Gets the current resource provider.
		/// </summary>
		/// <value>The current resource provider.</value>
		public static IResourceProvider Current
		{
			get
			{
				return DependencyService.Create<IResourceProvider>();
			}
		}
		#endregion
	}
}

