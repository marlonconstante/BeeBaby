using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby.Controllers
{
	public partial class InviteFriendsController : NavigationViewController
	{
		public InviteFriendsController(IntPtr handle) : base(handle)
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

		}
	}
}