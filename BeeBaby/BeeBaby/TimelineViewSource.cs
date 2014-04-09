using System;
using MonoTouch.UIKit;
using Domain.Moment;
using System.Collections.Generic;
using Application;
using MonoTouch.Foundation;
using Skahal.Infrastructure.Framework.Domain;

namespace BeeBaby
{
	public class TimelineViewSource : UITableViewSource
	{
		private List<IAggregateRoot> m_tableItems;
		private UIViewController m_viewController;

		public TimelineViewSource(UIViewController viewController, List<IAggregateRoot> items)
		{
			m_tableItems = items;
			m_viewController = viewController;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section
		/// </summary>
		public override int RowsInSection(UITableView tableview, int section)
		{
			return m_tableItems.Count;
		}

		/// <summary>
		/// Called by the TableView to determine the estimated height of the row
		/// </summary>
		public override float EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{

			return GetHeight(indexPath);
		}

		/// <summary>
		/// Called by the TableView to determine the row height
		/// </summary>
		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return GetHeight(indexPath);
		}

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row
		/// </summary>
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			string cellIdentifier = GetCellIdentifier(indexPath);

			// Request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
			return cell;
		}

		/// <summary>
		/// Get the cell identifier
		/// </summary>
		private string GetCellIdentifier(NSIndexPath indexPath)
		{
			switch (m_tableItems[indexPath.Row].GetType().Name)
			{
				case "Moment":
					return "MomentCell";
				case "Event":
					return "EventCell";
				default:
					return "";
			}
		}

		/// <summary>
		/// Get the row height
		/// </summary>
		private float GetHeight(NSIndexPath indexPath)
		{
			switch (m_tableItems[indexPath.Row].GetType().Name)
			{
				case "Moment":
					return 255f;
				case "Event":
					return 56f;
				default:
					return 0;
			}
		}
	}
}