using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Domain;

namespace BeeBaby
{
	public partial class TimelineViewController : UIViewController
	{
		public TimelineViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			initTimeline();
		}

		private void initTimeline()
		{
			var m_momentService = new MomentService();
			var m_events = m_momentService.GetAllMoments();

			List<IAggregateRoot> items = new List<IAggregateRoot>();
			items.AddRange(m_events.ToList());
			items.Add(new Event());

			tblView.Source = new TimelineViewSource(this, items);
		}
	}
}