using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using BeeBaby.ResourcesProviders;
using Domain.Moment;
using Application;
using BigTed;

namespace BeeBaby
{
	public partial class MainViewController : UIViewController
	{
		UIImagePickerController m_picker;

		public MainViewController(IntPtr handle) : base(handle)
		{
			// Custom initialization
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
				var imagePickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.Camera);
				m_picker = imagePickerProvider.GetUIImagePickerController();

				LoadCameraOverlayView();

				PresentViewController(m_picker, false, null);
			} else {
				btnDone(new UIBarButtonItem());
			}
		}

		void LoadCameraOverlayView()
		{
			// Setup the "OverlayView", basically the custom interface of the camera.
			var nibObjects = NSBundle.MainBundle.LoadNib("OverlayView", this, null);
			overlayView = (UIView) Runtime.GetNSObject(nibObjects.ValueAt(0));
			overlayView.Frame = m_picker.CameraOverlayView.Frame;
			m_picker.CameraOverlayView = overlayView;
		}

		partial void btnSnap(UIBarButtonItem sender)
		{
			m_picker.TakePicture();
		}

		partial void btnDone(UIBarButtonItem sender)
		{
			// Shows the spinner
			BTProgressHUD.Show(); 

			PerformSegue("segueMedia", sender);
			DismissViewController(true, null);
		}
	}
}

