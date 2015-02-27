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
	[Register ("ConfigViewController")]
	partial class ConfigViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIScrollView scrView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (scrView != null) {
				scrView.Dispose ();
				scrView = null;
			}
		}
	}
}
