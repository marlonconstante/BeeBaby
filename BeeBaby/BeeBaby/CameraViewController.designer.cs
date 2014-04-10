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
		MonoTouch.UIKit.UIButton btnOpenMedia { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTakePhoto { get; set; }

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
		}
	}
}
