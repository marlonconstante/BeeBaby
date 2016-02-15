using System;
using CoreGraphics;
using UIKit;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using BeeBaby.Util;
using System.Threading.Tasks;
using BigTed;
using Infrastructure.Systems;
using BeeBaby.Navigations;

namespace BeeBaby.Controllers
{
	public partial class LoginConfigViewController : NavigationViewController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Controllers.LoginConfigViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public LoginConfigViewController(IntPtr handle) : base(handle)
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
			btnLogIn.SetTitle("LogIn".Translate(), UIControlState.Normal);
			btnForgotPassword.SetTitle("ForgotPassword".Translate(), UIControlState.Normal);
		}

		/// <summary>
		/// Logs the in.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void LogIn(UIButton sender)
		{
			Validators.RunIfValidLogin(txtUser.Text, txtPassword.Text, () => {
				PerformActionAsync(() => {
					return RemoteDataSystem.LogIn(txtUser.Text, txtPassword.Text);
				}, () => {
					OpenBenefitsPage(sender);
				}, () => {
					ShowErrorMessage("EmailAndPasswordNotMatch");
				});
			});
		}

		/// <summary>
		/// Forgots the password.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void ForgotPassword(UIButton sender)
		{
			var email = txtUser.Text;
			Validators.RunIfValidEmail(email, () => {
				PerformActionAsync(() => {
					return RemoteDataSystem.ResetPassword(email);
				}, () => {
					ShowSuccessMessage("PasswordResetSent");
				}, () => {
					ShowErrorMessage("PasswordResetNotSent");
				});
			});
		}

		/// <summary>
		/// Performs the action async.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="successAction">Success action.</param>
		/// <param name="errorAction">Error action.</param>
		async void PerformActionAsync(Func<Task<bool>> action, Action successAction, Action errorAction)
		{
			BTProgressHUD.Show("Wait".Translate());
			if (await action())
			{
				successAction();
			}
			else
			{
				errorAction();
			}
			BTProgressHUD.Dismiss();
		}

		/// <summary>
		/// Opens the benefits page.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void OpenBenefitsPage(UIButton sender) {
			PerformSegue("segueBenefitsConfig", sender);
		}

		/// <summary>
		/// Show the success message.
		/// </summary>
		/// <param name="key">Key.</param>
		void ShowSuccessMessage(string key) {
			new UIAlertView("Ready".Translate(), key.Translate(), null, "GotIt".Translate(), null).Show();
		}

		/// <summary>
		/// Show the error message.
		/// </summary>
		/// <param name="key">Key.</param>
		void ShowErrorMessage(string key) {
			new UIAlertView("Ops".Translate(), key.Translate(), null, "TryAgain".Translate(), null).Show();
		}
	}
}