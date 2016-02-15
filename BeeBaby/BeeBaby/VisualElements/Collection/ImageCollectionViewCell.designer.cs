// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace BeeBaby.VisualElements
{
	[Register ("ImageCollectionViewCell")]
	partial class ImageCollectionViewCell
	{
		[Outlet]
		UIKit.UIImageView imgCheckmark { get; set; }

		[Outlet]
		UIKit.UIImageView imgPhoto { get; set; }

		[Outlet]
		UIKit.UIView vwOverlay { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgPhoto != null) {
				imgPhoto.Dispose ();
				imgPhoto = null;
			}

			if (vwOverlay != null) {
				vwOverlay.Dispose ();
				vwOverlay = null;
			}

			if (imgCheckmark != null) {
				imgCheckmark.Dispose ();
				imgCheckmark = null;
			}
		}
	}
}
