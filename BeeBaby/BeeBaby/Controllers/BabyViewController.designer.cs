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
	[Register ("BabyViewController")]
	partial class BabyViewController
	{
		[Outlet]
		UIKit.UILabel lblBirthDate { get; set; }

		[Outlet]
		UIKit.UILabel lblName { get; set; }

		[Outlet]
		UIKit.UILabel lblUser { get; set; }

		[Outlet]
		UIKit.UIScrollView scrView { get; set; }

		[Outlet]
		UIKit.UISegmentedControl segGender { get; set; }

		[Outlet]
		BeeBaby.VisualElements.TextField txtName { get; set; }

		[Outlet]
		BeeBaby.VisualElements.TextField txtUser { get; set; }

		[Outlet]
		BeeBaby.VisualElements.ViewDatePicker vwBirthDay { get; set; }

		[Outlet]
		BeeBaby.VisualElements.ViewDatePicker vwBirthTime { get; set; }
		
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

			if (lblUser != null) {
				lblUser.Dispose ();
				lblUser = null;
			}

			if (scrView != null) {
				scrView.Dispose ();
				scrView = null;
			}

			if (segGender != null) {
				segGender.Dispose ();
				segGender = null;
			}

			if (txtName != null) {
				txtName.Dispose ();
				txtName = null;
			}

			if (txtUser != null) {
				txtUser.Dispose ();
				txtUser = null;
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
