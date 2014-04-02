using System;
using MonoTouch.UIKit;
using Domain.Moment;
using System.Collections.Generic;
using Application;
using MonoTouch.Foundation;

namespace BeeBaby
{
	public class TimelineViewSource : UITableViewSource
	{
		List<Moment> tableItems;
		string m_cellIdentifier = "MomentCell";
		UIViewController m_viewController;

		public TimelineViewSource (UIViewController viewController, List<Moment> items)
		{
			tableItems = items;
			m_viewController = viewController;
		}


		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override int RowsInSection (UITableView tableview, int section)
		{
			return tableItems.Count;
		}


		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row
		/// </summary>
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell (m_cellIdentifier);

			var cellStyle = UITableViewCellStyle.Default;
			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new UITableViewCell (cellStyle, m_cellIdentifier);
			}

			cell.TextLabel.Text = tableItems[indexPath.Row].Id;

			return cell;
		}
	}
}

