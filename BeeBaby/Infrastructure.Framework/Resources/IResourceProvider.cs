using System;
using System.Collections.Generic;
using System.IO;

namespace Infrastructure.Framework.Resources
{
	/// <summary>
	/// Defines an interface to a resource provider for entities.
	/// </summary>
	public interface IResourceProvider
	{
		/// <summary>
		/// Saves the temporary images.
		/// </summary>
		/// <param name="resourceEntity">Resource entity.</param>
		/// <param name="filesStreams">Files streams.</param>
		void SaveTemporaryImages(IResourceEntity resourceEntity, params Stream[] filesStreams);

		/// <summary>
		/// Saves the permanent images.
		/// </summary>
		/// <returns><c>true</c>, if permanent images was saved, <c>false</c> otherwise.</returns>
		/// <param name="resourceEntity">Resource entity.</param>
		bool SavePermanentImages(IResourceEntity resourceEntity);

		/// <summary>
		/// Copies the images.
		/// </summary>
		/// <returns><c>true</c>, if images was copyed, <c>false</c> otherwise.</returns>
		/// <param name="fromEntity">From entity.</param>
		/// <param name="toEntity">To entity.</param>
		bool CopyImages(IResourceEntity fromEntity, IResourceEntity toEntity);

		/// <summary>
		/// Gets the images urls.
		/// </summary>
		/// <returns>The images urls.</returns>
		/// <param name="resourceEntity">Resource entity.</param>
		IEnumerable<string> GetImagesUrls(IResourceEntity resourceEntity);

		/// <summary>
		/// Gets the images bytes.
		/// </summary>
		/// <returns>The images bytes.</returns>
		/// <param name="resourceEntity">Resource entity.</param>
		IEnumerable<byte[]> GetImagesBytes(IResourceEntity resourceEntity);
	}
}

