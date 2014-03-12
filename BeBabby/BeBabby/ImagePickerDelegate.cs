using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace BeBabby
{
	public class ImagePickerDelegate : UIImagePickerControllerDelegate
	{
		public ImagePickerDelegate()
		{
		}

		public override void FinishedPickingImage(UIImagePickerController picker, UIImage image, NSDictionary editingInfo)
		{
			var documentsDirectory = Environment.GetFolderPath
					(Environment.SpecialFolder.Personal);
			string jpgFilename = System.IO.Path.Combine(documentsDirectory, string.Format("{0}.jpg", Guid.NewGuid()));
			NSData imgData = image.AsJPEG();
			NSError err = null;
			if (imgData.Save(jpgFilename, false, out err))
			{
				Console.WriteLine("saved as " + jpgFilename);
			}
			else
			{
				Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
			}
		}
	}
}

