using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.IO;
using Application;

namespace BeeBaby
{
	public class ImagePickerDelegate : UIImagePickerControllerDelegate
	{
		public ImagePickerDelegate()
		{
		}

		public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
		{
			UIImage photo = (UIImage)info.ObjectForKey(UIImagePickerController.OriginalImage);
			var currentMoment = CurrentMoment.Instance.Moment;

			var appDocumentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var momentDirectoryPath = Path.Combine(appDocumentsDirectory, currentMoment.Id);
			Directory.CreateDirectory(momentDirectoryPath);
			string jpgFilename = Path.Combine(momentDirectoryPath, string.Format("{0}.jpg", Guid.NewGuid()));

			InvokeOnMainThread(() =>
			{
				using (NSData imageData = photo.AsJPEG())
				{
					NSError err;
					if (!imageData.Save(jpgFilename, false, out err))
					{
						Console.WriteLine("Saving of file failed: " + err.Description);
					}
				}
			});

			if (picker.SourceType != UIImagePickerControllerSourceType.Camera)
			{
				picker.DismissViewController(true, null);
			}
		}

		public override void FinishedPickingImage(UIImagePickerController picker, UIImage image, NSDictionary editingInfo)
		{
			var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string jpgFilename = System.IO.Path.Combine(documentsDirectory, string.Format("{0}.jpg", Guid.NewGuid()));

			using (NSData imageData = image.AsJPEG(0.2f))
			{
				NSError err;
				if (!imageData.Save(jpgFilename, false, out err))
				{
					Console.WriteLine("Saving of file failed: " + err.Description);
				}
			}
		}
	}
}