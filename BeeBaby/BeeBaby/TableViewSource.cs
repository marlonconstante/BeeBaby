using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace BeeBaby
{
	public abstract class TableViewSource : UITableViewSource
	{
		PointF m_lastContentOffset;

		public TableViewSource()
		{
			m_lastContentOffset = PointF.Empty;
		}

		/// <Docs>Scroll view where the scrolling occurred.</Docs>
		/// <summary>
		/// Scrolled the specified scrollView.
		/// </summary>
		/// <param name="scrollView">Scroll view.</param>
		public override void Scrolled(UIScrollView scrollView)
		{
			var y = scrollView.ContentOffset.Y;
			if (m_lastContentOffset.Y >= y)
			{
				ScrollVerticalDirection = UIAccessibilityScrollDirection.Up;
			}
			else
			{
				ScrollVerticalDirection = UIAccessibilityScrollDirection.Down;
			}
			m_lastContentOffset = scrollView.ContentOffset;
		}

		/// <summary>
		/// Determines whether this instance is visible row the specified tableView indexPath.
		/// </summary>
		/// <returns><c>true</c> if this instance is visible row the specified tableView indexPath; otherwise, <c>false</c>.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public bool IsVisibleRow(UITableView tableView, NSIndexPath indexPath)
		{
			foreach (var visibleIndexPath in tableView.IndexPathsForVisibleRows)
			{
				if (visibleIndexPath.Row == indexPath.Row)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Gets or sets the scroll vertical direction.
		/// </summary>
		/// <value>The scroll vertical direction.</value>
		public UIAccessibilityScrollDirection ScrollVerticalDirection {
			get;
			set;
		}

		/// <summary>
		/// Dispose the specified disposing.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Discard.ReleaseProperties(this);
				Discard.ReleaseFields(this);
			}

			base.Dispose(disposing);
		}
	}
}