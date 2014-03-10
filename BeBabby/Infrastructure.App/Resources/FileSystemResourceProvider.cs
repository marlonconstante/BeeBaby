using System;
using Infrastructure.Framework.Resources;
using System.Collections.Generic;	
using HelperSharp;
using System.IO;
using Skahal.Infrastructure.Framework.Domain;
using System.Linq;

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

		/// <summary>
		/// Saves the temporary images.
		/// </summary>
		/// <param name="resourceEntity">Resource entity.</param>
		/// <param name="filesStreams">Files streams.</param>
		public void SaveTemporaryImages(IResourceEntity resourceEntity, params System.IO.Stream[] filesStreams)
		{
			ExceptionHelper.ThrowIfNull("resourceEntity", resourceEntity);
			ExceptionHelper.ThrowIfNull("filesStreams", filesStreams);
			resourceEntity.TemporaryKey = Guid.NewGuid().ToString();

			for (int i = 0; i < filesStreams.Length; i++)
			{
				var fileName = Path.Combine(GetTemporaryImagesDirectory(resourceEntity), "{0}.jpg".With(i + 1));

//				ImageHelper.SaveImage(
//					filesStreams[i],
//					resourceEntity.ResourceWidth,
//					resourceEntity.ResourceHeight,
//					fileName);
			}



//			TweetStation.Camera.TakePicture (this, (obj) =>{
//				var photo = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
//				var documentsDirectory = Environment.GetFolderPath
//					(Environment.SpecialFolder.Personal);
//				string jpgFilename = System.IO.Path.Combine (documentsDirectory, "Photo.jpg"); // hardcoded filename, overwritten each time
//				NSData imgData = photo.AsJPEG();
//				NSError err = null;
//				if (imgData.Save(jpgFilename, false, out err)) {
//					Console.WriteLine("saved as " + jpgFilename);
//				} else {
//					Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
//				}
//			});

		}

		/// <summary>
		/// Saves the permanent images.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="resourceEntity">Resource entity.</param>
		public bool SavePermanentImages(IResourceEntity resourceEntity)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Copies the images.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="fromEntity">From entity.</param>
		/// <param name="toEntity">To entity.</param>
		public bool CopyImages(IResourceEntity fromEntity, IResourceEntity toEntity)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the images urls.
		/// </summary>
		/// <returns>The images urls.</returns>
		/// <param name="resourceEntity">Resource entity.</param>
		public IEnumerable<string> GetImagesUrls(IResourceEntity resourceEntity)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the images bytes.
		/// </summary>
		/// <returns>The images bytes.</returns>
		/// <param name="resourceEntity">Resource entity.</param>
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

