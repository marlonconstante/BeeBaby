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
	[Register ("MenuCell")]
	partial class MenuCell
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView imgIcon { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgIcon != null) {
				imgIcon.Dispose ();
				imgIcon = null;
			}

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}
		}
	}
}