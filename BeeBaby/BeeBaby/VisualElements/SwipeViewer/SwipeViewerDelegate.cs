using System;
using CoreGraphics;
using SwipeViewer;
using BeeBaby.Util;
using BeeBaby.Controllers;
using System.Drawing;
using UIKit;
using System.CodeDom.Compiler;

namespace BeeBaby.VisualElements
{
	public class SwipeViewerDelegate : SwipeViewDelegate
	{
		public SwipeViewerDelegate()
		{
		}

		/// <summary>
		/// Swipes the size of the view item.
		/// </summary>
		/// <returns>The view item size.</returns>
		/// <param name="swipeView">Swipe view.</param>
		public override SizeF SwipeViewItemSize (SwipeView swipeView)
		{
			return swipeView.Bounds.Size;
		}

		/// <summary>
		/// Dids the index of the select item at.
		/// </summary>
		/// <param name="swipeView">Swipe view.</param>
		/// <param name="index">Index.</param>
		public override void DidSelectItemAtIndex(SwipeView swipeView, int index)
		{
			var viewController = Windows.GetTopViewController(swipeView.Window);
			if (viewController is FullscreenViewController)
			{
				((FullscreenViewController) viewController).ShowOrHideSubviews();
			}
		}
	}
}