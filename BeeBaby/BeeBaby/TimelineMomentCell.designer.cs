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
	[Register ("TimelineMomentCell")]
	partial class TimelineMomentCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel lblAge { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblDate { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblEventName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblWhere { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblWho { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView vwPhotos { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblEventName != null) {
				lblEventName.Dispose ();
				lblEventName = null;
			}

			if (lblDate != null) {
				lblDate.Dispose ();
				lblDate = null;
			}

			if (lblWhere != null) {
				lblWhere.Dispose ();
				lblWhere = null;
			}

			if (lblWho != null) {
				lblWho.Dispose ();
				lblWho = null;
			}

			if (vwPhotos != null) {
				vwPhotos.Dispose ();
				vwPhotos = null;
			}

			if (lblAge != null) {
				lblAge.Dispose ();
				lblAge = null;
			}
		}
	}
}
