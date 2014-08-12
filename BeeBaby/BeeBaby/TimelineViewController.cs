using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using Domain.Baby;
using Application;
using BeeBaby.ResourcesProviders;
using BeeBaby.Util;
using Domain.User;
using Skahal.Infrastructure.Framework.Globalization;
using System.Drawing;
using PixateFreestyleLib;
using System.Collections.Generic;

namespace BeeBaby
{
	public partial class TimelineViewController : NavigationViewController
	{
		static bool s_openCamera = true;
		const float c_buttonHeight = 44f;
		NSIndexPath m_currentIndexPath;
		Popover<TimelineViewController, EventArgs> m_popover;
		Popover<TimelineViewController, EventArgs> m_descriptionPopover;
		ModalViewController m_modalViewController;
		TimelineViewSource m_tableSource;

		public TimelineViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			FlurryAnalytics.Flurry.LogEvent("Momento: Timeline.", true);

			CurrentContext.Instance.ReloadMoments = true;

			LoadUser();
			LoadBaby();
		}

		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			LoadEvents();
			LoadModalViewController();

			if (s_openCamera)
			{
				OpenCamera();
				s_openCamera = false;
			}
			else
			{
				InitTimeline();
			}
		}

		/// <summary>
		/// Views the did disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidDisappear(bool animated)
		{
			FlurryAnalytics.Flurry.EndTimedEvent("Momento: Timeline.", null);
			base.ViewDidDisappear(animated);
		}

		/// <summary>
		/// Ends the editing.
		/// </summary>
		public override void EndEditing()
		{
			base.EndEditing();

			HidePopovers();
		}

		/// <summary>
		/// Hides the popover.
		/// </summary>
		public void HidePopovers()
		{
			if (m_popover != null)
			{
				m_popover.Hide();
			}

			if (m_descriptionPopover != null)
			{
				m_descriptionPopover.Hide();
			}
		}

		/// <summary>
		/// Loads the user.
		/// </summary>
		void LoadUser()
		{
			CurrentContext.Instance.User = new UserService().LoadUser();
		}

		/// <summary>
		/// Loads the baby.
		/// </summary>
		void LoadBaby()
		{
			CurrentContext.Instance.CurrentBaby = PreferencesEditor.LoadLastUsedBaby();
		}

		/// <summary>
		/// Loads the events.
		/// </summary>
		void LoadEvents()
		{
			if (CurrentContext.Instance.AllEvents.Count == 0)
			{
				CurrentContext.Instance.AllEvents = new EventService().GetAllEvents().ToList();
			}
		}

		/// <summary>
		/// Loads the modal view controller.
		/// </summary>
		void LoadModalViewController()
		{
			if (m_modalViewController == null)
			{
				var board = UIStoryboard.FromName("MainStoryboard", null);
				m_modalViewController = (ModalViewController) board.InstantiateViewController("ModalViewController");
			}
		}

		/// <summary>
		/// Inits the timeline.
		/// </summary>
		void InitTimeline()
		{
			if (CurrentContext.Instance.ReloadMoments)
			{
				new ImageProvider().DeleteFiles(true);

				var baby = CurrentContext.Instance.CurrentBaby;
				var moments = new MomentService().GetAllMoments(baby);

				lblBabyName.Text = baby.Name;
				lblBabyAge.Text = string.Concat("Have".Translate(), " ", baby.AgeInWords, " ", "old".Translate());

				InitPopovers();

				m_tableSource = new TimelineViewSource(this, moments.ToList(), baby);
				tblView.Source = m_tableSource;
				tblView.ReloadData();

				CurrentContext.Instance.ReloadMoments = false;
			}
		}

		/// <summary>
		/// Inits the popover.
		/// </summary>
		void InitPopovers()
		{
			if (m_popover == null)
			{
				var proxyAddPhotos = new EventProxy<TimelineViewController, EventArgs>(this);
				var proxyChangeMoments = new EventProxy<TimelineViewController, EventArgs>(this);
				var proxyRemoveRow = new EventProxy<TimelineViewController, EventArgs>(this);

				proxyAddPhotos.Action = (target, sender, args) => {
					CurrentContext.Instance.Moment = target.m_tableSource.MomentAt(target.m_currentIndexPath);
					target.AddPhotos((Button) sender);							
					target.HidePopovers();
				};

				proxyChangeMoments.Action = (target, sender, args) => {
					CurrentContext.Instance.Moment = target.m_tableSource.MomentAt(target.m_currentIndexPath);
					CurrentContext.Instance.SelectedEvent = CurrentContext.Instance.Moment.Event;
					target.ChangeMoment((Button) sender);							
					target.HidePopovers();
				};

				proxyRemoveRow.Action = (target, sender, args) => {
					CurrentContext.Instance.Moment = target.m_tableSource.MomentAt(target.m_currentIndexPath);
					target.RemoveCurrentRow();
					target.HidePopovers();
				};
					
				m_popover = new Popover<TimelineViewController, EventArgs>(new RectangleF(0f, 0f, 240f, 0));


				m_popover.AddPopoverItem("AddPhotos".Translate(), "photo", true, c_buttonHeight, proxyAddPhotos);
				m_popover.AddPopoverItem("ChangeMoment".Translate(), "pencil", true, c_buttonHeight, proxyChangeMoments);
				m_popover.AddPopoverItem("RemoveMoment".Translate(), "trash", false, c_buttonHeight, proxyRemoveRow);

				m_popover.MinY = tblView.Frame.Y;
				m_popover.AddSubviews(m_popover.MenuItems.ToArray());
			}

			if (m_descriptionPopover == null)
			{
				m_descriptionPopover = new Popover<TimelineViewController, EventArgs>(RectangleF.Empty);
				m_descriptionPopover.SetStyleClass("description-popover");
				m_descriptionPopover.MinY = tblView.Frame.Y;

				var label = new Label(new RectangleF(33f, 8f, 220f, 0f));
				label.IsAutoAdjustSize = true;
				m_descriptionPopover.AddSubview(label);

				var icon = new UIView(new RectangleF(8f, 8f, 19f, 19f));
				icon.SetStyleClass("comments");
				m_descriptionPopover.AddSubview(icon);
			}
		}

		/// <summary>
		/// Adds the photos.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void AddPhotos(Button sender)
		{
			ShowProgressWhilePerforming(() => RootViewController.PerformSegue("segueMedia", sender), false);
		}

		/// <summary>
		/// Changes the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void ChangeMoment(Button sender)
		{
			ShowProgressWhilePerforming(() => RootViewController.PerformSegue("segueMoment", sender), false);
		}

		/// <summary>
		/// Removes the current row.
		/// </summary>
		void RemoveCurrentRow()
		{
			var alertView = new UIAlertView("Delete".Translate(), "QuestionRemoveMoment".Translate(), null, null, "Yes".Translate(), "No".Translate());

			var proxy = new EventProxy<TimelineViewController, UIButtonEventArgs>(this);
			proxy.Action = (target, sender, args) => {
				if (args.ButtonIndex == 0)
				{
					target.m_tableSource.RemoveRow(target.tblView, target.m_currentIndexPath);
					if (target.tblView.NumberOfRowsInSection(0) == 0)
					{
						target.OpenCamera();
					}
				}
			};
			alertView.Clicked += proxy.HandleEvent;

			alertView.Show();
		}

		/// <summary>
		/// Opens the options.
		/// </summary>
		/// <param name="cell">Cell.</param>
		public void OpenOptions(TimelineMomentCell cell)
		{
			m_currentIndexPath = tblView.IndexPathForCell(cell);

			m_popover.Show(new PointF(UIScreen.MainScreen.Bounds.Width - m_popover.Frame.Width, CurrentCellRect.Y));
		}

		/// <summary>
		/// Shows the description.
		/// </summary>
		/// <param name="cell">Cell.</param>
		public void ShowDescription(TimelineMomentCell cell)
		{
			m_currentIndexPath = tblView.IndexPathForCell(cell);

			var moment = m_tableSource.MomentAt(m_currentIndexPath);

//			var label = m_descriptionPopover.Subviews[0] as Label;
//			label.Text = m_tableSource.MomentAt(m_currentIndexPath).Description;
//
//			var width = label.Frame.Width + label.Frame.X + label.Frame.Y;
//			var height = label.Frame.Height + (label.Frame.Y * 2f);
//
//			m_descriptionPopover.Hide(() => {
//				m_descriptionPopover.Frame = new RectangleF(0f, 0f, width, height);
//			});
//
//			m_descriptionPopover.Show(new PointF(UIScreen.MainScreen.Bounds.Width - width, CurrentCellRect.Y));

			PresentViewController(m_modalViewController, true, null);
			m_modalViewController.SetInformation(moment);
		}

		/// <summary>
		/// Gets the current cell rect.
		/// </summary>
		/// <value>The current cell rect.</value>
		RectangleF CurrentCellRect {
			get {
				var tableRect = tblView.RectForRowAtIndexPath(m_currentIndexPath);
				return tblView.ConvertRectToView(tableRect, View);
			}
		}
	}
}