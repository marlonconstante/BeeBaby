using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using Application;

namespace BeBabby
{
	public partial class MomentDetailViewController : UIViewController
	{
		public MomentDetailViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			if (CurrentContext.Instance.SelectedEvent != null)
				btnSelectEvent.SetTitle(CurrentContext.Instance.SelectedEvent.Description, UIControlState.Normal);

		}

		partial void SelectEvent(UIButton sender)
		{
			PerformSegue("segueSelectEvent", sender);
		}
	}
}
