using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using Infrastructure.Systems;
using Infrastructure.Systems.Domain;
using Application;
using BeeBaby.Util;
using Skahal.Infrastructure.Framework.Globalization;
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
		/// Invite the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Invite(UIButton sender)
		{
			var friendUserEmail = txtFriendUser.Text;
			Email.RunIfValid(friendUserEmail, () => {
				RemoteDataSystem.SendInvite(new FriendshipShared {
					UserEmail = CurrentContext.Instance.CurrentBaby.Email,
					FriendUserEmail = friendUserEmail
				});

				BTProgressHUD.ShowSuccessWithStatus(string.Empty, 2000);
			});
		}
	}
}