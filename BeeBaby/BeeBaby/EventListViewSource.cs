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
		List<Event> m_tableItems;
		UIViewController m_viewController;

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


		/// <Docs>Table view containing the row.</Docs>
		/// <summary>
		/// Rows the selected.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine("Ok chegou");
			CurrentContext.Instance.SelectedEvent = m_tableItems[indexPath.Row];
			m_viewController.NavigationController.PopViewControllerAnimated(true);

		}

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row.
		/// </summary>
		public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
//			// Request a recycled cell to save memory
//			UITableViewCell cell = tableView.DequeueReusableCell(new NSString(s_cellIdentifier), indexPath);
//			cell.TextLabel.Text = m_tableItems[indexPath.Row].Description;
//			return cell;
//
			UITableViewCell cell = tableView.DequeueReusableCell (s_cellIdentifier);

			// if there are no cells create a new one
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, s_cellIdentifier);      

				// why do you set the interaction to false? Setting it to false disable interaction for your cell
				cell.UserInteractionEnabled = true;

				// create label and view here. You customize the cell with additional elements once
			}

			cell.TextLabel.Text = m_tableItems[indexPath.Row].Description;


			// update image and label here (you need to grab a reference for your label for example through the tag)

			return cell;

		}
	}
}

