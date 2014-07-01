﻿using System;
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
		/// Move the specified view, x, y and animated.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public static void Move(UIView view, float x, float y, bool animated = true)
		{
			UIView.BeginAnimations(string.Empty, IntPtr.Zero);
			UIView.SetAnimationDuration(animated ? 0.3d : 0d);

			if (view is ViewScrollable)
			{
				((ViewScrollable) view).ScrollToTop();
			}

			var superview = view.Superview;
			if (superview is UIScrollView)
			{
				var scrollView = (UIScrollView) superview;
				var contentSize = scrollView.ContentSize;
				scrollView.ContentSize = new SizeF(contentSize.Width, contentSize.Height - y);
			}

			RectangleF frame = view.Frame;
			frame.X += x; 
			frame.Y += y; 
			view.Frame = frame;

			UIView.CommitAnimations();
		}
	}
}