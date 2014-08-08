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
		NSIndexPath m_currentIndexPath;
		Popover m_popover;
		Popover m_descriptionPopover;

		IList<Button> m_popoverItems;
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
				lblBabyAge.Text = string.Concat("Have".Translate(), " ", baby.AgeInWords);

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
				m_popoverItems = new List<Button>();
				AddPopoverItem("AddPhotos".Translate(), "photo");
				AddPopoverItem("ChangeEvent".Translate(), "pencil");
				AddPopoverItem("RemoveMoment".Translate(), "trash");

				m_popover = new Popover(new RectangleF(0f, 0f, 220f, m_popoverItems.Count * 36f));
				m_popover.MinY = tblView.Frame.Y;
				m_popover.AddSubviews(m_popoverItems.ToArray());
			}

			if (m_descriptionPopover == null)
			{
				m_descriptionPopover = new Popover(RectangleF.Empty);
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
		/// Adds the popover item.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="iconClass">Icon class.</param>
		void AddPopoverItem(string title, string iconClass)
		{
			var button = new Button(new RectangleF(0f, m_popoverItems.Count * 36f, 220f, 36f));
			button.TitleEdgeInsets = new UIEdgeInsets(1f, 17f, 0f, 0f);
			button.ImageEdgeInsets = new UIEdgeInsets(0f, 10f, 0f, 0f);
			button.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			button.VerticalAlignment = UIControlContentVerticalAlignment.Center;
			button.SetTitle(title, UIControlState.Normal);
			button.SetStyleClass("button-popover " + iconClass);

			var proxy = new EventProxy<TimelineViewController, EventArgs>(this);
			proxy.Action = (target, sender, args) => {
				CurrentContext.Instance.Moment = target.m_tableSource.MomentAt(target.m_currentIndexPath);
				var buttonSender = (Button) sender;
				var indexOf = target.m_popoverItems.IndexOf(buttonSender);

				if (indexOf == 0)
				{
					target.AddPhotos(buttonSender);
				}
				else if (indexOf == 1)
				{
					CurrentContext.Instance.SelectedEvent = CurrentContext.Instance.Moment.Event;
					target.ChangeEvent(buttonSender);
				}
				else if (indexOf == 2)
				{
					target.RemoveCurrentRow();
				}

				target.HidePopovers();
			};
			button.TouchUpInside += proxy.HandleEvent;

			m_popoverItems.Add(button);
		}

		/// <summary>
		/// Adds the photos.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void AddPhotos(Button sender)
		{
			ShowProgressWhilePerforming(() => {
				RootViewController.PerformSegue("segueMedia", sender);
			}, false);
		}

		/// <summary>
		/// Changes the event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void ChangeEvent(Button sender)
		{
			ShowProgressWhilePerforming(() => {
				RootViewController.PerformSegue("segueEvent", sender);
			}, false);
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
			if (!m_descriptionPopover.IsVisible)
			{
				m_currentIndexPath = tblView.IndexPathForCell(cell);

				var description = m_tableSource.MomentAt(m_currentIndexPath).Description;
				if (!string.IsNullOrEmpty(description))
				{
					var label = m_descriptionPopover.Subviews[0] as Label;
					label.Text = description;

					var width = label.Frame.Width + label.Frame.X + label.Frame.Y;
					var height = label.Frame.Height + (label.Frame.Y * 2f);
					m_descriptionPopover.Frame = new RectangleF(0f, 0f, width, height);

					m_descriptionPopover.Show(new PointF(UIScreen.MainScreen.Bounds.Width - width, CurrentCellRect.Y));
				}
			}
			else
			{
				HidePopovers();
			}
		}

		/// <summary>
		/// Gets the current cell rect.
		/// </summary>
		/// <value>The current cell rect.</value>
		RectangleF CurrentCellRect
		{
			get {
				var tableRect = tblView.RectForRowAtIndexPath(m_currentIndexPath);
				return tblView.ConvertRectToView(tableRect, View);
			}
		}
	}
}