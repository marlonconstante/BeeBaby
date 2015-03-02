using System;
using System.Drawing;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.PCL.Globalization;

namespace BeeBaby.Controllers
{
	public partial class ConfigViewController : NavigationViewController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Controllers.ConfigViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public ConfigViewController(IntPtr handle) : base(handle)
		{
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
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			lblHeader.Text = "NewUserOrExisting".Translate();
			txtUser.Placeholder = "EnterUserName".Translate();
			txtPassword.Placeholder = "EnterPassword".Translate();
			lblObservations.Text = "WithoutSpamAndConfidentialData".Translate();
			btnSignUp.SetTitle("SignUp".Translate(), UIControlState.Normal);
			btnLogIn.SetTitle("LogIn".Translate(), UIControlState.Normal);
			btnForgotPassword.SetTitle("ForgotPassword".Translate(), UIControlState.Normal);
		}

		/// <summary>
		/// Signs up.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void SignUp(UIButton sender)
		{

		}

		/// <summary>
		/// Logs the in.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void LogIn(UIButton sender)
		{

		}

		/// <summary>
		/// Forgots the password.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void ForgotPassword(UIButton sender)
		{

		}
	}
}