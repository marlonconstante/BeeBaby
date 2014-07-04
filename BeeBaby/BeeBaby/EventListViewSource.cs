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
	public class EventListViewSource : TableViewSource
	{
		const string s_cellIdentifier = "EventCell";
		EventListViewController m_viewController;
		IList<Event> m_otherEventsTableItems;
		float m_scrollY;

		public EventListViewSource(EventListViewController viewController, IList<Event> otherItems)
		{
			m_viewController = viewController;
			m_otherEventsTableItems = otherItems;
			m_scrollY = 0f;
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
			ActionProgress actionProgress = new ActionProgress(() => {
				m_viewController.PerformSegue("segueMoment", this);
			}, false);
			actionProgress.Execute();
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
			var moment = m_otherEventsTableItems[indexPath.Row];
			EventViewCell cell = (EventViewCell) tableView.DequeueReusableCell(new NSString(s_cellIdentifier), indexPath);
			cell.EventDescription = moment.Description;


			var tagName = Enum.GetName(typeof(TagType), m_otherEventsTableItems[indexPath.Row].Tag).ToLower();
			var imageName = (moment.Kind == EventType.Achievement) ? "firsts_sm.png" : string.Format("{0}_sm.png", tagName);
			cell.TagIcon = UIImage.FromFile(imageName);

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

		/// <Docs>Scroll view where the scrolling occurred.</Docs>
		/// <summary>
		/// Scrolled the specified scrollView.
		/// </summary>
		/// <param name="scrollView">Scroll view.</param>
		public override void Scrolled(UIScrollView scrollView)
		{
			var scrollHeight = scrollView.ContentSize.Height - scrollView.Bounds.Height;
			var scrollY = scrollView.ContentOffset.Y;

			var y = m_scrollY - scrollY;
			var up = y > 0f;

			bool adjustLimit = scrollHeight < 261f && (scrollY < -64f || scrollY > scrollHeight - 64f);
			if ((up && scrollHeight > scrollY) || (!up && scrollY >= (adjustLimit ? -20f : 0f)))
			{
				m_viewController.MoveScroll(y, adjustLimit);
			}

			m_scrollY = scrollY;
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
	}
}