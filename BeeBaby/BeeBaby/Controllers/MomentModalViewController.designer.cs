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
	[Register ("MomentModalViewController")]
	partial class MomentModalViewController
	{
		[Outlet]
		BeeBaby.VisualElements.Button btnCancel { get; set; }

		[Outlet]
		UIKit.UIImageView imgEventBadge { get; set; }

		[Outlet]
		BeeBaby.VisualElements.Label lblDescription { get; set; }

		[Outlet]
		BeeBaby.VisualElements.Label lblEvent { get; set; }

		[Action ("Close:")]
		partial void Close (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnCancel != null) {
				btnCancel.Dispose ();
				btnCancel = null;
			}

			if (imgEventBadge != null) {
				imgEventBadge.Dispose ();
				imgEventBadge = null;
			}

			if (lblDescription != null) {
				lblDescription.Dispose ();
				lblDescription = null;
			}

			if (lblEvent != null) {
				lblEvent.Dispose ();
				lblEvent = null;
			}
		}
	}
}
