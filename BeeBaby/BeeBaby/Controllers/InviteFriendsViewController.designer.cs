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
	[Register ("InviteFriendsViewController")]
	partial class InviteFriendsViewController
	{
		[Outlet]
		UIKit.UIButton btnInvite { get; set; }

		[Outlet]
		UIKit.UILabel lblFriendUser { get; set; }

		[Outlet]
		UIKit.UIScrollView scrView { get; set; }

		[Outlet]
		BeeBaby.VisualElements.TextField txtFriendUser { get; set; }

		[Action ("Invite:")]
		partial void Invite (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (scrView != null) {
				scrView.Dispose ();
				scrView = null;
			}

			if (lblFriendUser != null) {
				lblFriendUser.Dispose ();
				lblFriendUser = null;
			}

			if (txtFriendUser != null) {
				txtFriendUser.Dispose ();
				txtFriendUser = null;
			}

			if (btnInvite != null) {
				btnInvite.Dispose ();
				btnInvite = null;
			}
		}
	}
}
