using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using Domain.Moment;
using Application;

namespace BeeBaby
{
	public class EventListViewSource : UITableViewSource
	{
		private static string s_cellIdentifier = "EventCell";
		private List<Event> m_tableItems;
		private UIViewController m_viewController;

		public EventListViewSource(UIViewController viewController, List<Event> items)
		{
			m_tableItems = items;
			m_viewController = viewController;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override int RowsInSection(UITableView tableview, int section)
		{
			return m_tableItems.Count;
		}

		/// <summary>
		/// Called when a row is touched.
		/// </summary>
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			CurrentContext.Instance.SelectedEvent = m_tableItems[indexPath.Row];
			m_viewController.NavigationController.PopViewControllerAnimated(true);
		}

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row.
		/// </summary>
		public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			// Request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell(s_cellIdentifier);
			cell.TextLabel.Text = m_tableItems[indexPath.Row].Description;
			return cell;
		}
	}
}

