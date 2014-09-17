using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Domain.Moment;
using MonoTouch.Foundation;
using BeeBaby.Util;

namespace BeeBaby
{
	public class EventTableSearchBarDelegate : UISearchBarDelegate
	{
		EventListViewController m_eventViewController;
		EventListViewSource m_eventListViewSource;
		IEnumerable<Event> m_events;

		public EventTableSearchBarDelegate(EventListViewController eventViewController, EventListViewSource eventListViewSource, IEnumerable<Event> events)
		{
			m_eventViewController = eventViewController;
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
			m_eventViewController.DeselectAllTags();

			var events = (searchText != string.Empty) ? 
				GetFilteredEvents(searchText) :
				m_eventViewController.AllEvents;

			m_eventViewController.ClearSelectedTag();

			m_eventListViewSource.ReloadData(tableView, events);
		}

		/// <summary>
		/// Searchs the button clicked.
		/// </summary>
		/// <param name="searchBar">Search bar.</param>
		public override void SearchButtonClicked(UISearchBar searchBar)
		{
			searchBar.ResignFirstResponder();
		}
			
		/// <Docs>To be added.</Docs>
		/// <remarks>To be added.</remarks>
		/// <summary>
		/// Determines whether this instance cancel button clicked the specified searchBar.
		/// </summary>
		/// <returns><c>true</c> if this instance cancel button clicked the specified searchBar; otherwise, <c>false</c>.</returns>
		/// <param name="searchBar">Search bar.</param>
		public override void CancelButtonClicked(UISearchBar searchBar)
		{
			searchBar.Text = string.Empty;
			TextChanged(searchBar, searchBar.Text);
		}

		/// <Docs>To be added.</Docs>
		/// <remarks>To be added.</remarks>
		/// <summary>
		/// Raises the editing started event.
		/// </summary>
		/// <param name="searchBar">Search bar.</param>
		public override void OnEditingStarted(UISearchBar searchBar)
		{
			m_eventViewController.HideViewTags();
		}

		/// <Docs>To be added.</Docs>
		/// <remarks>To be added.</remarks>
		/// <summary>
		/// Raises the editing stopped event.
		/// </summary>
		/// <param name="searchBar">Search bar.</param>
		public override void OnEditingStopped(UISearchBar searchBar)
		{
			m_eventViewController.ShowViewTags();
		}
			
		/// <summary>
		/// Gets the filtered events.
		/// </summary>
		/// <returns>The filtered events.</returns>
		/// <param name="searchText">Search text.</param>
		IList<Event> GetFilteredEvents(String searchText)
		{
			var param = new NSDictionary("FiltredText", searchText);
			FlurryAnalytics.Flurry.LogEvent("Filtrou pela busca.", param);

			return m_events.Where(e => e.Description.ToLower().Contains(searchText.ToLower())).ToList();
		}

		/// <summary>
		/// Dispose the specified disposing.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Discard.ReleaseProperties(this);
				Discard.ReleaseFields(this);
			}

			base.Dispose(disposing);
		}
	}
}