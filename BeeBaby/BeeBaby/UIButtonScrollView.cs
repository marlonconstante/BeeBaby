// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public partial class UIButtonScrollView : UIScrollView
	{
		public UIButtonScrollView (IntPtr handle) : base (handle)
		{
		}

		public UIButtonScrollView(RectangleF frame) : base(frame)
		{
		}

		public override bool TouchesShouldCancelInContentView(UIView view)
		{
			return true;
		}
	}
}