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
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		UIKit.UIButton btnForgotPass { get; set; }

		[Outlet]
		UIKit.UIButton btnLogin { get; set; }

		[Outlet]
		UIKit.UIScrollView scrView { get; set; }

		[Outlet]
		BeeBaby.VisualElements.TextField txtPassword { get; set; }

		[Outlet]
		BeeBaby.VisualElements.TextField txtUser { get; set; }

		[Action ("forgotPass:")]
		partial void forgotPass (UIKit.UIButton sender);

		[Action ("Login:")]
		partial void Login (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
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

			if (btnForgotPass != null) {
				btnForgotPass.Dispose ();
				btnForgotPass = null;
			}
		}
	}
}
