// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BeeBaby.Controllers
{
	[Register ("LoginConfigViewController")]
	partial class LoginConfigViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnForgotPassword { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnLogIn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblHeader { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblObservations { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView scrView { get; set; }

		[Outlet]
		BeeBaby.VisualElements.TextField txtPassword { get; set; }

		[Outlet]
		BeeBaby.VisualElements.TextField txtUser { get; set; }

		[Action ("ForgotPassword:")]
		partial void ForgotPassword (MonoTouch.UIKit.UIButton sender);

		[Action ("LogIn:")]
		partial void LogIn (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnForgotPassword != null) {
				btnForgotPassword.Dispose ();
				btnForgotPassword = null;
			}

			if (btnLogIn != null) {
				btnLogIn.Dispose ();
				btnLogIn = null;
			}

			if (lblHeader != null) {
				lblHeader.Dispose ();
				lblHeader = null;
			}

			if (lblObservations != null) {
				lblObservations.Dispose ();
				lblObservations = null;
			}

			if (scrView != null) {
				scrView.Dispose ();
				scrView = null;
			}

			if (txtPassword != null) {
				txtPassword.Dispose ();
				txtPassword = null;
			}

			if (txtUser != null) {
				txtUser.Dispose ();
				txtUser = null;
			}
		}
	}
}
