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
	[Register ("ImageShareViewController")]
	partial class ImageShareViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView ivwBackgroundImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView vwMainView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ivwBackgroundImage != null) {
				ivwBackgroundImage.Dispose ();
				ivwBackgroundImage = null;
			}

			if (lblLabel != null) {
				lblLabel.Dispose ();
				lblLabel = null;
			}

			if (vwMainView != null) {
				vwMainView.Dispose ();
				vwMainView = null;
			}
		}
	}
}
