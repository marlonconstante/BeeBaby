﻿using MonoTouch.UIKit;
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
using System.Threading;

namespace BeeBaby
{
	public partial class CameraViewController : UIViewController
	{
		UIImagePickerController m_picker;
		MediaPickerProvider m_mediaPickerProvider;
		UIImagePickerControllerCameraFlashMode m_cameraFlashMode;
		OrientationNotification m_orientationNotification;
		SystemSound m_systemSound;

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
				m_mediaPickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.Camera);
				m_picker = m_mediaPickerProvider.GetUIImagePickerController();
				m_picker.CameraOverlayView = this.View;

				PresentViewController(m_picker, false, null);

				m_cameraFlashMode = UIImagePickerControllerCameraFlashMode.Off;
				ChangeFlashMode(btnFlash);

				LoadOrientationNotification();
			}

			View.BackgroundColor = UIColor.Clear;
		}

		/// <summary>
		/// Loads the orientation notification.
		/// </summary>
		void LoadOrientationNotification()
		{
			if (m_orientationNotification == null)
			{
				m_orientationNotification = new OrientationNotification(btnFlash.Superview, btnSound, btnSwitchCamera, btnOpenTimeline, btnTakePhoto, btnOpenMedia);
			}
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

			if (m_mediaPickerProvider != null)
			{
				m_mediaPickerProvider.Delegate.WaitForPendingTasks();
			}

			StopSound();
			PerformSegue("segueMedia", sender);
			DismissViewController(true, null);
		}
	}
}
