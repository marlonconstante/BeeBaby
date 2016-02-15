using System;

using Foundation;
using UIKit;
using CoreGraphics;
using Infrastructure.Systems;
using Infrastructure.Systems.Domain;
using Application;
using BeeBaby.Util;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using BigTed;

namespace BeeBaby.Controllers
{
	public partial class InviteFriendsViewController : NavigationViewController
	{
		public InviteFriendsViewController(IntPtr handle) : base(handle)
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
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			txtFriendUser.BecomeFirstResponder();
		}

		/// <summary>
		/// Sends the invite.
		/// </summary>
		async void SendInvite()
		{
			BTProgressHUD.Show();

			await RemoteDataSystem.SendInvite(new FriendshipShared {
				UserEmail = CurrentContext.Instance.CurrentBaby.Email,
				FriendUserEmail = txtFriendUser.Text
			});

			BTProgressHUD.ShowSuccessWithStatus(string.Empty, 2000);
		}

		/// <summary>
		/// Invite the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Invite(UIButton sender)
		{
			Validators.RunIfValidEmail(txtFriendUser.Text, () => {
				SendInvite();
			});
		}
	}
}