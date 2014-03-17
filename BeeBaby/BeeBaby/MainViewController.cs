using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;
using Xamarin.Media;
using System.Threading.Tasks;
using MonoTouch.ObjCRuntime;
using BeBabby.ResourcesProviders;

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

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

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

