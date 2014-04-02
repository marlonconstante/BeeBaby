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
	[Register ("CollectionViewCell")]
	partial class CollectionViewCell
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView p_imageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (p_imageView != null) {
				p_imageView.Dispose ();
				p_imageView = null;
			}
		}
	}
}
