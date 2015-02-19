using System;
using System.Linq;
using Application;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Domain.Media;
using System.Drawing;
using MonoTouch.CoreGraphics;
using BeeBaby.ViewModels;
using Domain.Moment;
using Infrastructure.Systems.Domain;
using BeeBaby.Media;
using BeeBaby.Controllers;
using BeeBaby.Backup;

namespace BeeBaby.ResourcesProviders
{
	public class ImageProvider
	{
		string m_name;
		string m_appDocumentsDirectory;
		const string m_temporaryDirectoryName = "temp";
		const string m_fileExtension = ".jpg";
		const string m_thumbnailPrefix = "thumbnail-";

		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.ResourcesProviders.ImageProvider"/> class.
		/// </summary>
		public ImageProvider(string name = "")
		{
			m_name = name;
			m_appDocumentsDirectory = FileHandle.RootFolderPath;
		}

		/// <summary>
		/// Gets the temporary directory.
		/// </summary>
		/// <returns>The temporary directory.</returns>
		string GetTemporaryDirectory()
		{
			var temporaryDirectory = Path.Combine(m_appDocumentsDirectory, m_temporaryDirectoryName, m_name);
			Directory.CreateDirectory(temporaryDirectory);

			return temporaryDirectory;
		}

		/// <summary>
		/// Gets the permanent directory.
		/// </summary>
		/// <returns>The permanent directory.</returns>
		string GetPermanentDirectory()
		{
			var permanentDirectory = Path.Combine(m_appDocumentsDirectory, m_name);
			Directory.CreateDirectory(permanentDirectory);

			return permanentDirectory;
		}

		/// <summary>
		/// Generates the name of the file.
		/// </summary>
		/// <returns>The file name.</returns>
		string GenerateFileName()
		{
			return DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss-fff", CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Deletes the permanent file.
		/// </summary>
		/// <param name="fileName">File name.</param>
		public void DeletePermanentFile(string fileName)
		{
			var filePath = Path.Combine(GetPermanentDirectory(), string.Concat(fileName, m_fileExtension));
			File.Delete(filePath);
		}

		/// <summary>
		/// Creates the empty file.
		/// </summary>
		/// <param name="filePath">File path.</param>
		void CreateEmptyFile(string filePath) {
			if (FileHandle.IsValid(filePath))
			{
				File.Create(filePath).Close();
			}
		}

		/// <summary>
		/// Deletes the files.
		/// </summary>
		/// <param name="temporary">If set to <c>true</c> temporary.</param>
		public void DeleteFiles(bool temporary)
		{
			var path = Path.Combine(m_appDocumentsDirectory, temporary ? m_temporaryDirectoryName : "");
			Directory.CreateDirectory(path);

			Directory.EnumerateDirectories(path)
				.ToList().ForEach(directoryName => {
				var validTemporaryDirectory = temporary && (string.IsNullOrEmpty(m_name) || !directoryName.EndsWith(m_name));
				var validPermanentDirectory = !temporary && !string.IsNullOrEmpty(m_name) && directoryName.EndsWith(m_name);

				var files = Directory.EnumerateFiles(directoryName).ToList();
				if (validTemporaryDirectory)
				{
					files.ForEach(file => File.Delete(file));
					Directory.Delete(directoryName);
				}
				else if (validPermanentDirectory)
				{
					files.ForEach(file => CreateEmptyFile(file));
				}
			});
		}

		/// <summary>
		/// Removes the empty directories.
		/// </summary>
		public void RemoveEmptyDirectories()
		{
			Directory.EnumerateDirectories(m_appDocumentsDirectory)
				.ToList().ForEach(directoryName => {
				if (Directory.EnumerateFileSystemEntries(directoryName).Count() == 0)
				{
					if (!directoryName.EndsWith("/temp"))
					{
						Directory.Delete(directoryName);
					}
				}
			});
		}

		/// <summary>
		/// Gets the file names.
		/// </summary>
		/// <returns>The file names.</returns>
		/// <param name="thumbnails">If set to <c>true</c> thumbnails.</param>
		public IList<string> GetFileNames(bool thumbnails = false)
		{
			var fileNames = new List<string>();
			fileNames.AddRange(Directory.GetFiles(GetPermanentDirectory(), string.Concat("*", m_fileExtension)));
			fileNames.AddRange(Directory.GetFiles(GetTemporaryDirectory(), string.Concat("*", m_fileExtension)));

			fileNames = fileNames.Where(f => 
				thumbnails 
				? f.Contains(m_thumbnailPrefix)
				: !f.Contains(m_thumbnailPrefix)
			).Where(f => new FileInfo(f).Length > 0L).ToList();

			return fileNames;
		}

		/// <summary>
		/// Gets the images.
		/// </summary>
		/// <returns>The images.</returns>
		/// <param name="thumbnails">If set to <c>true</c> thumbnails.</param>
		public IList<ImageModel> GetImages(bool thumbnails = false)
		{
			var images = new List<ImageModel>();
			var scale = CurrentContext.Instance.Scale;

			foreach (var fileName in GetFileNames(thumbnails))
			{
				var data = NSData.FromFile(fileName);
				var image = new ImageModel {
					Image = UIImage.LoadFromData(data, scale),
					FileName = Path.GetFileName(fileName)
				};
				images.Add(image);
			}

			return images;
		}

		/// <summary>
		/// Gets the remote images.
		/// </summary>
		/// <returns>The remote images.</returns>
		/// <param name="urls">Urls.</param>
		public IList<ImageModel> GetRemoteImages(IList<Uri> urls)
		{
			var remoteImages = new List<ImageModel>();
			var scale = CurrentContext.Instance.Scale;

			foreach (var url in urls)
			{
				var data = NSData.FromUrl(NSUrl.FromString(url.AbsoluteUri));
				var image = new ImageModel {
					Image = UIImage.LoadFromData(data, scale),
					FileName = Path.GetFileName(url.AbsolutePath)
				};
				remoteImages.Add(image);
			}

			return remoteImages;
		}

		/// <summary>
		/// Gets the moment images.
		/// </summary>
		/// <returns>The moment images.</returns>
		/// <param name="moment">Moment.</param>
		/// <param name="thumbnails">If set to <c>true</c> thumbnails.</param>
		public IList<ImageModel> getMomentImages(IMoment moment, bool thumbnails = false)
		{
			if (moment is MomentShared)
			{
				var momentShared = (MomentShared) moment;
				return GetRemoteImages(thumbnails ? momentShared.ThumbnailsUrl : momentShared.FullImagesUrl);
			}
			else
			{
				return GetImages(thumbnails);
			}
		}

		/// <summary>
		/// Gets the data images.
		/// </summary>
		/// <returns>The data images.</returns>
		/// <param name="thumbnails">If set to <c>true</c> thumbnails.</param>
		public IDictionary<string, byte[]> GetDataImages(bool thumbnails = false)
		{
			var dataImages = new Dictionary<string, byte[]>();

			foreach (var fileName in GetFileNames(thumbnails))
			{
				var data = NSData.FromFile(fileName);
				dataImages.Add(Path.GetFileName(fileName), data.ToArray());
			}

			return dataImages;
		}

		/// <summary>
		/// Saves the permanent images.
		/// </summary>
		/// <returns>The permanent images.</returns>
		/// <param name="thumbnailNames">Thumbnail names.</param>
		public IList<string> SavePermanentImages(IList<string> thumbnailNames)
		{
			var temporaryDirectory = GetTemporaryDirectory();
			var permanentDirectory = GetPermanentDirectory();

			var imageNames = new List<string>();
			foreach (var thumbnailName in thumbnailNames)
			{
				imageNames.Add(thumbnailName);
				imageNames.Add(thumbnailName.Remove(0, m_thumbnailPrefix.Length));
			}

			Directory.EnumerateFiles(permanentDirectory)
				.Where(f => f.EndsWith(m_fileExtension))
				.ToList().ForEach(filePath => {
				var imageName = Path.GetFileName(filePath);
				if (imageNames.Contains(imageName))
				{
					imageNames.Remove(imageName);
				}
				else
				{
					CreateEmptyFile(filePath);
				}
			});

			var permanentImages = new List<string>();
			foreach (var imageName in imageNames)
			{
				var permanentImage = Path.Combine(permanentDirectory, imageName);
				File.Move(Path.Combine(temporaryDirectory, imageName), permanentImage);
				permanentImages.Add(permanentImage);
			}

			return permanentImages;
		}

		/// <summary>
		/// Saves the temporary image on app.
		/// </summary>
		/// <returns>The temporary image on app.</returns>
		/// <param name="image">Image.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="createThumbnail">If set to <c>true</c> create thumbnail.</param>
		public string SaveTemporaryImageOnApp(UIImage image, string fileName = null, bool createThumbnail = true)
		{
			return SaveImageOnApp(image, fileName, GetTemporaryDirectory(), createThumbnail);
		}

		/// <summary>
		/// Saves the permanent image on app.
		/// </summary>
		/// <returns>The permanent image on app.</returns>
		/// <param name="image">Image.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="createThumbnail">If set to <c>true</c> create thumbnail.</param>
		public string SavePermanentImageOnApp(UIImage image, string fileName = null, bool createThumbnail = true)
		{
			return SaveImageOnApp(image, fileName, GetPermanentDirectory(), createThumbnail);
		}

		/// <summary>
		/// Saves the image on app.
		/// </summary>
		/// <returns>The image on app.</returns>
		/// <param name="image">Image.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="directoryPath">Directory path.</param>
		/// <param name="createThumbnail">If set to <c>true</c> create thumbnail.</param>
		string SaveImageOnApp(UIImage image, string fileName, string directoryPath, bool createThumbnail)
		{
			if (String.IsNullOrEmpty(fileName))
			{
				fileName = GenerateFileName();
			}
			fileName = string.Concat(fileName, m_fileExtension);

			var fullImagePath = Path.Combine(directoryPath, fileName);
			var thumbnailImagePath = Path.Combine(directoryPath, GetThumbnailImageName(fileName));

			using (var fullScreenImage = ResizeFullScreenImage(image))
			{
				using (var imageData = fullScreenImage.AsJPEG(MediaBase.ImageCompressionQuality))
				{
					NSError error;
					if (!imageData.Save(fullImagePath, false, out error))
					{
						Console.WriteLine("Ocorreu um erro ao salvar o arquivo \"" + fileName + "\":\n" + error.LocalizedDescription);
					}
				}

				if (createThumbnail)
				{
					using (var thumbnail = GenerateThumbnail(fullScreenImage))
					{
						using (var imageData = thumbnail.AsJPEG(MediaBase.ImageCompressionQuality))
						{
							NSError error;
							if (!imageData.Save(thumbnailImagePath, false, out error))
							{
								Console.WriteLine("Ocorreu um erro ao salvar o arquivo \"" + fileName + "\":\n" + error.LocalizedDescription);
							}
						}
					}
				}
			}

			return fileName;
		}

		/// <summary>
		/// Gets the name of the thumbnail image.
		/// </summary>
		/// <returns>The thumbnail image name.</returns>
		/// <param name="imageName">Image name.</param>
		public string GetThumbnailImageName(string imageName)
		{
			return string.Concat(m_thumbnailPrefix, imageName);
		}

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="imageName">Image name.</param>
		public UIImage GetImage(string imageName)
		{
			var permanentDirectory = GetPermanentDirectory();

			var imagePath = Directory.GetFiles(permanentDirectory, string.Concat("*", m_fileExtension))
				.FirstOrDefault(i => i.Contains(Path.Combine(permanentDirectory, imageName)));

			if (imagePath != null)
			{
				return LoadImage(imagePath);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Loads the image.
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="imagePath">Image path.</param>
		public UIImage LoadImage(string imagePath)
		{
			var scale = CurrentContext.Instance.Scale;
			var data = NSData.FromFile(imagePath);
			return UIImage.LoadFromData(data, scale);
		}

		/// <summary>
		/// Generates the thumbnail.
		/// </summary>
		/// <returns>The thumbnail.</returns>
		/// <param name="sourceImage">Source image.</param>
		public UIImage GenerateThumbnail(UIImage sourceImage)
		{
			return CroppedImageResize(sourceImage, MediaBase.ImageThumbnailSize);
		}

		/// <summary>
		/// Croppeds the image resize.
		/// </summary>
		/// <returns>The image resize.</returns>
		/// <param name="sourceImage">Source image.</param>
		/// <param name="size">Size.</param>
		UIImage CroppedImageResize(UIImage sourceImage, float size)
		{
			UIImage resultImage;

			float width = sourceImage.Size.Width;
			float height = sourceImage.Size.Height;
			float marginHorizontal = 0;
			float marginVertical = 0;

			if (width > height)
			{
				width = width / (height / size);
				height = size;
				marginHorizontal = (width - height) / 2;
			}
			else
			{
				height = height / (width / size);
				width = size;
				marginVertical = (height - width) / 2;
			}

			UIGraphics.BeginImageContextWithOptions(new SizeF(size, size), true, 0f);
			try
			{
				sourceImage.Draw(new RectangleF(-marginHorizontal, -marginVertical, width, height));
				resultImage = UIGraphics.GetImageFromCurrentImageContext();
			}
			finally
			{
				UIGraphics.EndImageContext();
			}

			return resultImage;
		}

		/// <summary>
		/// Resizes the full screen image.
		/// </summary>
		/// <returns>The full screen image.</returns>
		/// <param name="sourceImage">Source image.</param>
		UIImage ResizeFullScreenImage(UIImage sourceImage)
		{
			return ResizeImage(sourceImage, MediaBase.FullScreenImageMaxSizeInPixels);
		}

		/// <summary>
		/// Resizes the image.
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="sourceImage">Source image.</param>
		/// <param name="maxSizeInPixels">Max size in pixels.</param>
		UIImage ResizeImage(UIImage sourceImage, float maxSizeInPixels)
		{
			var size = sourceImage.Size;
			var scale = sourceImage.CurrentScale;

			var maxSize = maxSizeInPixels / scale;
			var ratio = Math.Min(maxSize / size.Width, maxSize / size.Height);

			if (ratio < 1f)
			{
				var preferredSize = new SizeF(size.Width * ratio, size.Height * ratio);
				return sourceImage.Scale(preferredSize, scale);
			}
			return sourceImage;
		}

		/// <summary>
		/// Creates the image for share.
		/// </summary>
		/// <returns>The image for share.</returns>
		/// <param name="sourceImage">Source image.</param>
		/// <param name="moment">Moment.</param>
		public UIImage CreateImageForShare(UIImage sourceImage, IMoment moment)
		{
			UIImage resultImage;

			var board = UIStoryboard.FromName("MainStoryboard", null);
			var controller = (ImageShareViewController) board.InstantiateViewController("ImageShareViewController");
			controller.LoadView();

			using (var image = CroppedImageResize(sourceImage, MediaBase.ImageShareSize))
			{
				controller.SetInformation(moment, image);
			}

			UIGraphics.BeginImageContextWithOptions(new SizeF(MediaBase.ImageShareSize, MediaBase.ImageShareSize), false, 0f);
			try
			{
				using (var context = UIGraphics.GetCurrentContext())
				{
					controller.View.Layer.RenderInContext(context);
					resultImage = UIGraphics.GetImageFromCurrentImageContext();
				}
			}
			finally
			{
				UIGraphics.EndImageContext();
			}

			return resultImage;
		}

		/// <summary>
		/// Saves the temporary image.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="saveToAlbum">If set to <c>true</c> save to album.</param>
		/// <param name="selected">If set to <c>true</c> selected.</param>
		public void SaveTemporaryImage(NSDictionary info, bool saveToAlbum = false, bool selected = true)
		{
			SaveTemporaryImage((UIImage) info.ObjectForKey(UIImagePickerController.OriginalImage), saveToAlbum, selected);
		}

		/// <summary>
		/// Saves the temporary image.
		/// </summary>
		/// <param name="photo">Photo.</param>
		/// <param name="saveToAlbum">If set to <c>true</c> save to album.</param>
		/// <param name="selected">If set to <c>true</c> selected.</param>
		public void SaveTemporaryImage(UIImage photo, bool saveToAlbum = false, bool selected = true)
		{
			using (photo)
			{
				var fileName = SaveTemporaryImageOnApp(photo);
				if (saveToAlbum)
				{
					MediaLibrary.Instance.Save(photo);
				}
				if (selected)
				{
					var thumbnailImageName = GetThumbnailImageName(fileName);
					CurrentContext.Instance.Moment.SelectedMediaNames.Add(thumbnailImageName);
				}
			}
		}
	}
}