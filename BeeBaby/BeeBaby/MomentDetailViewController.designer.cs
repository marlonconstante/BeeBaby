// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BeBabby
{
	[Register ("MomentDetailViewController")]
	partial class MomentDetailViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnSelectEvent { get; set; }

		[Action ("SelectEvent:")]
		partial void SelectEvent (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnSelectEvent != null) {
				btnSelectEvent.Dispose ();
				btnSelectEvent = null;
			}
		}
	}
}
