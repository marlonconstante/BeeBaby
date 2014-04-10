using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using BeeBaby.ResourcesProviders;
using Domain.Moment;
using Application;
using BigTed;
using PixateFreestyleLib;

namespace BeeBaby
{
	public partial class CameraViewController : UIViewController
	{
		UIImagePickerController m_picker;

		public CameraViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// View did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			// Create the moment, saves and generate a ID for future use.
			var momentService = new MomentService();
			CurrentContext.Instance.Moment = momentService.CreateMoment();

			if (MediaPickerProvider.IsCameraAvailable()) {
				var mediaPickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.Camera);
				m_picker = mediaPickerProvider.GetUIImagePickerController();
				m_picker.CameraOverlayView = this.View;

				PresentViewController(m_picker, false, null);
			} else {
				OpenMedia(btnOpenMedia);
			}
		}

		/// <summary>
		/// Takes the photo.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void TakePhoto(UIButton sender)
		{
			m_picker.TakePicture();
		}

		/// <summary>
		/// Opens the media.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void OpenMedia(UIButton sender)
		{
			// Shows the spinner
			BTProgressHUD.Show(); 

			PerformSegue("segueMedia", sender);
			DismissViewController(true, null);
		}
	}
}

