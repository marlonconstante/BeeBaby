using UIKit;
using System;
using System.Linq;
using Foundation;
using ObjCRuntime;
using BeeBaby.ResourcesProviders;
using Application;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using AudioToolbox;
using System.Collections.Generic;
using BeeBaby.Navigations;
using BeeBaby.Media;

namespace BeeBaby.Controllers
{
	public partial class CameraViewController : NavigationViewController
	{
		UIImagePickerController m_picker;
		MediaPickerProvider m_mediaPickerProvider;
		UIImagePickerControllerCameraFlashMode m_cameraFlashMode;
		SystemSound m_systemSound;

		public CameraViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			((MomentNavigationController) NavigationController).CreateMoment();
		}

		/// <summary>
		/// View did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			if (MediaPickerProvider.IsCameraAvailable() && m_picker == null)
			{
				var imagePickerDelegate = new MomentImagePickerDelegate(CurrentContext.Instance.Moment);
				m_mediaPickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.Camera, imagePickerDelegate);
				m_picker = m_mediaPickerProvider.GetUIImagePickerController();

				View.InsertSubview(m_picker.View, 0);

				AddChildViewController(m_picker);
				m_picker.DidMoveToParentViewController(this);

				m_cameraFlashMode = UIImagePickerControllerCameraFlashMode.Off;
				ChangeFlashMode(btnFlash);
			}

		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			StopSound();
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			lblReady.Text = "Ready".Translate();
		}

		/// <summary>
		/// Gets the supported orientation views.
		/// </summary>
		/// <returns>The supported orientation views.</returns>
		public override IEnumerable<UIView> GetSupportedOrientationViews()
		{
			if (IsViewLoaded)
			{
				return new UIView[] { btnFlash.Superview, btnSound, btnSwitchCamera, btnTakePhoto };
			}
			return base.GetSupportedOrientationViews();
		}

		/// <summary>
		/// Determines whether this instance is show status bar.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		public override bool IsShowStatusBar()
		{
			return false;
		}

		/// <summary>
		/// Stops the sound.
		/// </summary>
		void StopSound()
		{
			if (m_systemSound != null)
			{
				m_systemSound.Close();
				m_systemSound = null;
			}
		}

		/// <summary>
		/// Play the sound.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void PlaySound(UIButton sender)
		{
			if (m_systemSound == null)
			{
				string filePath = NSBundle.MainBundle.PathForResource("lake-waves", "mp3");
				m_systemSound = SystemSound.FromFile(filePath);
				m_systemSound.PlaySystemSound();
				m_systemSound.AddSystemSoundCompletion(() => {
					m_systemSound.PlaySystemSound();
				});
			}
			else
			{
				StopSound();
			}
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
			UIView.Transition(m_picker.View, 0.75f, UIViewAnimationOptions.TransitionFlipFromLeft, () => {
				m_picker.CameraDevice = front ? UIImagePickerControllerCameraDevice.Rear : UIImagePickerControllerCameraDevice.Front;
			}, null);
		}

		/// <summary>
		/// Close the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Close(UIButton sender)
		{

			ShowProgressWhilePerforming(() => {
				if (m_mediaPickerProvider != null)
				{
					var imagePickerDelegate = (MomentImagePickerDelegate) m_mediaPickerProvider.Delegate;
					imagePickerDelegate.WaitForPendingTasks();
				}
				((MomentNavigationController) NavigationController).Close();
			}, false);
		}

		/// <summary>
		/// Takes the photo.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void TakePhoto(UIButton sender)
		{
			m_picker.TakePicture();
			UIView.Animate(0.1d, () => {
				vwFlash.Alpha = 0.9f;
			}, () => {
				UIView.Animate(0.2d, () => {
					vwFlash.Alpha = 0f;
				});
			});
		}

		/// <summary>
		/// Opens the media.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void OpenMedia(UIButton sender)
		{
			ShowProgressWhilePerforming(() => {
				if (m_mediaPickerProvider != null)
				{
					var imagePickerDelegate = (MomentImagePickerDelegate) m_mediaPickerProvider.Delegate;
					imagePickerDelegate.WaitForPendingTasks();
				}
				PerformSegue("segueMedia", sender);
			}, false);
		}
	}
}