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
			mapView.ShowsUserLocation = true;

			if (CurrentContext.Instance.SelectedEvent != null)
			{
				CurrentContext.Instance.Moment.Event = CurrentContext.Instance.SelectedEvent;
				btnSelectEvent.SetTitle(CurrentContext.Instance.SelectedEvent.Description, UIControlState.Normal);
			}
		}

		/// <summary>
		/// Selects the event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void SelectEvent(UIButton sender)
		{
			PerformSegue("segueSelectEvent", sender);
		}

		/// <summary>
		/// Save the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Save(UIButton sender)
		{
			var momentService = new MomentService();
			momentService.SaveMoment(CurrentContext.Instance.Moment);
		}
	}
}
