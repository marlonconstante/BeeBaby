using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using BeeBaby.Util;
using BigTed;
using Infrastructure.Systems;
using Parse;

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
		/// Signs up.
		/// </summary>
		async void SignUp()
		{
			BTProgressHUD.Show();

			await RemoteDataSystem.SignUp(txtUser.Text, txtPassword.Text);

			BTProgressHUD.ShowSuccessWithStatus(string.Empty, 2000);
		}

		/// <summary>
		/// Login the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Login(UIButton sender)
		{
			Email.RunIfValid(txtUser.Text, () => {
				SignUp();
			});
		}
	}
}