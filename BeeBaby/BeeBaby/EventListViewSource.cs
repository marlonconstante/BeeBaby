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
		static string s_cellIdentifier = "EventCell";
		UIViewController m_viewController;
		List<Event> m_tableItems;

		public EventListViewSource(UIViewController viewController, List<Event> items)
		{
			m_viewController = viewController;
			m_tableItems = items;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override int RowsInSection(UITableView tableview, int section)
		{
			return m_tableItems.Count;
		}

		/// <Docs>Table view containing the row.</Docs>
		/// <summary>
		/// Rows the selected.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			CurrentContext.Instance.SelectedEvent = m_tableItems[indexPath.Row];
			m_viewController.NavigationController.PopViewControllerAnimated(true);
		}

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row.
		/// </summary>
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(new NSString(s_cellIdentifier), indexPath);
			cell.TextLabel.Text = m_tableItems[indexPath.Row].Description;
			return cell;
		}
	}
}