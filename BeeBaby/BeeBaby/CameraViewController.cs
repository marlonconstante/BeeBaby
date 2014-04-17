using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using BeeBaby.ResourcesProviders;
using Domain.Moment;
using Application;
using BigTed;
using Domain.Baby;
using Skahal.Infrastructure.Framework.Globalization;
using MonoTouch.AudioToolbox;

namespace BeeBaby
{
	public partial class CameraViewController : UIViewController
	{
		UIImagePickerController m_picker;
		UIImagePickerControllerCameraFlashMode m_cameraFlashMode;
		OrientationNotification m_orientationNotification;
		SystemSound m_systemSound;

		public CameraViewController(IntPtr handle) : base(handle)
		{
			m_cameraFlashMode = UIImagePickerControllerCameraFlashMode.Off;
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			loadSound();
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			View.BackgroundColor = UIColor.Black;

			// Hides the status bar
			NavigationController.NavigationBarHidden = true;

			// Create the moment, saves and generate a ID for future use.
			var momentService = new MomentService();
			CurrentContext.Instance.Moment = momentService.CreateMoment();

			if (!MediaPickerProvider.IsCameraAvailable())
			{
				OpenMedia(btnOpenMedia);
			}
		}

		/// <summary>
		/// View did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			if (MediaPickerProvider.IsCameraAvailable())
			{
				var mediaPickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.Camera);
				m_picker = mediaPickerProvider.GetUIImagePickerController();
				m_picker.CameraOverlayView = this.View;

				PresentViewController(m_picker, false, null);
				ChangeFlashMode(btnFlash);

				if (m_orientationNotification == null)
				{
					m_orientationNotification = new OrientationNotification(btnFlash.Superview, btnSound, btnSwitchCamera, btnOpenTimeline, btnTakePhoto, btnOpenMedia);
				}
			}

			View.BackgroundColor = UIColor.Clear;
		}

		/// <summary>
		/// Loads the sound.
		/// </summary>
		void loadSound()
		{
			string filePath = NSBundle.MainBundle.PathForResource("lake-waves", "mp3");
			m_systemSound = SystemSound.FromFile(filePath);
		}

		/// <summary>
		/// Play the sound.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void PlaySound(UIButton sender)
		{
			m_systemSound.PlaySystemSound(); 
		}

		/// <summary>
		/// Changes the flash mode.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void ChangeFlashMode(UIButton sender)
		{
			switch (m_cameraFlashMode)
			{
			case UIImagePickerControllerCameraFlashMode.Auto:
				m_cameraFlashMode = UIImagePickerControllerCameraFlashMode.On;
				lblFlash.Text = "FlashOn".Translate();
				break;
			case UIImagePickerControllerCameraFlashMode.On:
				m_cameraFlashMode = UIImagePickerControllerCameraFlashMode.Off;
				lblFlash.Text = "FlashOff".Translate();
				break;
			default:
				m_cameraFlashMode = UIImagePickerControllerCameraFlashMode.Auto;
				lblFlash.Text = "FlashAuto".Translate();
				break;
			}
			m_picker.CameraFlashMode = m_cameraFlashMode;
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
