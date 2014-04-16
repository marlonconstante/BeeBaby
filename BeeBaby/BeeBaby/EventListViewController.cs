using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Domain.Moment;
using BigTed;

namespace BeeBaby
{
	public partial class EventListViewController : UIViewController
	{
		IEnumerable<Event> m_events;
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
			m_events = m_eventService.GetAllEvents();
			tblView.Source = new EventListViewSource(this, m_events.ToList());

			BTProgressHUD.Dismiss();
		}
	}
}