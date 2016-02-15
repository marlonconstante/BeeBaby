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
	[Register ("MediaViewController")]
	partial class MediaViewController
	{
		[Outlet]
		UIKit.UIButton btnAddMediaFromLibrary { get; set; }

		[Outlet]
		UIKit.UIButton btnNextStep { get; set; }

		[Outlet]
		UIKit.UICollectionView clnView { get; set; }

		[Action ("AddMediaFromLibrary:")]
		partial void AddMediaFromLibrary (UIKit.UIButton sender);

		[Action ("NextStep:")]
		partial void NextStep (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (clnView != null) {
				clnView.Dispose ();
				clnView = null;
			}

			if (btnNextStep != null) {
				btnNextStep.Dispose ();
				btnNextStep = null;
			}

			if (btnAddMediaFromLibrary != null) {
				btnAddMediaFromLibrary.Dispose ();
				btnAddMediaFromLibrary = null;
			}
		}
	}
}
