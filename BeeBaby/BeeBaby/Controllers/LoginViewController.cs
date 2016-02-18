using System;

using Foundation;
using UIKit;
using CoreGraphics;
using BeeBaby.Util;
using BigTed;
using Infrastructure.Systems;
using Parse;
using Skahal.Infrastructure.Framework.PCL.Globalization;

namespace BeeBaby.Controllers
{
	public partial class LoginViewController : BaseViewController
	{
		public LoginViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			NavigationController.NavigationBarHidden = true;

			txtUser.PlaceholderColor = UIColor.Gray;
			txtPassword.PlaceholderColor = UIColor.Gray;
		}

		/// <summary>
		/// Views the did layout subviews.
		/// </summary>
		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			if (scrView.ContentSize == CGSize.Empty)
			{
				scrView.ContentSize = new CGSize(320f, 504f);
			}
		}

		/// <summary>
		/// Enter the specified signUp.
		/// </summary>
		/// <param name="signUp">If set to <c>true</c> sign up.</param>
		/// ZBKEY
		async void Enter(bool signUp)
		{
			BTProgressHUD.Show();

			if (signUp ? await RemoteDataSystem.LogIn(txtUser.Text, txtPassword.Text) : await RemoteDataSystem.SignUp(txtUser.Text, txtPassword.Text))
			{
				BTProgressHUD.Dismiss();
			}
			else
			{
				BTProgressHUD.Dismiss();

				new UIAlertView("Ops".Translate(), (signUp ? "SignUpError" : "EmailAndPasswordNotMatch").Translate(), null, "TryAgain".Translate(), null).Show();
			}
		}

		/// <summary>
		/// Resets the password.
		/// </summary>
		void ResetPassword()
		{
			var email = txtUser.Text;
			Validators.RunIfValidEmail(email, () => {

				ParseUser.RequestPasswordResetAsync(email);
				Console.WriteLine("Enviado ao seu email.");
			});
		}

		partial void forgotPass(UIButton sender)
		{
			ResetPassword();
		}

		/// <summary>
		/// Login the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// ZBKEY
		partial void Login(UIButton sender)
		{
			Validators.RunIfValidLogin(txtUser.Text, txtPassword.Text, () => {
				Enter(true);
			});
		}
	}
}