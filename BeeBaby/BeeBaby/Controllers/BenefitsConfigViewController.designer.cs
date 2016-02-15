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
	[Register ("BenefitsConfigViewController")]
	partial class BenefitsConfigViewController
	{
		[Outlet]
		UIKit.UIButton btnContinue { get; set; }

		[Outlet]
		UIKit.UIButton btnLogOut { get; set; }

		[Outlet]
		UIKit.UILabel lblAdvantages { get; set; }

		[Outlet]
		UIKit.UILabel lblObservations { get; set; }

		[Outlet]
		UIKit.UILabel lblThankYou { get; set; }

		[Outlet]
		UIKit.UIScrollView scrView { get; set; }

		[Action ("Continue:")]
		partial void Continue (UIKit.UIButton sender);

		[Action ("LogOut:")]
		partial void LogOut (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (scrView != null) {
				scrView.Dispose ();
				scrView = null;
			}

			if (lblThankYou != null) {
				lblThankYou.Dispose ();
				lblThankYou = null;
			}

			if (lblAdvantages != null) {
				lblAdvantages.Dispose ();
				lblAdvantages = null;
			}

			if (lblObservations != null) {
				lblObservations.Dispose ();
				lblObservations = null;
			}

			if (btnContinue != null) {
				btnContinue.Dispose ();
				btnContinue = null;
			}

			if (btnLogOut != null) {
				btnLogOut.Dispose ();
				btnLogOut = null;
			}
		}
	}
}
