using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Domain.Moment;

namespace BeeBaby
{
	public partial class EventListViewController : ViewController
	{
		IList<Event> m_events;
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
			m_events = m_eventService.GetAllEvents().ToList();

			EventListViewSource eventListViewSource = new EventListViewSource(this, m_events);
			schBar.Delegate = new EventTableSearchBarDelegate(eventListViewSource, m_events);
			tblView.SeparatorInset = UIEdgeInsets.Zero;
			tblView.Source = eventListViewSource;
		}
	}
}