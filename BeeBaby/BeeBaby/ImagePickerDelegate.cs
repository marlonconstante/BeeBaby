using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.IO;

namespace BeeBaby
{
	public class ImagePickerDelegate : UIImagePickerControllerDelegate
	{
		public ImagePickerDelegate()
		{
		}

		public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
		{
			UIImage item = (UIImage)info.ObjectForKey(UIImagePickerController.OriginalImage);
			var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string jpgFilename = System.IO.Path.Combine(documentsDirectory, string.Format("{0}.jpg", Guid.NewGuid()));

			InvokeOnMainThread(() => 
			{
				using (NSData imageData = item.AsJPEG())
				{
					NSError err;
					if (!imageData.Save(jpgFilename, false, out err))
					{
						Console.WriteLine("Saving of file failed: " + err.Description);
					}
				}
			});
		}

		public override void FinishedPickingImage(UIImagePickerController picker, UIImage image, NSDictionary editingInfo)
		{

			FinishedPickingMedia(picker, editingInfo);
		}
	}
}