using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using BeeBaby.ResourcesProviders;

namespace BeeBaby
{
	public class ImagePickerDelegate : UIImagePickerControllerDelegate
	{
		ImageProvider m_imageProvider;

		public ImagePickerDelegate()
		{
			m_imageProvider = new ImageProvider();
		}

		public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
		{
			UIImage photo = (UIImage)info.ObjectForKey(UIImagePickerController.OriginalImage);

			m_imageProvider.SaveTemporaryImageOnApp(photo);

			if (picker.SourceType != UIImagePickerControllerSourceType.Camera)
			{
				picker.DismissViewController(true, null);
			}
		}

		public override void FinishedPickingImage(UIImagePickerController picker, UIImage image, NSDictionary editingInfo)
		{
			m_imageProvider.SaveTemporaryImageOnApp(image);
		}
	}
}