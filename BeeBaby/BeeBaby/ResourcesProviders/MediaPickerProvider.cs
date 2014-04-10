using System;
using MonoTouch.UIKit;
using BeeBaby;
using Application;

namespace BeeBaby.ResourcesProviders
{
	public class MediaPickerProvider
	{
		UIImagePickerControllerSourceType m_sourceType;

		public MediaPickerProvider(UIImagePickerControllerSourceType sourceType)
		{
			m_sourceType = sourceType;
		}

		public static bool IsCameraAvailable()
		{
			return UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera);
		}

		public UIImagePickerController GetUIImagePickerController()
		{
			// Setup the UIImagePickerController
			var picker = new UIImagePickerController();
			if (IsCameraAvailable() && m_sourceType == UIImagePickerControllerSourceType.Camera)
			{
				picker.SourceType = UIImagePickerControllerSourceType.Camera;
				picker.PrefersStatusBarHidden();
				picker.ShowsCameraControls = false;
				picker.CameraCaptureMode = UIImagePickerControllerCameraCaptureMode.Photo;
			}
			else
			{
				picker.SourceType = UIImagePickerControllerSourceType.SavedPhotosAlbum;
			}
			picker.ModalPresentationStyle = UIModalPresentationStyle.CurrentContext;
			picker.Delegate = new ImagePickerDelegate();

			Console.WriteLine(CurrentContext.Instance.Moment.Id);

			return picker;
		}
	}
}

