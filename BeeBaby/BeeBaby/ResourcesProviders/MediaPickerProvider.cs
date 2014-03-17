﻿using System;
using MonoTouch.UIKit;
using BeeBaby;

namespace BeBabby.ResourcesProviders
{
	public class MediaPickerProvider
	{
		UIImagePickerControllerSourceType m_sourceType;

		public MediaPickerProvider(UIImagePickerControllerSourceType sourceType)
		{
			m_sourceType = sourceType;
		}

		public UIImagePickerController GetUIImagePickerController()
		{
			// Setup the UIImagePickerController
			var picker = new UIImagePickerController();
			if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera) && m_sourceType == UIImagePickerControllerSourceType.Camera)
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

			return picker;
		}
	}
}

