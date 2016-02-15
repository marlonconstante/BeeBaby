// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace BeeBaby.Controllers
{
	[Register ("CameraViewController")]
	partial class CameraViewController
	{
		[Outlet]
		UIKit.UIButton btnFlash { get; set; }

		[Outlet]
		UIKit.UIButton btnClose { get; set; }

		[Outlet]
		UIKit.UIButton btnSound { get; set; }

		[Outlet]
		UIKit.UIButton btnSwitchCamera { get; set; }

		[Outlet]
		UIKit.UIButton btnTakePhoto { get; set; }

		[Outlet]
		UIKit.UILabel lblFlash { get; set; }

		[Outlet]
		UIKit.UILabel lblReady { get; set; }

		[Outlet]
		UIKit.UIView vwFlash { get; set; }

		[Action ("ChangeFlashMode:")]
		partial void ChangeFlashMode (UIKit.UIButton sender);

		[Action ("OpenMedia:")]
		partial void OpenMedia (UIKit.UIButton sender);

		[Action ("Close:")]
		partial void Close (UIKit.UIButton sender);

		[Action ("PlaySound:")]
		partial void PlaySound (UIKit.UIButton sender);

		[Action ("SwitchCamera:")]
		partial void SwitchCamera (UIKit.UIButton sender);

		[Action ("TakePhoto:")]
		partial void TakePhoto (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnFlash != null) {
				btnFlash.Dispose ();
				btnFlash = null;
			}

			if (btnClose != null) {
				btnClose.Dispose ();
				btnClose = null;
			}

			if (vwFlash != null) {
				vwFlash.Dispose ();
				vwFlash = null;
			}

			if (btnSound != null) {
				btnSound.Dispose ();
				btnSound = null;
			}

			if (btnSwitchCamera != null) {
				btnSwitchCamera.Dispose ();
				btnSwitchCamera = null;
			}

			if (btnTakePhoto != null) {
				btnTakePhoto.Dispose ();
				btnTakePhoto = null;
			}

			if (lblFlash != null) {
				lblFlash.Dispose ();
				lblFlash = null;
			}

			if (lblReady != null) {
				lblReady.Dispose ();
				lblReady = null;
			}
		}
	}
}
