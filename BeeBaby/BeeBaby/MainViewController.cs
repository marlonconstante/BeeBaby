using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using BeBabby.ResourcesProviders;
using Domain.Moment;
using Application;

namespace BeeBaby
{
	public partial class MainViewController : UIViewController
	{
		UIImagePickerController m_picker;

		public MainViewController(IntPtr handle) : base(handle)
		{
			// Custom initialization
		}

		#region View lifecycle

		/// <summary>
		/// View did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			// create the moment, saves and generate a ID for future use.
			var momentService = new MomentService();
			CurrentMoment.Instance.Moment = momentService.CreateMoment();

			var imagePickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.Camera);
			m_picker = imagePickerProvider.GetUIImagePickerController();

			LoadOverlayView();

			PresentViewController(m_picker, false, null);
		}

		#endregion

		void LoadOverlayView()
		{
			if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
			{
				// Setup the "OverlayView", basically the custom interface of the camera.
				var nibObjects = NSBundle.MainBundle.LoadNib("OverlayView", this, null);
				overlayView = (UIView)Runtime.GetNSObject(nibObjects.ValueAt(0));
				overlayView.Frame = m_picker.CameraOverlayView.Frame;
				m_picker.CameraOverlayView = overlayView;
			}
		}

		partial void btnSnap(UIBarButtonItem sender)
		{
			m_picker.TakePicture();
		}

		partial void btnDone(UIBarButtonItem sender)
		{
			PerformSegue("cameraDone", sender);
			DismissViewController(true, null);
		}
	}
}

