using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using BeeBaby.ResourcesProviders;
using Domain.Moment;
using Application;
using BigTed;
using PixateFreestyleLib;
using Domain.Baby;
using MonoTouch.CoreGraphics;

namespace BeeBaby
{
	public partial class CameraViewController : UIViewController
	{
		UIImagePickerController m_picker;

		public CameraViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			// Hides the status bar
			NavigationController.NavigationBarHidden = true;
		}

		/// <summary>
		/// View did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			new OrientationNotification(btnFlash, lblFlash, btnSwitchCamera, btnOpenTimeline, btnTakePhoto, btnOpenMedia);

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
		/// Switchs the camera.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void SwitchCamera(UIButton sender)
		{
			bool front = m_picker.CameraDevice == UIImagePickerControllerCameraDevice.Front;
			View.BackgroundColor = UIColor.Black;
			UIView.Transition(m_picker.View, 0.75f, UIViewAnimationOptions.TransitionFlipFromLeft, () => {
				m_picker.CameraDevice = front ? UIImagePickerControllerCameraDevice.Rear : UIImagePickerControllerCameraDevice.Front;
				View.BackgroundColor = UIColor.Clear;
			}, null);
		}

		/// <summary>
		/// Takes the photo.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void TakePhoto(UIButton sender)
		{
			View.BackgroundColor = UIColor.Black;
			m_picker.TakePicture();
			UIView.Animate(0.3d, () => {
				View.BackgroundColor = UIColor.Clear;
			});
		}

		/// <summary>
		/// Opens the media.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void OpenMedia(UIButton sender)
		{
			// Shows the status bar
			NavigationController.NavigationBarHidden = false;

			// Shows the spinner
			BTProgressHUD.Show(); 

			PerformSegue("segueMedia", sender);
			DismissViewController(true, null);
		}
	}
}
