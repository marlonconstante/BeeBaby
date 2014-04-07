using System;
using System.Linq;
using Application;
using System.IO;
using Domain.Moment;
using System.Globalization;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Domain.Media;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace BeeBaby.ResourcesProviders
{
	public class ImageProvider
	{
		Moment m_currentMoment;
		string m_appDocumentsDirectory;
		const string m_temporaryDirectoryName = "temp";
		const string m_fileExtension = ".jpg";
		const string m_thumbnailPrefix = "thumbnail-";

		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.ResourcesProviders.ImageProvider"/> class.
		/// </summary>
		public ImageProvider()
		{
			m_currentMoment = CurrentContext.Instance.Moment;
			m_appDocumentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		}

		/// <summary>
		/// Gets the temporary directory path.
		/// </summary>
		/// <returns>The temporary directory path.</returns>
		string GetTemporaryDirectoryPath()
		{
			return Path.Combine(m_appDocumentsDirectory, m_temporaryDirectoryName, m_currentMoment.Id);
		}

		/// <summary>
		/// Gets the permanent directory.
		/// </summary>
		/// <returns>The permanent directory.</returns>
		string GetPermanentDirectory()
		{
			var permanentDirectory = Path.Combine(m_appDocumentsDirectory, m_currentMoment.Id);
			Directory.CreateDirectory(permanentDirectory);

			return permanentDirectory;
		}

		/// <summary>
		/// Creates the temporary directory.
		/// </summary>
		/// <returns>The temporary directory.</returns>
		private string CreateTemporaryDirectory()
		{
			var path = GetTemporaryDirectoryPath();
			Directory.CreateDirectory(path);

			return path;
		}

		/// <summary>
		/// Creates the temporary file path.
		/// </summary>
		/// <returns>The temporary file path.</returns>
		/// <param name="isThumbnail">If set to <c>true</c> is thumbnail.</param>
		public string CreateTemporaryFilePath(bool isThumbnail = false)
		{
			var fileName = string.Concat(
				               isThumbnail ? m_thumbnailPrefix : string.Empty,
				               DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss-fff", CultureInfo.InvariantCulture), 
				               m_fileExtension);
			var filePath = Path.Combine(CreateTemporaryDirectory(), fileName);

			return filePath;
		}

		/// <summary>
		/// Gets the temporary images names for current moment.
		/// </summary>
		/// <returns>The temporary images names for current moment.</returns>
		public IList<string> GetTemporaryImagesNamesForCurrentMoment()
		{
			var temporaryDirectory = CreateTemporaryDirectory();
			return Directory.GetFiles(temporaryDirectory, string.Concat("*", m_fileExtension));
		}

		/// <summary>
		/// Gets the images for current moment.
		/// </summary>
		/// <returns>The images for current moment.</returns>
		/// <param name="thumbnails">If set to <c>true</c> thumbnails.</param>
		public IList<UIImage> GetImagesForCurrentMoment(bool thumbnails = false)
		{
			var temporaryDirectory = CreateTemporaryDirectory();
			var fileNames = Directory.GetFiles(temporaryDirectory, string.Concat("*", m_fileExtension))
				.Where(f => 
						thumbnails 
						? f.Contains(m_thumbnailPrefix)
						: !f.Contains(m_thumbnailPrefix)
            ).ToArray();
		

			var images = new List<UIImage>();

			foreach (var fileName in fileNames)
			{
				var data = NSData.FromFile(System.IO.Path.Combine(temporaryDirectory, fileName));
				images.Add(UIImage.LoadFromData(data));
			}

			return images;
		}

		/// <summary>
		/// Saves the permanent images.
		/// </summary>
		/// <param name="imagesNames">Images names.</param>
		public void SavePermanentImages(IList<string> imagesNames)
		{
			var temporaryDirectory = GetTemporaryDirectoryPath();
			var permanentDirectory = GetPermanentDirectory();
			foreach (var imageName in imagesNames)
			{
				var source = Path.Combine(temporaryDirectory, imageName);
				var destiny = Path.Combine(permanentDirectory, imageName);
				File.Move(source, destiny);
			}
		}

		/// <summary>
		/// Saves the temporary image on app.
		/// </summary>
		/// <param name="image">Image.</param>
		public void SaveTemporaryImageOnApp(UIImage image)
		{
			using (NSData imageData = image.AsJPEG())
			{
				NSError err;
				if (!imageData.Save(CreateTemporaryFilePath(), false, out err))
				{
					Console.WriteLine("Saving of file failed: " + err.Description);
				}
			}

			using (NSData imageData = GenerateThumbnail(image).AsJPEG())
			{
				NSError err;
				if (!imageData.Save(CreateTemporaryFilePath(true), false, out err))
				{
					Console.WriteLine("Saving of file failed: " + err.Description);
				}
			}
		}

		/// <summary>
		/// Generates the thumbnail.
		/// </summary>
		/// <returns>The thumbnail.</returns>
		/// <param name="sourceImage">Source image.</param>
		private UIImage GenerateThumbnail(UIImage sourceImage)
		{
			var croppedImage = CropImage(sourceImage, sourceImage.Size.Width, sourceImage.Size.Height);

			return ResizeImage(croppedImage, MediaBase.ImageThumbnailWidth, MediaBase.ImageThumbnailHeight);
		}

		/// <summary>
		/// Resizes the image.
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="sourceImage">Source image.</param>
		/// <param name="newWidth">New width.</param>
		/// <param name="newHeight">New height.</param>
		static UIImage ResizeImage(UIImage sourceImage, int newWidth, int newHeight)
		{
			var size = new Size(newWidth, newHeight);
			UIGraphics.BeginImageContextWithOptions(size, true, 0f);
			sourceImage.Draw(new Rectangle(0, 0, newWidth, newHeight));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return resultImage;
		}

		/// <summary>
		/// Crops the image.
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="sourceImage">Source image.</param>
		/// <param name="sourceWidth">Source width.</param>
		/// <param name="sourceHeight">Source height.</param>
		static UIImage CropImage(UIImage sourceImage, float sourceWidth, float sourceHeight)
		{
			float marginHorizontal = 0;
			float marginVertical = 0;
			float imageSize;

			if (sourceWidth > sourceHeight)
			{
				marginHorizontal = (sourceWidth - sourceHeight) / 2;
				imageSize = sourceHeight;
			}
			else
			{
				marginVertical = (sourceHeight - sourceWidth) / 2;
				imageSize = sourceWidth;
			}

			// Crop the image at original size
			UIImage cropped;
			using (CGImage cr = sourceImage.CGImage.WithImageInRect(new RectangleF(marginHorizontal, marginVertical, imageSize, imageSize)))
			{
				cropped = UIImage.FromImage(cr, 0f, sourceImage.Orientation);
			}
			return cropped;
		}
	}
}

