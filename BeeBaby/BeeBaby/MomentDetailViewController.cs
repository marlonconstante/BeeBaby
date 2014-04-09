using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;
using Domain.Moment;
using Application;
using System.Drawing;
using BigTed;

namespace BeeBaby
{
	public partial class MomentDetailViewController : UIViewController
	{
		private float m_mapViewHeight = -1;

		public MomentDetailViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);

			pckDate.Hidden = true;
		}

		public async override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			new KeyboardNotification(View);
			mapView.Delegate = new ZoomMapViewDelegate(0.001d);
			txtDescription.Delegate = new PlaceholderTextViewDelegate();

			Event selectedEvent = CurrentContext.Instance.SelectedEvent;
			if (selectedEvent != null)
			{
				CurrentContext.Instance.Moment.Event = selectedEvent;
				btnSelectEvent.SetTitle(selectedEvent.Description, UIControlState.Normal);
			}

			BTProgressHUD.Dismiss();
		}

		/// <summary>
		/// Selects the event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void SelectEvent(UIButton sender)
		{
			BTProgressHUD.Show(); //shows the spinner

			PerformSegue("segueSelectEvent", sender);
		}

		/// <summary>
		/// Selects the date.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void SelectDate(UIButton sender)
		{
			pckDate.Hidden = !pckDate.Hidden;

			float height = pckDate.Frame.Height - 20f;
			RectangleF frame = viewDate.Frame;
			frame.Height += (pckDate.Hidden) ? -height : height;

			viewDate.Frame = frame;
		}

		/// <summary>
		/// Save the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Save(UIButton sender)
		{
			var momentService = new MomentService();
			var moment = CurrentContext.Instance.Moment;

			moment.Description = txtDescription.Text;
			moment.Event = CurrentContext.Instance.SelectedEvent;
			moment.Date = pckDate.Date;

			if (!mapView.Hidden)
			{
				moment.Position = new GlobalPosition();
				moment.Position.Latitude = mapView.UserLocation.Coordinate.Latitude;
				moment.Position.Longitude = mapView.UserLocation.Coordinate.Longitude;
			}

			CurrentContext.Instance.Moment = moment;
			momentService.SaveMoment(moment);

			BTProgressHUD.Show(); //shows the spinner

			PerformSegue("segueSave", sender);
		}

		/// <summary>
		/// Controls the display of the map.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void LocationChanged(UISwitch sender)
		{
			InvokeInBackground(() =>
			{
				if (m_mapViewHeight == -1)
				{
					m_mapViewHeight = mapView.Frame.Height;
				}
				mapViewConstraint.Constant += (sender.On) ? -m_mapViewHeight : m_mapViewHeight;
				mapView.Hidden = !sender.On;
			});
		}
	}
}
