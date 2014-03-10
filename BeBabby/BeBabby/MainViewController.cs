using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;
using Xamarin.Media;
using System.Threading.Tasks;

namespace BeBabby
{
	public partial class MainViewController : UIViewController
	{
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

		partial void btnStartCamera (MonoTouch.UIKit.UIButton sender)
		{

		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			var picker = new MediaPicker ();

			if (!picker.IsCameraAvailable)
				Console.WriteLine("No Camera");
			else {
				var tempName = new Guid().ToString();
				picker.TakePhotoAsync(new StoreCameraMediaOptions
				{
					Name = tempName + ".jpg",
					Directory = "MediaPickerSample"
				}).ContinueWith (t => {
					if (t.IsCanceled) {
						Console.WriteLine("User cancelled");
						return;
					}
					Console.WriteLine("Photo succeeded");

					Console.WriteLine(t.Result.Path);
				}, TaskScheduler.FromCurrentSynchronizationContext());
			}

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

		partial void showInfo(NSObject sender)
		{
		}
	}
}

