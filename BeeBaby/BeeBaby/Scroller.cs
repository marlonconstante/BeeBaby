using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public abstract class Scroller
	{
		private Scroller()
		{
		}

		/// <summary>
		/// Move the specified view, x and y.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static void Move(UIView view, float x, float y)
		{
			UIView.BeginAnimations(string.Empty, IntPtr.Zero);
			UIView.SetAnimationDuration(0.3d);

			RectangleF frame = view.Frame;
			frame.X += x; 
			frame.Y += y; 
			view.Frame = frame;

			UIView.CommitAnimations();
		}
	}
}