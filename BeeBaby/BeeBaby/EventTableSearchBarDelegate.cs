using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Domain.Moment;

namespace BeeBaby
{
	public class EventTableSearchBarDelegate : UISearchBarDelegate
	{
		EventListViewSource m_eventListViewSource;
		IEnumerable<Event> m_events;

		public EventTableSearchBarDelegate(EventListViewSource eventListViewSource, IEnumerable<Event> events, IEnumerable<Event> otherEvents)
		{
			m_eventListViewSource = eventListViewSource;
			m_events = events.Union(otherEvents);
		}

		/// <summary>
		/// Text changed event.
		/// </summary>
		/// <param name="searchBar">Search bar.</param>
		/// <param name="searchText">Search text.</param>
		public override void TextChanged(UISearchBar searchBar, string searchText)
		{
			m_eventListViewSource.IsSearching = true;
			var tableView = searchBar.Superview as UITableView;
			m_eventListViewSource.ReloadData(tableView, GetFilteredEvents(searchText));
		}

		/// <summary>
		/// Searchs the button clicked.
		/// </summary>
		/// <param name="searchBar">Search bar.</param>
		public override void SearchButtonClicked(UISearchBar searchBar)
		{
			m_eventListViewSource.IsSearching = false;
			searchBar.ResignFirstResponder();
		}

		/// <summary>
		/// Gets the filtered events.
		/// </summary>
		/// <returns>The filtered events.</returns>
		/// <param name="searchText">Search text.</param>
		IList<Event> GetFilteredEvents(String searchText)
		{
			return m_events.Where(e => e.Description.ToLower().Contains(searchText.ToLower())).ToList();
		}
	}
}