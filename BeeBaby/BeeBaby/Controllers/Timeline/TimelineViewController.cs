using System;
using System.Linq;
using Foundation;
using UIKit;
using Domain.Moment;
using Domain.Baby;
using Application;
using BeeBaby.ResourcesProviders;
using BeeBaby.Util;
using Domain.User;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using CoreGraphics;
using PixateFreestyleLib;
using System.Collections.Generic;
using Infrastructure.Systems;
using BeeBaby.Proxy;
using BeeBaby.VisualElements;
using BigTed;
using System.Drawing;

namespace BeeBaby.Controllers
{
	public partial class TimelineViewController : SyncNavigationViewController
	{
		const float c_buttonHeight = 44f;
		NSIndexPath m_currentIndexPath;
		Popover<TimelineViewController, EventArgs> m_popover;
		MomentModalViewController m_modalViewController;
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


			CurrentContext.Instance.Scale = UIScreen.MainScreen.Scale;
			CurrentContext.Instance.ReloadMoments = true;

			LoadUser();
			LoadBaby();

			ReleaseControl.CheckForUpdates();
		}

		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			LoadEvents();
			InitTimeline();
		}
			
		/// <summary>
		/// Raises the sync performed event.
		/// </summary>
		public override void OnSyncPerformed()
		{
			base.OnSyncPerformed();

			InitTimeline();
		}

		/// <summary>
		/// Ends the editing.
		/// </summary>
		public override void EndEditing()
		{
			base.EndEditing();

			HidePopover();
		}

		/// <summary>
		/// Hides the popover.
		/// </summary>
		public void HidePopover()
		{
			if (m_popover != null && m_popover.IsVisible)
			{
				m_popover.Hide();
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
				m_modalViewController = (MomentModalViewController) board.InstantiateViewController("MomentModalViewController");
				m_modalViewController.LoadView();
			}
		}

		/// <summary>
		/// Shows the on boarding.
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void ShowOnBoarding<T>() where T : ModalViewController
		{
			var board = UIStoryboard.FromName("MainStoryboard", null);
			var onBoarding = (T) board.InstantiateViewController(typeof(T).Name);
			onBoarding.LoadView();
			onBoarding.Show();
		}

		/// <summary>
		/// Inits the timeline.
		/// </summary>
		async void InitTimeline()
		{
			if (CurrentContext.Instance.ReloadMoments)
			{
				var imageProvider = new ImageProvider();
				imageProvider.DeleteFiles(true);
				imageProvider.RemoveEmptyDirectories();

				var baby = CurrentContext.Instance.CurrentBaby;

				var moments = new MomentService().GetAllMoments(baby).ToList();
				//var moments = await RemoteDataSystem.GetAllMoments(baby).ToList();

				lblBabyName.Text = string.IsNullOrEmpty(baby.Name) ? "MyBaby".Translate() : baby.Name;
				lblBabyAge.Text = string.Concat("Have".Translate(), " ", baby.AgeInWords, " ", "old".Translate());

				InitPopover();

				if (m_tableSource == null)
				{
					m_tableSource = new TimelineViewSource(this);
					tblView.Source = m_tableSource;
				}

				if (moments.Count == 0)
				{
					BuildMomentTemplateIfNeeded();
					moments.Add(MomentTemplate);
				}

				m_tableSource.ReloadData(tblView, moments, baby);

				if (!PreferencesEditor.IsOnBoardingViewed)
				{
					ShowOnBoarding<OnBoardingModalViewController>();
					PreferencesEditor.IsOnBoardingViewed = true;
				}
				else if (!PreferencesEditor.IsConfigOnBoardingViewed)
				{
					ShowOnBoarding<ConfigOnBoardingModalViewController>();
				}

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
				var proxyAddPhotos = new EventProxy<TimelineViewController, EventArgs>(this);
				proxyAddPhotos.Action = (target, sender, args) => {
					target.AddPhotos((Button) sender);							
					target.HidePopover();
				};

				var proxyChangeMoment = new EventProxy<TimelineViewController, EventArgs>(this);
				proxyChangeMoment.Action = (target, sender, args) => {
					target.ChangeMoment((Button) sender);							
					target.HidePopover();
				};

				var proxySyncMoment = new EventProxy<TimelineViewController, EventArgs>(this);
				proxySyncMoment.Action = (target, sender, args) => {
					target.SyncMoment((Button) sender);							
					target.HidePopover();
				};

				var proxyRemoveRow = new EventProxy<TimelineViewController, EventArgs>(this);
				proxyRemoveRow.Action = (target, sender, args) => {
					target.RemoveCurrentRow();
					target.HidePopover();
				};

				m_popover = new Popover<TimelineViewController, EventArgs>(new CGRect(0f, 0f, 240f, 0));
				m_popover.MinY = tblView.Frame.Y;

				m_popover.AddPopoverItem("AddPhotos".Translate(), "photo", true, c_buttonHeight, proxyAddPhotos);
				m_popover.AddPopoverItem("ChangeMoment".Translate(), "pencil", true, c_buttonHeight, proxyChangeMoment);
//				m_popover.AddPopoverItem("SyncMoment".Translate(), "sync", true, c_buttonHeight, proxySyncMoment);
				m_popover.AddPopoverItem("RemoveMoment".Translate(), "trash", false, c_buttonHeight, proxyRemoveRow);

				m_popover.AddSubviews(m_popover.MenuItems.ToArray());
			}
		}

		/// <summary>
		/// Adds the photos.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void AddPhotos(Button sender)
		{
			ShowProgressWhilePerforming(() => {
				CurrentContext.Instance.Moment = m_tableSource.MomentAt(m_currentIndexPath) as Moment;
				RootViewController.PerformSegue("segueMedia", sender);
			}, false);
		}

		/// <summary>
		/// Changes the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void ChangeMoment(Button sender)
		{
			ShowProgressWhilePerforming(() => {
				CurrentContext.Instance.Moment = m_tableSource.MomentAt(m_currentIndexPath) as Moment;
				CurrentContext.Instance.SelectedEvent = CurrentContext.Instance.Moment.Event;
				RootViewController.PerformSegue("segueMoment", sender);
			}, false);
		}

		/// <summary>
		/// Synchronize the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		async void SyncMoment(Button sender)
		{
			BTProgressHUD.Show();

			var moment = m_tableSource.MomentAt(m_currentIndexPath) as Moment;
			var imageProvider = new ImageProvider(moment.Id);
			await RemoteDataSystem.SyncMoment(moment, imageProvider.GetDataImages(true), imageProvider.GetDataImages(false));

			BTProgressHUD.ShowSuccessWithStatus(string.Empty, 2000);
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

			LoadModalViewController();
			m_modalViewController.SetInformation(moment);

			m_modalViewController.Show();
		}

		/// <summary>
		/// Builds the moment template if needed.
		/// </summary>
		void BuildMomentTemplateIfNeeded()
		{
			if (MomentTemplate == null)
			{
				MomentTemplate = new Moment {
					Id = Moment.IdTemplate,
					Description = "MomentTemplateDescription".Translate(),
					Date = DateTime.Now,
					MediaCount = 2,
					Event = new Event {
						Description = "MomentTemplateEvent".Translate(),
						Kind = EventType.Achievement
					},
					Location = new Location {
						Name = "MomentTemplateLocation".Translate()
					}
				};
				MomentTemplate.Babies.Add(CurrentContext.Instance.CurrentBaby);

				SaveMomentTemplateImages();
				PreferencesEditor.IsOnBoardingViewed = false;
			}
		}

		/// <summary>
		/// Saves the moment template images.
		/// </summary>
		void SaveMomentTemplateImages()
		{
			var imageProvider = new ImageProvider(MomentTemplate.Id);
			imageProvider.DeleteFiles(false);

			for (var index = 1; index <= MomentTemplate.MediaCount; index++)
			{
				using (var photo = UIImage.FromFile(string.Concat("template-", index, ".jpg")))
				{
					imageProvider.SavePermanentImageOnApp(photo);
				}
			}
		}

		/// <summary>
		/// Gets or sets the moment template.
		/// </summary>
		/// <value>The moment template.</value>
		Moment MomentTemplate {
			get;
			set;
		}

		/// <summary>
		/// Gets the current cell rect.
		/// </summary>
		/// <value>The current cell rect.</value>
		CGRect CurrentCellRect {
			get {
				var tableRect = tblView.RectForRowAtIndexPath(m_currentIndexPath);
				return tblView.ConvertRectToView(tableRect, View);
			}
		}
	}
}