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
	[Register ("MediaCollectionViewController")]
	partial class MediaCollectionViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem btnAddMediaFromLibrary { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem btnNextStep { get; set; }

		[Action ("AddMediaFromLibrary:")]
		partial void AddMediaFromLibrary (MonoTouch.UIKit.UIBarButtonItem sender);

		[Action ("NextStep:")]
		partial void NextStep (MonoTouch.UIKit.UIBarButtonItem sender);
		
		void ReleaseDesignerOutlets ()
		{
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
