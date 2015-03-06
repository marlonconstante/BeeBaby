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
	[Register ("ConfigOnBoardingModalViewController")]
	partial class ConfigOnBoardingModalViewController
	{
		[Outlet]
		BeeBaby.VisualElements.Label lblSavedPhotos { get; set; }

		[Outlet]
		BeeBaby.VisualElements.Label lblStart { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblSavedPhotos != null) {
				lblSavedPhotos.Dispose ();
				lblSavedPhotos = null;
			}

			if (lblStart != null) {
				lblStart.Dispose ();
				lblStart = null;
			}
		}
	}
}
