using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Domain.Moment;
using Application;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public partial class EventListViewController : NavigationViewController
	{
		public bool ShowEverydayEvents { get; set; }

		public bool ShowFirstsEvents { get; set; }

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
			var allEvents = m_eventService.GetAllEvents();
			m_events = allEvents
				.Where(e => (ShowFirstsEvents && e.Kind == EventType.Achivment) ||
			(ShowEverydayEvents && e.Kind == EventType.Everyday))
				.ToList();
					
			EventListViewSource eventListViewSource = new EventListViewSource(this, m_events);
			schBar.Delegate = new EventTableSearchBarDelegate(eventListViewSource, allEvents);
			tblView.Source = eventListViewSource;
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			TitleScreen = "Event".Translate();
		}
	}
}