using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BeeBaby.VisualElements
{
	public partial class ScrollView : UIScrollView
	{
		public ScrollView (IntPtr handle) : base (handle)
		{
		}

		/// <summary>
		/// Toucheses the should begin.
		/// </summary>
		/// <returns><c>true</c>, if should begin was touchesed, <c>false</c> otherwise.</returns>
		/// <param name="touches">Touches.</param>
		/// <param name="ev">Event.</param>
		/// <param name="view">View.</param>
		public override bool TouchesShouldBegin(NSSet touches, UIEvent ev, UIView view)
		{
			var touch = touches.AnyObject as UITouch;
			if (touch.Phase == UITouchPhase.Moved)
			{
				return false;
			}
			else
			{
				return base.TouchesShouldBegin(touches, ev, view);
			}
		}
	}
}