using System;
using MonoTouch.UIKit;
using Domain.Moment;
using Application;
using System.Drawing;
using BeeBaby.ResourcesProviders;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public partial class MomentDetailViewController : ViewController
	{
		float m_mapViewHeight;
		PlaceholderTextViewDelegate m_txtDescriptionDelegate;

		public MomentDetailViewController(IntPtr handle) : base(handle)
		{
			m_mapViewHeight = -1;
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			TranslateLabels();

			new KeyboardNotification(View);

			m_txtDescriptionDelegate = new PlaceholderTextViewDelegate();
			txtDescription.Delegate = m_txtDescriptionDelegate;
			mapView.Delegate = new ZoomMapViewDelegate(0.001d);
		}

		/// <summary>
		/// Views the did disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);

			pckDate.Hidden = true;
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			UpdateDateTimeInfo();
		}

		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			Event selectedEvent = CurrentContext.Instance.SelectedEvent;
			if (selectedEvent != null) {
				CurrentContext.Instance.Moment.Event = selectedEvent;
				btnSelectEvent.SetTitle(selectedEvent.Description, UIControlState.Normal);
			}
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		void TranslateLabels()
		{
			lblMomentAbout.Text = "MomentAbout".Translate();
			btnSelectEvent.SetTitle("SelectEvent".Translate(), UIControlState.Normal);
			lblLocation.Text = "WhichWas".Translate();
			btnSave.SetTitle("Save".Translate(), UIControlState.Normal);
			txtDescription.Text = "MomentRemember".Translate();
		}

		/// <summary>
		/// Updates the date time info.
		/// </summary>
		void UpdateDateTimeInfo()
		{
			var date = (DateTime) pckDate.Date;
			btnDate.SetTitle(date.ToLocalTime().ToString("d", System.Globalization.DateTimeFormatInfo.CurrentInfo), UIControlState.Normal);
			lblTime.Text = date.ToLocalTime().ToString("HH:mm", System.Globalization.DateTimeFormatInfo.CurrentInfo);
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
		/// Selects the date.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void SelectDate(UIButton sender)
		{
			pckDate.Hidden = !pckDate.Hidden;

			float height = pckDate.Frame.Height - 20f;
			RectangleF frame = vwDate.Frame;
			frame.Height += (pckDate.Hidden) ? -height : height;

			pckDate.MultipleTouchEnabled = true;

			pckDate.ValueChanged += (s, args) => {
				CurrentContext.Instance.Moment.Date = pckDate.Date;
				UpdateDateTimeInfo();
			};

			vwDate.Frame = frame;
		}

		/// <summary>
		/// Save the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Save(UIButton sender)
		{
			var imageProvider = new ImageProvider(CurrentContext.Instance.Moment);
			var momentService = new MomentService();
			var moment = CurrentContext.Instance.Moment;

			moment.Description = m_txtDescriptionDelegate.Placeholder.GetInitialText(txtDescription.Text);
			moment.Event = CurrentContext.Instance.SelectedEvent;
			moment.Date = pckDate.Date;

			if (!mapView.Hidden) {
				moment.Position = new GlobalPosition();
				moment.Position.Latitude = mapView.UserLocation.Coordinate.Latitude;
				moment.Position.Longitude = mapView.UserLocation.Coordinate.Longitude;
			}

			CurrentContext.Instance.Moment = moment;

			imageProvider.SavePermanentImages(moment.SelectedMediaNames);
			momentService.SaveMoment(moment);

			PerformSegue("segueSave", sender);
		}

		/// <summary>
		/// Controls the display of the map.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void LocationChanged(UISwitch sender)
		{
			if (m_mapViewHeight == -1) {
				m_mapViewHeight = mapView.Frame.Height;
			}
			mapViewConstraint.Constant += (sender.On) ? -m_mapViewHeight : m_mapViewHeight;
			mapView.Hidden = !sender.On;
		}
	}
}
