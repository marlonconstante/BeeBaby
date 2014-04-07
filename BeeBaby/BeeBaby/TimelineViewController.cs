using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;

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

			var m_momentService = new MomentService();
			var m_events = m_momentService.GetAllMoments();

			tblView.Source = new TimelineViewSource(this, m_events.ToList());
		}
	}
}