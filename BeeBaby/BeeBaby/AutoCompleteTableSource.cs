using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Domain.Moment;

namespace BeeBaby
{
	public class AutoCompleteTableSource : TableViewSource
	{
		const string s_cellIdentifier = "suggestionCell";
		Location[] m_locations;
		MomentDetailViewController m_viewController;

		public AutoCompleteTableSource(Location[] locations, MomentDetailViewController controllerContext)
		{
			this.m_locations = locations;
			this.m_viewController = controllerContext;
		}

		/// <Docs>Table view displaying the rows.</Docs>
		/// <summary>
		/// Rowses the in section.
		/// </summary>
		/// <returns>The in section.</returns>
		/// <param name="tableview">Tableview.</param>
		/// <param name="section">Section.</param>
		public override int RowsInSection(UITableView tableview, int section)
		{
			return m_locations.Length;
		}

		/// <Docs>Table view containing the row.</Docs>
		/// <summary>
		/// Rows the selected.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			FlurryAnalytics.Flurry.LogEvent("Momento: GPS Selecionou da lista.");
			m_viewController.SelectLocation(m_locations[indexPath.Row]);
			tableView.DeselectRow(indexPath, true);
		}

		/// <Docs>Table view requesting the cell.</Docs>
		/// <summary>
		/// Gets the cell.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(s_cellIdentifier);
			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Default, s_cellIdentifier);
				cell.BackgroundColor = UIColor.Clear;
			}

			var location = m_locations[indexPath.Row];
			cell.TextLabel.Text = location.Name;

			return cell;
		}
	}
}