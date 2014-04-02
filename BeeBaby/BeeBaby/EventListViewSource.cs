﻿using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using Domain.Moment;
using Application;

namespace BeeBaby
{
	public class EventListViewSource : UITableViewSource
	{
		List<Event> tableItems;
		string m_cellIdentifier = "EventCell";
		UIViewController m_viewController;

		public EventListViewSource (UIViewController viewController, List<Event> items)
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
		/// Called when a row is touched
		/// </summary>
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			CurrentContext.Instance.SelectedEvent = tableItems[indexPath.Row];
			m_viewController.NavigationController.PopViewControllerAnimated(true);
		}

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row
		/// </summary>
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell (m_cellIdentifier);

			var cellStyle = UITableViewCellStyle.Default;
			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new UITableViewCell (cellStyle, m_cellIdentifier);
			}

			cell.TextLabel.Text = tableItems[indexPath.Row].Description;

			return cell;
		}
	}
}

