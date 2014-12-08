// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BeeBaby.Controllers
{
	[Register ("CameraOptionsModalViewController")]
	partial class CameraOptionsModalViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnImportPhotos { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTakePhotos { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView vwPopover { get; set; }

		[Action ("ImportPhotos:")]
		partial void ImportPhotos (MonoTouch.UIKit.UIButton sender);

		[Action ("TakePhotos:")]
		partial void TakePhotos (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (vwPopover != null) {
				vwPopover.Dispose ();
				vwPopover = null;
			}

			if (btnTakePhotos != null) {
				btnTakePhotos.Dispose ();
				btnTakePhotos = null;
			}

			if (btnImportPhotos != null) {
				btnImportPhotos.Dispose ();
				btnImportPhotos = null;
			}
		}
	}
}
