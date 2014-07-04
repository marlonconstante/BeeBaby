using MonoTouch.UIKit;
using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using BeeBaby.ResourcesProviders;
using Domain.Moment;
using Application;
using Domain.Baby;
using Skahal.Infrastructure.Framework.Globalization;
using MonoTouch.AudioToolbox;
using System.Threading;
using BeeBaby.Util;
using System.Collections.Generic;

namespace BeeBaby
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
			FlurryAnalytics.Flurry.LogEvent("Camera: Abriu a Camera.", true);
			base.ViewDidLoad();

			CreateMoment();
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			View.BackgroundColor = UIColor.Black;
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
				var imagePickerDelegate = new MomentImagePickerDelegate(CurrentContext.Instance.Moment);
				m_mediaPickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.Camera, imagePickerDelegate);
				m_picker = m_mediaPickerProvider.GetUIImagePickerController();
				m_picker.CameraOverlayView = View;

				PresentViewController(m_picker, false, null);

				m_cameraFlashMode = UIImagePickerControllerCameraFlashMode.Off;
				ChangeFlashMode(btnFlash);

				View.BackgroundColor = UIColor.Clear;
			}
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			FlurryAnalytics.Flurry.EndTimedEvent("Camera: Abriu a Camera.", null);
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
		/// Creates the moment.
		/// </summary>
		void CreateMoment()
		{
			var momentService = new MomentService();
			new ImageProvider().DeleteTemporaryFiles();
			CurrentContext.Instance.Moment = momentService.CreateMoment();

			InvokeInBackground(() =>
			{
				CurrentContext.Instance.AllEvents = new EventService().GetAllEvents().ToList();
			});

			btnOpenTimeline.Hidden = !momentService.HasValidMoments();
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
				FlurryAnalytics.Flurry.LogEvent("Camera: Tocou som.");
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
			FlurryAnalytics.Flurry.LogEvent("Camera: Mudou Flash.");
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
			FlurryAnalytics.Flurry.LogEvent("Camera: Trocou entre as cameras.");

			bool front = m_picker.CameraDevice == UIImagePickerControllerCameraDevice.Front;
			View.BackgroundColor = UIColor.Black;
			UIView.Transition(m_picker.View, 0.75f, UIViewAnimationOptions.TransitionFlipFromLeft, () => {
				m_picker.CameraDevice = front ? UIImagePickerControllerCameraDevice.Rear : UIImagePickerControllerCameraDevice.Front;
				View.BackgroundColor = UIColor.Clear;
			}, null);
		}

		/// <summary>
		/// Opens the timeline.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void OpenTimeline(UIButton sender)
		{
			FlurryAnalytics.Flurry.LogEvent("Camera: Botão Timeline.");

			NSAction segueTimeline = () => {
				PresentingViewController.DismissViewController(false, null);
				Discard.ReleaseNavigation(NavigationController);
			};
			ShowProgressWhilePerforming(() => {
				if (m_picker != null)
				{
					m_picker.DismissViewController(false, segueTimeline);
				}
				else
				{
					segueTimeline();
				}
			}, false);
		}

		/// <summary>
		/// Takes the photo.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void TakePhoto(UIButton sender)
		{
			FlurryAnalytics.Flurry.LogEvent("Camera: Tirou uma foto.");
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
			ShowProgressWhilePerforming(() => {
				if (m_mediaPickerProvider != null)
				{
					var imagePickerDelegate = (MomentImagePickerDelegate) m_mediaPickerProvider.Delegate;
					imagePickerDelegate.WaitForPendingTasks();
				}
				StopSound();
				PerformSegue("segueMedia", sender);
				if (m_picker != null)
				{
					m_picker.DismissViewController(false, null);
				}
			}, false);
		}
	}
}