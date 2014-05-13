﻿using System;
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
			m_appDocumentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		}

		/// <summary>
		/// Gets the temporary directory path.
		/// </summary>
		/// <returns>The temporary directory path.</returns>
		string GetTemporaryDirectoryPath()
		{
			var path = Path.Combine(m_appDocumentsDirectory, m_temporaryDirectoryName, m_name);
			Directory.CreateDirectory(path);

			return path;
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
			return string.Concat(DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss-fff", CultureInfo.InvariantCulture), m_fileExtension);
		}

		/// <summary>
		/// Gets the temporary images names for current moment.
		/// </summary>
		/// <returns>The temporary images names for current moment.</returns>
		public IList<string> GetTemporaryImagesNamesForCurrentMoment()
		{
			var temporaryDirectory = GetTemporaryDirectoryPath();

			return Directory.GetFiles(temporaryDirectory, string.Concat("*", m_fileExtension));
		}

		/// <summary>
		/// Gets the images for current moment.
		/// </summary>
		/// <returns>The images for current moment.</returns>
		/// <param name="includeTemporary">If set to <c>true</c> include temporary.</param>
		/// <param name="thumbnails">If set to <c>true</c> thumbnails.</param>
		public IList<ImageViewModel> GetImagesForCurrentMoment(bool includeTemporary, bool thumbnails = false)
		{
			var fileNames = new List<string>();

			if (includeTemporary)
			{
				var temporaryDirectory = GetTemporaryDirectoryPath();
				fileNames.AddRange(Directory.GetFiles(temporaryDirectory, string.Concat("*", m_fileExtension)));
			}

			var permanentDirectory = GetPermanentDirectory();
			fileNames.AddRange(Directory.GetFiles(permanentDirectory, string.Concat("*", m_fileExtension)));

			fileNames = fileNames.Where(f => 
				thumbnails 
				? f.Contains(m_thumbnailPrefix)
				: !f.Contains(m_thumbnailPrefix)
			).ToList();

			var images = new List<ImageViewModel>();

			foreach (var fileName in fileNames)
			{
				var data = NSData.FromFile(fileName);
				var image = new ImageViewModel {
					Image = UIImage.LoadFromData(data),
					FileName = fileName.Split('/').Last()
				};
				images.Add(image);
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
				// Thumbnails
				var source = Path.Combine(temporaryDirectory, imageName);
				var destiny = Path.Combine(permanentDirectory, imageName);

				File.Move(source, destiny);

				// Images at full size
				var imageFullSizedName = imageName.Remove(0, m_thumbnailPrefix.Length);
				source = Path.Combine(temporaryDirectory, imageFullSizedName);
				destiny = Path.Combine(permanentDirectory, imageFullSizedName);

				File.Move(source, destiny);
			}
		}

		/// <summary>
		/// Saves the temporary image on app.
		/// </summary>
		/// <returns>The temporary image on app.</returns>
		/// <param name="image">Image.</param>
		public string SaveTemporaryImageOnApp(UIImage image)
		{
			var fileName = GenerateFileName();
			SaveImageOnApp(image, fileName, GetTemporaryDirectoryPath());
			return fileName;
		}

		/// <summary>
		/// Saves the image on app.
		/// </summary>
		/// <param name="image">Image.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="directoryPath">Directory path.</param>
		public void SaveImageOnApp(UIImage image, string fileName, string directoryPath)
		{
			var fullImagePath = Path.Combine(directoryPath, fileName);
			var thumbnailImagePath = Path.Combine(directoryPath, GetThumbnailImageName(fileName));

			using (NSData imageData = image.AsJPEG(MediaBase.ImageCompressionQuality))
			{
				NSError err;
				if (!imageData.Save(fullImagePath, false, out err))
				{
					Console.WriteLine("Saving of file failed: " + err.Description);
				}
			}

			using (NSData imageData = GenerateThumbnail(image).AsJPEG(MediaBase.ImageCompressionQuality))
			{
				NSError err;
				if (!imageData.Save(thumbnailImagePath, false, out err))
				{
					Console.WriteLine("Saving of file failed: " + err.Description);
				}
			}
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
		/// <param name="thumbnail">If set to <c>true</c> thumbnail.</param>
		public UIImage GetImage(string imageName, bool thumbnail = false)
		{
			var permanentDirectory = GetPermanentDirectory();

			if (!thumbnail)
			{
				imageName = imageName.Remove(0, m_thumbnailPrefix.Length);
			}

			var imagePath = Directory.GetFiles(permanentDirectory, string.Concat("*", m_fileExtension))
				.FirstOrDefault(i => i.Equals(Path.Combine(GetPermanentDirectory(), imageName)));

			var data = NSData.FromFile(imagePath);

			return UIImage.LoadFromData(data);
		}

		/// <summary>
		/// Generates the thumbnail.
		/// </summary>
		/// <returns>The thumbnail.</returns>
		/// <param name="sourceImage">Source image.</param>
		private UIImage GenerateThumbnail(UIImage sourceImage)
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
		/// Creates the image for share.
		/// </summary>
		/// <returns>The image for share.</returns>
		/// <param name="sourceImage">Source image.</param>
		public UIImage CreateImageForShare(UIImage sourceImage, Moment moment)
		{
			UIImage resultImage;

			var board = UIStoryboard.FromName("MainStoryboard", null);
			var controller = (ImageShareViewController) board.InstantiateViewController("ImageShareViewController");
			controller.LoadView();

			using (var image = CroppedImageResize(sourceImage, MediaBase.ImageShareSize))
			{
				controller.SetInformation(moment, image, CurrentContext.Instance.CurrentBaby);
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
	}
}