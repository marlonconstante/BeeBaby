using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using Domain.Moment;
using Application;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using BeeBaby.Progress;
using BeeBaby.VisualElements;

namespace BeeBaby.Controllers
{
	public class EventListViewSource : TableViewSource
	{
		const string s_cellIdentifier = "EventCell";
		EventListViewController m_viewController;
		IList<Event> m_otherEventsTableItems;
		IDictionary<string, UIImage> m_tagIcons = new Dictionary<string, UIImage>();
		float m_scrollY;

		public EventListViewSource(EventListViewController viewController, IList<Event> otherItems)
		{
			m_viewController = viewController;
			m_otherEventsTableItems = otherItems;
			m_scrollY = 0f;
			LoadTagIcons();
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

		/// <summary>
		/// Gets the height for row.
		/// </summary>
		/// <returns>The height for row.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 44f;
		}

		/// <summary>
		/// Estimateds the height.
		/// </summary>
		/// <returns>The height.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override float EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 44f;
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

			var actionProgress = new ActionProgress(() => {
				if (m_viewController.IsEditFlow())
				{
					m_viewController.GoBackToMoment();
				}
				else
				{
					m_viewController.PerformSegue("segueMoment", this);
				}
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
			return tableView.DequeueReusableCell(s_cellIdentifier);
		}

		/// <Docs>Table view containing the row.</Docs>
		/// <param name="indexPath">Location of the row.</param>
		/// <paramref name="indexPath"></paramref>
		/// <remarks>Use this method to override properties of the cell before it is rendered (such as selection status or background
		/// color). After this method has been called, the table view will only modify the Alpha and Frame properties as it
		/// animates them (if required).</remarks>
		/// <summary>
		/// Wills the display.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="cell">Cell.</param>
		public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			var eventCell = cell as EventViewCell;
			var eventValue = m_otherEventsTableItems[indexPath.Row];
			eventCell.EventDescription = eventValue.Description;

			UIImage image;
			if (m_tagIcons.TryGetValue(eventValue.TagName, out image))
			{
				eventCell.TagIcon = image;
			}
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

			bool adjustLimit = scrollHeight < m_viewController.ViewTagsHeight && (scrollY < -64f || scrollY > scrollHeight - 64f);
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
			m_viewController.UpdateViewTagsHeight();
		}

		/// <summary>
		/// Loads the tag icons.
		/// </summary>
		void LoadTagIcons()
		{
			var names = Enum.GetNames(typeof(TagType)).Select((tag) => tag.ToLower()).ToList();
			names.Add("firsts");

			foreach (var name in names)
			{
				var image = UIImage.FromFile(string.Format("{0}_sm.png", name));
				m_tagIcons.Add(name, image);
			}
		}
	}
}