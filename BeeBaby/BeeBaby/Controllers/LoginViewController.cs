using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using BeeBaby.Util;
using BigTed;
using Infrastructure.Systems;
using Parse;
using Skahal.Infrastructure.Framework.Globalization;

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

			if (scrView.ContentSize == SizeF.Empty)
			{
				scrView.ContentSize = new SizeF(320f, 504f);
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

			if (signUp ? await RemoteDataSystem.Login(txtUser.Text, txtPassword.Text) :await RemoteDataSystem.SignUp(txtUser.Text, txtPassword.Text))
			{
				BTProgressHUD.Dismiss();
				//Windows.ChangeRootViewController("SlideoutNavigationController");
				Console.WriteLine("Sucesso");
			}
			else
			{
				BTProgressHUD.Dismiss();
				Console.WriteLine("Error");

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