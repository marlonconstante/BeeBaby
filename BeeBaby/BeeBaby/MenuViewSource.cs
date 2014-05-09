using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using MonoTouch.Dialog;

namespace BeeBaby
{
	public class MenuViewSource : UITableViewSource
	{
		const string s_cellIdentifier = "MenuCell";
		UIViewController m_viewController;
		IList<MenuItem> m_tableItems;

		public MenuViewSource(UIViewController viewController, IList<MenuItem> menuItems)
		{
			m_viewController = viewController;
			m_tableItems = menuItems;
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
			MenuItem menuItem = m_tableItems[indexPath.Row] as MenuItem;
			menuItem.Action.Invoke();
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
			MenuItem menuItem = m_tableItems[indexPath.Row] as MenuItem;

			MenuCell cell = (MenuCell) tableView.DequeueReusableCell(new NSString(s_cellIdentifier), indexPath);
			cell.IconStyleClass = menuItem.IconStyleClass;
			cell.Title = menuItem.Title;

			return cell;
		}
	}
}