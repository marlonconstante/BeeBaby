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
	[Register ("EventViewCell")]
	partial class EventViewCell
	{
		[Outlet]
		UIKit.UIImageView imgEventTagIcon { get; set; }

		[Outlet]
		UIKit.UILabel lblEventDesc { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgEventTagIcon != null) {
				imgEventTagIcon.Dispose ();
				imgEventTagIcon = null;
			}

			if (lblEventDesc != null) {
				lblEventDesc.Dispose ();
				lblEventDesc = null;
			}
		}
	}
}
