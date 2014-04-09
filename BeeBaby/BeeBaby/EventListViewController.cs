using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Domain.Moment;
using BigTed;

namespace BeeBaby
{
	public partial class EventListViewController : UITableViewController
	{
		IEnumerable<Event> m_events;
		EventService m_eventService;

		public EventListViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			m_eventService = new EventService();
			m_events = m_eventService.GetAllEvents();
			TableView.Source = new EventListViewSource(this, m_events.ToList());

			BTProgressHUD.Dismiss(); //shows the spinner
		}
	}
}
