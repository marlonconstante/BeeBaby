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
	[Register ("OnBoardingModalViewController")]
	partial class OnBoardingModalViewController
	{
		[Outlet]
		UIKit.UIView vwEditMoment { get; set; }

		[Outlet]
		UIKit.UIView vwLetsStart { get; set; }

		[Outlet]
		UIKit.UIView vwViewAndShare { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (vwEditMoment != null) {
				vwEditMoment.Dispose ();
				vwEditMoment = null;
			}

			if (vwViewAndShare != null) {
				vwViewAndShare.Dispose ();
				vwViewAndShare = null;
			}

			if (vwLetsStart != null) {
				vwLetsStart.Dispose ();
				vwLetsStart = null;
			}
		}
	}
}
