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
	[Register ("BabyViewController")]
	partial class BabyViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel lblBirthDate { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISegmentedControl segGender { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView vwBirthDay { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView vwBirthTime { get; set; }

		[Action ("Save:")]
		partial void Save (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (lblBirthDate != null) {
				lblBirthDate.Dispose ();
				lblBirthDate = null;
			}

			if (lblName != null) {
				lblName.Dispose ();
				lblName = null;
			}

			if (segGender != null) {
				segGender.Dispose ();
				segGender = null;
			}

			if (txtName != null) {
				txtName.Dispose ();
				txtName = null;
			}

			if (vwBirthDay != null) {
				vwBirthDay.Dispose ();
				vwBirthDay = null;
			}

			if (vwBirthTime != null) {
				vwBirthTime.Dispose ();
				vwBirthTime = null;
			}
		}
	}
}
