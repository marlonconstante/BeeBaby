using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using Domain.Moment;
using Application;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public class EventListViewSource : UITableViewSource
	{
		static string s_cellIdentifier = "EventCell";
		UIViewController m_viewController;
		IList<Event> m_suggestedTableItems;
		IList<Event> m_otherEventsTableItems;

		public EventListViewSource(UIViewController viewController, IList<Event> suggetedItems, IList<Event> otherItems)
		{
			m_viewController = viewController;
			m_suggestedTableItems = suggetedItems;
			m_otherEventsTableItems = otherItems;
			IsSearching = false;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override int RowsInSection(UITableView tableview, int section)
		{
			if (section == 0)
			{
				return m_suggestedTableItems.Count;
			}
			return m_otherEventsTableItems.Count;
		}

		/// <Docs>Table view containing the row.</Docs>
		/// <summary>
		/// Rows the selected.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			CurrentContext.Instance.SelectedEvent = m_suggestedTableItems[indexPath.Row];
			m_viewController.NavigationController.PopViewControllerAnimated(true);
		}

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row.
		/// </summary>
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(new NSString(s_cellIdentifier), indexPath);
			if (indexPath.Section == 0)
			{
				cell.TextLabel.Text = m_suggestedTableItems[indexPath.Row].Description;
			}
			else
			{
				cell.TextLabel.Text = m_otherEventsTableItems[indexPath.Row].Description;
			}
			return cell;
		}

		/// <Docs>Table view displaying the sections.</Docs>
		/// <returns>Number of sections required to display the data. The default is 1 (a table must have at least one section).</returns>
		/// <para>Declared in [UITableViewDataSource]</para>
		/// <summary>
		/// Numbers the of sections.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		public override int NumberOfSections(UITableView tableView)
		{
			if (IsSearching)
			{
				return 1;
			}
			return 2;
		}

		/// <Docs>Table view containing the section.</Docs>
		/// <summary>
		/// Called to populate the header for the specified section.
		/// </summary>
		/// <see langword="null"></see>
		/// <returns>The for header.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="section">Section.</param>
		public override string TitleForHeader(UITableView tableView, int section)
		{
			if (section == 0)
			{
				return "Suggestions".Translate();
			}
			return "All Other Events".Translate();
		}

		/// <summary>
		/// Reloads the data.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="items">Items.</param>
		public void ReloadData(UITableView tableView, IList<Event> items)
		{
			m_suggestedTableItems = items;
			tableView.ReloadData();
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is searching.
		/// </summary>
		/// <value><c>true</c> if this instance is searching; otherwise, <c>false</c>.</value>
		public bool IsSearching {
			get;
			set;
		}
	}
}