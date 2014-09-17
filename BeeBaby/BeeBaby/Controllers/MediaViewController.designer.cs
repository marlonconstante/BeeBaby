// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BeeBaby
{
	[Register ("MediaViewController")]
	partial class MediaViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnAddMediaFromLibrary { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnNextStep { get; set; }

		[Outlet]
		MonoTouch.UIKit.UICollectionView clnView { get; set; }

		[Action ("AddMediaFromLibrary:")]
		partial void AddMediaFromLibrary (MonoTouch.UIKit.UIButton sender);

		[Action ("NextStep:")]
		partial void NextStep (MonoTouch.UIKit.UIButton sender);
		
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
