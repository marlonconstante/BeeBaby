using System;
using MonoTouch.UIKit;
using Domain.Moment;
using Application;
using System.Drawing;
using BeeBaby.ResourcesProviders;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public partial class MomentDetailViewController : NavigationViewController
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

			m_txtDescriptionDelegate = new PlaceholderTextViewDelegate();
			txtDescription.Delegate = m_txtDescriptionDelegate;
			mapView.Delegate = new ZoomMapViewDelegate(0.001d);
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			ViewDate.UpdateInfo();
		}

		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			Event selectedEvent = CurrentContext.Instance.SelectedEvent;
			if (selectedEvent != null)
			{
				CurrentContext.Instance.Moment.Event = selectedEvent;
				btnSelectEvent.SetTitle(selectedEvent.Description, UIControlState.Normal);
			}
		}

		/// <summary>
		/// Determines whether this instance is keyboard animation.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		public override bool IsKeyboardAnimation()
		{
			return true;
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			lblMomentAbout.Text = "MomentAbout".Translate();
			btnSelectEvent.SetTitle("SelectEvent".Translate(), UIControlState.Normal);
			lblLocation.Text = "WhichWas".Translate();
			btnSave.SetTitle("Save".Translate(), UIControlState.Normal);
			txtDescription.Text = "MomentRemember".Translate();
		}

		/// <summary>
		/// Starts the editing.
		/// </summary>
		public override void StartEditing()
		{
			base.StartEditing();

			ViewDate.Hide();
		}

		/// <summary>
		/// Ends the editing.
		/// </summary>
		public override void EndEditing()
		{
			base.EndEditing();

			ViewDate.Hide();
		}

		/// <summary>
		/// Selects the event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void SelectEvent(UIButton sender)
		{
			ShowProgressWhilePerforming(() => {
				PerformSegue("segueSelectEvent", sender);
			}, false);
		}

		/// <summary>
		/// Save the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Save(UIButton sender)
		{
			ShowProgressWhilePerforming(() => {
				var imageProvider = new ImageProvider(CurrentContext.Instance.Moment);
				var momentService = new MomentService();
				var moment = CurrentContext.Instance.Moment;

				moment.Description = m_txtDescriptionDelegate.Placeholder.GetInitialText(txtDescription.Text);
				moment.Event = CurrentContext.Instance.SelectedEvent;
				moment.Date = ViewDate.GetDateTime();

				if (!mapView.Hidden)
				{
					moment.Position = new GlobalPosition();
					moment.Position.Latitude = mapView.UserLocation.Coordinate.Latitude;
					moment.Position.Longitude = mapView.UserLocation.Coordinate.Longitude;
				}

				imageProvider.SavePermanentImages(moment.SelectedMediaNames);
				momentService.SaveMoment(moment);

				CurrentContext.Instance.Moment = null;
				CurrentContext.Instance.SelectedEvent = null;

				PerformSegue("segueTimeline", sender);
			}, false);
		}

		/// <summary>
		/// Controls the display of the map.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void LocationChanged(UISwitch sender)
		{
			if (m_mapViewHeight == -1)
			{
				m_mapViewHeight = mapView.Frame.Height;
			}
			mapViewConstraint.Constant += (sender.On) ? -m_mapViewHeight : m_mapViewHeight;
			mapView.Hidden = !sender.On;
		}

		/// <summary>
		/// Gets the view date.
		/// </summary>
		/// <value>The view date.</value>
		public ViewDatePicker ViewDate {
			get {
				return (ViewDatePicker) vwDate;
			}
		}
	}
}