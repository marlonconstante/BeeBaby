using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Domain.Moment;
using Application;

namespace BeeBaby
{
	public partial class EventListViewController : NavigationViewController
	{
		IList<Event> m_suggestedEvents;
		IList<Event> m_otherEvents;
		EventService m_eventService;

		public EventListViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			m_eventService = new EventService();
			m_suggestedEvents = m_eventService.GetSuggestedEvents(CurrentContext.Instance.CurrentBaby).ToList();
			m_otherEvents = m_eventService.GetAllEvents().Except(m_suggestedEvents).ToList();

			EventListViewSource eventListViewSource = new EventListViewSource(this, m_suggestedEvents, m_otherEvents);
			schBar.Delegate = new EventTableSearchBarDelegate(eventListViewSource, m_suggestedEvents, m_otherEvents);
			tblView.SeparatorInset = UIEdgeInsets.Zero;
			tblView.Source = eventListViewSource;
		}
	}
}