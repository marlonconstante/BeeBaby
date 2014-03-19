using System;
using System.Collections.Generic;	
using HelperSharp;
using System.IO;
using Infrastructure.Framework.Resources;

namespace Infrastructure.App.Resources
{
	/// <summary>
	/// File system resource provider.
	/// </summary>
	public class FileSystemResourceProvider : IResourceProvider
	{
		#region Fields
		private string m_rootDirectory;
		private string m_urlsHost;
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="Infrastructure.App.Resources.FileSystemResourceProvider"/> class.
		/// </summary>
		/// <param name="rootDirectory">Root directory.</param>
		/// <param name="urlsHost">Urls host.</param>
		public FileSystemResourceProvider(string rootDirectory, string urlsHost = "~")
		{
			ExceptionHelper.ThrowIfNullOrEmpty("rootDirectory", rootDirectory);            

			m_rootDirectory = rootDirectory;
			m_urlsHost = urlsHost;

			if (!Directory.Exists(m_rootDirectory))
			{
				Directory.CreateDirectory(m_rootDirectory);
			}
		}

		#region IResourceProvider implementation
		public void SaveTemporaryImages(IResourceEntity resourceEntity, params Stream[] filesStreams)
		{
			throw new NotImplementedException();
		}
		public bool SavePermanentImages(IResourceEntity resourceEntity)
		{
			throw new NotImplementedException();
		}
		public bool CopyImages(IResourceEntity fromEntity, IResourceEntity toEntity)
		{
			throw new NotImplementedException();
		}
		public IEnumerable<string> GetImagesUrls(IResourceEntity resourceEntity)
		{
			throw new NotImplementedException();
		}
		public IEnumerable<byte[]> GetImagesBytes(IResourceEntity resourceEntity)
		{
			throw new NotImplementedException();
		}
		#endregion


		private string GetTemporaryImagesDirectory(IResourceEntity resourceEntity, bool createIfNotExists = true)
		{
			var imagesDirectory = Path.Combine(m_rootDirectory, "Temp");
			imagesDirectory = Path.Combine(imagesDirectory, resourceEntity.TemporaryKey);

			if (createIfNotExists && !Directory.Exists(imagesDirectory))
			{
				Directory.CreateDirectory(imagesDirectory);
			}

			return imagesDirectory;
		}

		private string GetPermanetImagesDirectory(IResourceEntity resourceEntity)
		{
			var imagesDirectory = Path.Combine(m_rootDirectory, GetEntityName(resourceEntity));
			imagesDirectory = Path.Combine(imagesDirectory, resourceEntity.Key.ToString());

			if (!Directory.Exists(imagesDirectory))
			{
				Directory.CreateDirectory(imagesDirectory);
			}

			return imagesDirectory;
		}

		private static string GetEntityName(IResourceEntity resourceEntity)
		{
			return resourceEntity.GetType().Name.Split('_')[0];
		}
	}
}

