using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace BeeBaby
{
	public class AutoCompleteTableSource : TableViewSource
	{
		const string s_cellIdentifier = "suggestionCell";
		string[] m_suggestions;
		MomentDetailViewController m_viewController;

		public AutoCompleteTableSource(string[] suggestions, MomentDetailViewController controllerContext)
		{
			this.m_suggestions = suggestions;
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
			return m_suggestions.Length;
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
			m_viewController.SetAutoCompleteText(m_suggestions[indexPath.Row]);
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
			}
			cell.TextLabel.Text = m_suggestions[indexPath.Row];
			return cell;
		}
	}
}