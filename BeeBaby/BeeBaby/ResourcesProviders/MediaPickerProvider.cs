using System;
using MonoTouch.UIKit;
using BeeBaby;
using Application;

namespace BeeBaby.ResourcesProviders
{
	public class MediaPickerProvider
	{
		UIImagePickerControllerSourceType m_sourceType;

		public MediaPickerProvider(UIImagePickerControllerSourceType sourceType, ImagePickerDelegate imagePickerDelegate)
		{
			m_sourceType = sourceType;
			Delegate = imagePickerDelegate;
		}

		/// <summary>
		/// Determines if is camera available.
		/// </summary>
		/// <returns><c>true</c> if is camera available; otherwise, <c>false</c>.</returns>
		public static bool IsCameraAvailable()
		{
			return UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera);
		}

		/// <summary>
		/// Gets the user interface image picker controller.
		/// </summary>
		/// <returns>The user interface image picker controller.</returns>
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
			picker.Delegate = Delegate;

			return picker;
		}

		/// <summary>
		/// Gets or sets the delegate.
		/// </summary>
		/// <value>The delegate.</value>
		public ImagePickerDelegate Delegate { set; get; }
	}
}