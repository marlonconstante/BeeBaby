using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public partial class ViewScrollable : UIView
	{
		ScrollViewDelegate m_scrollViewDelegate;

		public ViewScrollable(IntPtr handle) : base(handle)
		{
			m_scrollViewDelegate = new ScrollViewDelegate();
			ScrollView.Delegate = m_scrollViewDelegate;
		}

		/// <summary>
		/// Scrolls to top.
		/// </summary>
		public void ScrollToTop()
		{
			m_scrollViewDelegate.Enable = false;
			ScrollView.ScrollRectToVisible(new RectangleF(0f, 0f, 1f, 1f), false);
			m_scrollViewDelegate.Enable = true;
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

		/// <summary>
		/// Dispose the specified disposing.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Discard.ReleaseFields(this);
			}

			base.Dispose(disposing);
		}
	}
}