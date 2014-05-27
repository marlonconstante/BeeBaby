﻿using System;
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
		const string s_cellIdentifier = "EventCell";
		UIViewController m_viewController;
		IList<Event> m_otherEventsTableItems;

		public EventListViewSource(UIViewController viewController, IList<Event> otherItems)
		{
			m_viewController = viewController;
			m_otherEventsTableItems = otherItems;
			IsSearching = false;
		}

		/// <Docs>Table view displaying the rows.</Docs>
		/// <summary>
		/// Rowses the in section.
		/// </summary>
		/// <returns>The in section.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="section">Section.</param>
		public override int RowsInSection(UITableView tableView, int section)
		{
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
			CurrentContext.Instance.SelectedEvent = m_otherEventsTableItems[indexPath.Row];
			m_viewController.NavigationController.PopViewControllerAnimated(true);
		}

		/// <Docs>Table view requesting the cell.</Docs>
		/// <summary>
		/// Gets the cell.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(new NSString(s_cellIdentifier), indexPath);

			cell.TextLabel.Text = m_otherEventsTableItems[indexPath.Row].Description;

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
			return 1;
		}

			/// <summary>
		/// Reloads the data.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="items">Items.</param>
		public void ReloadData(UITableView tableView, IList<Event> items)
		{
			m_otherEventsTableItems = items;
			tableView.ReloadData();
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is searching.
		/// </summary>
		/// <value><c>true</c> if this instance is searching; otherwise, <c>false</c>.</value>
		public bool IsSearching
		{
			get;
			set;
		}
	}
}