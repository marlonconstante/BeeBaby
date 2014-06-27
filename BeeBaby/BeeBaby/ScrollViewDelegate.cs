using System;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public class ScrollViewDelegate : UIScrollViewDelegate
	{
		public ScrollViewDelegate()
		{
			Enable = true;
		}

		/// <Docs>Scroll view where the scrolling occurred.</Docs>
		/// <summary>
		/// Scrolled the specified scrollView.
		/// </summary>
		/// <param name="scrollView">Scroll view.</param>
		public override void Scrolled(UIScrollView scrollView)
		{
			if (Enable)
			{
				var topViewController = Windows.GetTopViewController(scrollView.Window);
				if (topViewController is BaseViewController)
				{
					((BaseViewController) topViewController).EndEditing();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BeeBaby.ScrollViewDelegate"/> is enable.
		/// </summary>
		/// <value><c>true</c> if enable; otherwise, <c>false</c>.</value>
		public bool Enable {
			get;
			set;
		}
	}
}