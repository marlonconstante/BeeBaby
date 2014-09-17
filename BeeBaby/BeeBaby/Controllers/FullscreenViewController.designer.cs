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
	[Register ("FullscreenViewController")]
	partial class FullscreenViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel lblAge { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblEvent { get; set; }

		[Outlet]
		SwipeViewer.SwipeView vwSwipe { get; set; }

		[Action ("Close:")]
		partial void Close (MonoTouch.UIKit.UIButton sender);

		[Action ("Share:")]
		partial void Share (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (lblAge != null) {
				lblAge.Dispose ();
				lblAge = null;
			}

			if (lblEvent != null) {
				lblEvent.Dispose ();
				lblEvent = null;
			}

			if (vwSwipe != null) {
				vwSwipe.Dispose ();
				vwSwipe = null;
			}
		}
	}
}
