using System;
using UIKit;
using Foundation;
using BeeBaby.Util;

namespace BeeBaby.VisualElements
{
	public abstract class TableViewSource : UITableViewSource
	{
		public TableViewSource()
		{
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