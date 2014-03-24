using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;

namespace BeBabby
{
	public partial class MomentDetailViewController : UIViewController
	{
		public Event SelectedEvent
		{
			get;
			set;
		}
		public MomentDetailViewController (IntPtr handle) : base (handle)
		{
		}

		partial void btnSelectEvent(UIButton sender)
		{
			PerformSegue("segueSelectEvent", sender);
		}
	}
}
