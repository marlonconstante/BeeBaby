using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Domain.Moment;

namespace BeeBaby
{
	public class EventTableSearchBarDelegate : UISearchBarDelegate
	{
		EventListViewSource m_eventListViewSource;
		IList<Event> m_events;

		public EventTableSearchBarDelegate(EventListViewSource eventListViewSource, IList<Event> events)
		{
			m_eventListViewSource = eventListViewSource;
			m_events = events;
		}

		/// <summary>
		/// Text changed event.
		/// </summary>
		/// <param name="searchBar">Search bar.</param>
		/// <param name="searchText">Search text.</param>
		public override void TextChanged(UISearchBar searchBar, string searchText)
		{
			var tableView = searchBar.Superview as UITableView;
			m_eventListViewSource.ReloadData(tableView, GetFilteredEvents(searchText));
		}

		/// <summary>
		/// Searchs the button clicked.
		/// </summary>
		/// <param name="searchBar">Search bar.</param>
		public override void SearchButtonClicked(UISearchBar searchBar)
		{
			searchBar.ResignFirstResponder();
		}

		/// <summary>
		/// Gets the filtered events.
		/// </summary>
		/// <returns>The filtered events.</returns>
		/// <param name="searchText">Search text.</param>
		IList<Event> GetFilteredEvents(String searchText)
		{
			List<Event> filteredEvents = new List<Event>();
			foreach (Event ev in m_events) {
				if (ev.Description.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) {
					filteredEvents.Add(ev);
				}
			}
			return filteredEvents;
		}
	}
}