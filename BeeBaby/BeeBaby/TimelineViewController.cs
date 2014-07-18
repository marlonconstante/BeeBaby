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

namespace BeeBaby
{
	public partial class TimelineViewController : NavigationViewController
	{
		static bool s_openCamera = true;
		NSIndexPath m_currentIndexPath;
		Popover m_popover;
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
			new ImageProvider().DeleteFiles(true);

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
				var baby = CurrentContext.Instance.CurrentBaby;
				var moments = new MomentService().GetAllMoments(baby);

				lblBabyName.Text = baby.Name;
				lblBabyAge.Text = baby.AgeInWords;

				InitPopover();

				m_tableSource = new TimelineViewSource(this, moments.ToList(), baby);
				tblView.Source = m_tableSource;
				tblView.ReloadData();

				CurrentContext.Instance.ReloadMoments = false;
			}
		}

		/// <summary>
		/// Inits the popover.
		/// </summary>
		void InitPopover()
		{
			if (m_popover == null)
			{
				var button = new Button(new RectangleF(0f, 0f, 200f, 32f));
				button.TitleEdgeInsets = new UIEdgeInsets(1f, 16f, 0f, 0f);
				button.ImageEdgeInsets = new UIEdgeInsets(0f, 10f, 0f, 0f);
				button.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
				button.VerticalAlignment = UIControlContentVerticalAlignment.Center;
				button.SetTitle("RemoveMoment".Translate(), UIControlState.Normal);
				button.SetStyleClass("button-trash");

				m_popover = new Popover(button);

				var proxy = new EventProxy<TimelineViewController, EventArgs>(this);
				proxy.Action = (target, sender, args) =>
				{
					target.RemoveCurrentRow();
					target.m_popover.DismissAnimated(true);
				};
				button.TouchUpInside += proxy.HandleEvent;
			}
		}

		/// <summary>
		/// Removes the current row.
		/// </summary>
		void RemoveCurrentRow()
		{
			var alertView = new UIAlertView("ConfirmDeletion".Translate(), "QuestionRemoveMoment".Translate(), null, null, "Yes".Translate(), "No".Translate());

			var proxy = new EventProxy<TimelineViewController, UIButtonEventArgs>(this);
			proxy.Action = (target, sender, args) =>
			{
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

			var rectangle = tblView.RectForRowAtIndexPath(m_currentIndexPath);
			rectangle.Height = 0f;
			rectangle.X = 44f;

			m_popover.Show(rectangle, tblView);
		}
	}
}