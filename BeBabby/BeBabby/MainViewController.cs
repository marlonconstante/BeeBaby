using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;
using Xamarin.Media;
using System.Threading.Tasks;
using MonoTouch.ObjCRuntime;

namespace BeBabby
{
	public partial class MainViewController : UIViewController
	{
		UIImagePickerController m_picker;

		public MainViewController(IntPtr handle) : base(handle)
		{
			// Custom initialization
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			// Setup the UIImagePickerController
			m_picker = new UIImagePickerController();
			m_picker.SourceType = UIImagePickerControllerSourceType.Camera;
			m_picker.ModalPresentationStyle = UIModalPresentationStyle.CurrentContext;
			m_picker.PrefersStatusBarHidden();
			m_picker.ShowsCameraControls = false;
			m_picker.Delegate = new ImagePickerDelegate();

			// Setup the "OverlayView", basically the custom interface of the camera.
			var nibObjects = NSBundle.MainBundle.LoadNib("OverlayView", this, null);
			overlayView = (UIView)Runtime.GetNSObject(nibObjects.ValueAt(0));
			overlayView.Frame = m_picker.CameraOverlayView.Frame;
			m_picker.CameraOverlayView = overlayView;

			PresentViewController(m_picker, false, null);
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
		}

		#endregion
		partial void btnSnap(MonoTouch.UIKit.UIBarButtonItem sender)
		{
			m_picker.TakePicture();
		}

		partial void btnDone(MonoTouch.UIKit.UIBarButtonItem sender)
		{
			throw new System.NotImplementedException();
		}
	}
}

