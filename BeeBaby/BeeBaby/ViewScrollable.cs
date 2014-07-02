using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public partial class ViewScrollable : UIView
	{
		public ViewScrollable(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Scrolls to top.
		/// </summary>
		public void ScrollToTop()
		{
			ScrollView.ScrollRectToVisible(new RectangleF(0f, 0f, 1f, 1f), false);
		}

		/// <summary>
		/// Gets the scroll view.
		/// </summary>
		/// <value>The scroll view.</value>
		public UIScrollView ScrollView {
			get {
				return (UIScrollView) Subviews[0];
			}
		}
	}
}