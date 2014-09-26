// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BeeBaby.Controllers
{
	[Register ("InviteFriendsController")]
	partial class InviteFriendsController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnInvite { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblFriendUser { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView scrView { get; set; }

		[Outlet]
		BeeBaby.VisualElements.TextField txtFriendUser { get; set; }

		[Action ("Invite:")]
		partial void Invite (MonoTouch.UIKit.UIButton sender);
		
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
