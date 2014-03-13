using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;
using Xamarin.Media;
using System.Threading.Tasks;
using MonoTouch.ObjCRuntime;

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

			SetUIImagePickerController();

			PresentViewController(m_picker, false, null);
		}

		#endregion

		void LoadOverlayView()
		{
			// Setup the "OverlayView", basically the custom interface of the camera.
			var nibObjects = NSBundle.MainBundle.LoadNib("OverlayView", this, null);
			overlayView = (UIView)Runtime.GetNSObject(nibObjects.ValueAt(0));
			overlayView.Frame = m_picker.CameraOverlayView.Frame;
			m_picker.CameraOverlayView = overlayView;
		}

		void SetUIImagePickerController()
		{
			// Setup the UIImagePickerController
			m_picker = new UIImagePickerController();
			if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
			{
				m_picker.SourceType = UIImagePickerControllerSourceType.Camera;
				m_picker.PrefersStatusBarHidden();
				m_picker.ShowsCameraControls = false;
				m_picker.CameraCaptureMode = UIImagePickerControllerCameraCaptureMode.Photo;
				LoadOverlayView();
			}
			else
			{
				m_picker.SourceType = UIImagePickerControllerSourceType.SavedPhotosAlbum;
			}
			m_picker.ModalPresentationStyle = UIModalPresentationStyle.CurrentContext;
			m_picker.Delegate = new ImagePickerDelegate();
		}

		partial void btnSnap(UIBarButtonItem sender)
		{
			m_picker.TakePicture();
		}

		partial void btnDone(UIBarButtonItem sender)
		{
			throw new NotImplementedException();
		}
	}
}

