// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BeeBaby
{
	[Register ("CameraViewController")]
	partial class CameraViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnFlash { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnOpenMedia { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnOpenTimeline { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSwitchCamera { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTakePhoto { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblFlash { get; set; }

		[Action ("OpenMedia:")]
		partial void OpenMedia (MonoTouch.UIKit.UIButton sender);

		[Action ("TakePhoto:")]
		partial void TakePhoto (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnOpenMedia != null) {
				btnOpenMedia.Dispose ();
				btnOpenMedia = null;
			}

			if (btnTakePhoto != null) {
				btnTakePhoto.Dispose ();
				btnTakePhoto = null;
			}

			if (btnOpenTimeline != null) {
				btnOpenTimeline.Dispose ();
				btnOpenTimeline = null;
			}

			if (btnSwitchCamera != null) {
				btnSwitchCamera.Dispose ();
				btnSwitchCamera = null;
			}

			if (btnFlash != null) {
				btnFlash.Dispose ();
				btnFlash = null;
			}

			if (lblFlash != null) {
				lblFlash.Dispose ();
				lblFlash = null;
			}
		}
	}
}
