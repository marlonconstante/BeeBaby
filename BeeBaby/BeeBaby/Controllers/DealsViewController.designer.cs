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
	[Register ("DealsViewController")]
	partial class DealsViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIWebView vwWeb { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (vwWeb != null) {
				vwWeb.Dispose ();
				vwWeb = null;
			}
		}
	}
}
