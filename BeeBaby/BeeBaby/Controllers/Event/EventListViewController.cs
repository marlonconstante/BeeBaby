using System;
using System.Linq;
using UIKit;
using System.Collections.Generic;
using Domain.Moment;
using Application;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using CoreGraphics;
using PixateFreestyleLib;
using Foundation;
using System.Diagnostics;
using Domain.Baby;
using BeeBaby.Proxy;
using BeeBaby.VisualElements;

namespace BeeBaby.Controllers
{
	public partial class EventListViewController : NavigationViewController
	{
		const float s_buttonSizeX = 106.66f;
		const float s_buttonSizeY = 100f;
		const float s_imageSize = 77f;
		const float s_buttonTitleHeight = 20f;

		const string s_recomendationTagName = "Recomendations";
		const string s_firstsTagName = "Firsts";
		const string s_everydayTagName = "Everyday";

		nfloat m_tagsHeight;
		nfloat m_minTagsHeight;
		string m_selectedTag;
		IList<string> m_buttonNamesList;
		IList<Event> m_events;
		EventService m_eventService;
		EventListViewSource m_eventListViewSource;

		public EventListViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{

			base.ViewDidLoad();

			CreateButtonsList();

			m_tagsHeight = tagsHeightConstraint.Constant;

			m_selectedTag = string.Empty;

			var proxy = new EventProxy<EventListViewController, EventArgs>(this);
			proxy.Action = (target, sender, args) =>
			{
				target.ScrollEvent();
			};
			scrView.Scrolled += proxy.HandleEvent;

			ConfigureScrollView();
			AddButtons();

			m_eventService = new EventService();

			InvokeInBackground(() =>
			{
				LoadEvents();

				InvokeOnMainThread(() =>
				{
					m_eventListViewSource = new EventListViewSource(this, m_events);
					schBar.Delegate = new EventTableSearchBarDelegate(this, m_eventListViewSource, CurrentContext.Instance.AllEvents);
					tblView.Source = m_eventListViewSource;
					tblView.ReloadData();
					
					var recomendedButton = scrView.Subviews.FirstOrDefault(s => s.Tag == s_recomendationTagName.GetHashCode()) as UIButton;
					SelectTag(recomendedButton);
				});
			});
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			MoveScrollToTop(false);
		}
			
		/// <summary>
		/// Shows the view tags.
		/// </summary>
		public void ShowViewTags()
		{
			MoveScroll(1f, true);
		}

		/// <summary>
		/// Hides the view tags.
		/// </summary>
		public void HideViewTags()
		{
			MoveScroll(-1f, true);
		}

		/// <summary>
		/// Moves the scroll to top.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public void MoveScrollToTop(bool animated = true)
		{
			if (tblView.NumberOfRowsInSection(0) > 0)
			{
				tblView.DeselectRow(tblView.IndexPathForSelectedRow, animated);
				tblView.ScrollRectToVisible(new CGRect(0f, 0f, 1f, schBar.Frame.Height), animated);
				ResizeViewTags(m_tagsHeight, animated);
			}
		}

		/// <summary>
		/// Moves the scroll.
		/// </summary>
		/// <param name="y">The y coordinate.</param>
		/// <param name="adjustLimit">If set to <c>true</c> adjust limit.</param>
		public void MoveScroll(nfloat y, bool adjustLimit)
		{
			var height = tagsHeightConstraint.Constant + y;
			if ((height > m_tagsHeight) || (adjustLimit && y > 0f))
			{
				height = m_tagsHeight;
			}
			else if ((height < m_minTagsHeight) || (adjustLimit && y < 0f))
			{
				height = m_minTagsHeight;
			}
			ResizeViewTags(height, adjustLimit);
		}

		/// <summary>
		/// Resizes the view tags.
		/// </summary>
		/// <param name="height">Height.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		void ResizeViewTags(nfloat height, bool animated)
		{
			var difference = Math.Abs(tagsHeightConstraint.Constant - height);
			if (difference != 0f)
			{
				if (animated)
				{
					UIView.Animate(difference / 200d, () => {
						tagsHeightConstraint.Constant = height;
						View.LayoutIfNeeded();
					});
				}
				else
				{
					tagsHeightConstraint.Constant = height;
				}
			}
		}

		/// <summary>
		/// Scrolls the event.
		/// </summary>
		void ScrollEvent()
		{

			pcrPager.CurrentPage = (int)Math.Floor(scrView.ContentOffset.X / scrView.Frame.Size.Width);
		}

		/// <summary>
		/// Adds the buttons.
		/// </summary>
		void AddButtons()
		{
			var up = true;
			var x = 0f;
			var y = 0f;

			foreach (var item in m_buttonNamesList)
			{
				var button = CreateButton(x, y, item);
				scrView.AddSubview(button);

				if (up)
				{
					y = s_buttonSizeY;
				}
				else
				{
					x = x + s_buttonSizeX;
					y = 0;
				}
				up = !up;
			}
		}

		/// <summary>
		/// Creates the button.
		/// </summary>
		/// <returns>The button.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="tagName">Tag name.</param>
		UITagButton CreateButton(nfloat x, nfloat y, string tagName)
		{
			var button = new UITagButton(new CGRect(x, y, s_buttonSizeX, s_buttonSizeY));
			button.BackgroundColor = UIColor.Clear;
			button.TagName = tagName;
			button.MultipleTouchEnabled = true;
			button.Tag = tagName.GetHashCode();

			var proxy = new EventProxy<EventListViewController, EventArgs>(this);
			proxy.Action = (target, sender, args) =>
			{
				target.SelectTag((UIButton)sender);
			};
			button.TouchUpInside += proxy.HandleEvent;

			SetTitle(button, tagName);
			SetImage(button, tagName);

			return button;
		}

		/// <summary>
		/// Selects the button.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="selected">If set to <c>true</c> selected.</param>
		void SelectButton(UITagButton sender, bool selected)
		{
			sender.Selected = selected;
			if (selected)
			{
				var param = new NSDictionary("Tag", sender.TagName);

				sender.AddStyleClass("border");
				sender.AddStyleClass("selected");
			}
			else
			{
				sender.RemoveStyleClass("border");
				sender.RemoveStyleClass("selected");
			}
		}

		/// <summary>
		/// Sets the image.
		/// </summary>
		/// <param name="button">Button.</param>
		/// <param name="tagName">Tag name.</param>
		void SetImage(UITagButton button, string tagName)
		{
			const float x = ((s_buttonSizeX - s_imageSize) / 2);

			var view = new UIView(new CGRect(x, 0f, s_imageSize, s_imageSize));
			view.UserInteractionEnabled = false;
			view.ContentMode = UIViewContentMode.Center;
			view.SetStyleClass(tagName.ToLower());

			var overlay = new UIView(new CGRect(0f, 0f, s_imageSize, s_imageSize));
			overlay.SetStyleClass("event-tag-overlay");
			view.AddSubview(overlay);

			button.AddSubview(view);
		}

		/// <summary>
		/// Configures the scroll view.
		/// </summary>
		void ConfigureScrollView()
		{
			var numberOfTags = m_buttonNamesList.Count;
			var scrollWidth = (nfloat)(Math.Floor(numberOfTags / 6f) + 1) * scrView.Frame.Size.Width;

			scrView.ContentSize = new CGSize(scrollWidth, s_buttonSizeY * 2);
			scrView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

			pcrPager.Pages = (int)Math.Floor(scrView.ContentSize.Width / scrView.Frame.Size.Width);
		}

		/// <summary>
		/// Loads the events.
		/// </summary>
		void LoadEvents()
		{
			m_events = m_eventService.GetAllEventsWithNonUsedAchivments().ToList();
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			TitleScreen = "WhatEventIsThis".Translate();
		}

		/// <summary>
		/// Selects the tag.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void SelectTag(UIButton sender)
		{
			var button = (UITagButton)sender;
			schBar.Text = string.Empty;

			if (m_selectedTag != string.Empty && m_selectedTag != button.TagName)
			{
				DeselectAllTags();
				FilterTableByTag(button.TagName);
				sender.Selected = true;
				SelectButton(button, true);
			}
			else if (m_selectedTag != string.Empty)
			{
				SetViewSource(m_events);
				tblView.ReloadData();
				m_selectedTag = string.Empty;
				sender.Selected = false;
				SelectButton(button, false);
			}
			else
			{
				FilterTableByTag(button.TagName);
				sender.Selected = true;
				SelectButton(button, true);
			}
		}

		/// <summary>
		/// Deselects all tags.
		/// </summary>
		public void DeselectAllTags()
		{
			foreach (UIView item in scrView.Subviews)
			{
				var button = item as UITagButton;
				if (button != null && button.Selected)
				{
					SelectButton(button, false);
					break;
				}
			}
		}

		/// <summary>
		/// Filters the table by tag.
		/// </summary>
		/// <param name="tagName">Tag name.</param>
		void FilterTableByTag(string tagName)
		{
			m_selectedTag = tagName;

			List<Event> filtredEvents = new List<Event>();

			if (m_selectedTag == s_firstsTagName)
			{
				filtredEvents = m_events.Where(e => e.Kind == EventType.Achievement).ToList();
			}
			else if (m_selectedTag == s_recomendationTagName)
			{
				var babyAge = (IsCameraFlow() || IsMediaFlow())
					? CurrentContext.Instance.CurrentBaby.AgeInDays 
					: CurrentContext.Instance.CurrentBaby.CalculateAgeInDay(CurrentContext.Instance.Moment.Date);

				filtredEvents = m_events.Where(e => e.StartAge <= babyAge && e.EndAge >= babyAge).ToList();
			}
			else if (m_selectedTag == s_everydayTagName)
			{
				filtredEvents = m_events.Where(e => e.Kind == EventType.Everyday).ToList();
			}
			else
			{
				filtredEvents = m_events.Where(e => e.Tag.ToString() == m_selectedTag).ToList();
			}

			if (m_selectedTag != s_recomendationTagName)
			{
				var baby = CurrentContext.Instance.CurrentBaby;
				var tempEvents = filtredEvents.Where(o => ((o.StartAge - baby.AgeInDays) + (o.EndAge - baby.AgeInDays) + (o.EndAge - o.StartAge)) >= 0).OrderBy(o => (o.StartAge - baby.AgeInDays) + (o.EndAge - baby.AgeInDays) + (o.EndAge - o.StartAge)).ToList();
				tempEvents.AddRange(filtredEvents.Where(o => ((o.StartAge - baby.AgeInDays) + (o.EndAge - baby.AgeInDays) + (o.EndAge - o.StartAge)) < 0).OrderByDescending(o => (o.StartAge - baby.AgeInDays) + (o.EndAge - baby.AgeInDays) + (o.EndAge - o.StartAge)));
				filtredEvents = tempEvents;
			}

			SetViewSource(filtredEvents);
			tblView.ReloadData();
		}

		/// <summary>
		/// Sets the view source.
		/// </summary>
		/// <param name="events">Events.</param>
		void SetViewSource(IList<Event> events)
		{
			m_eventListViewSource.ReloadData(tblView, events);

			MoveScrollToTop();
		}

		/// <summary>
		/// Sets the title.
		/// </summary>
		/// <param name="button">Button.</param>
		/// <param name="tagName">Tag name.</param>
		void SetTitle(UITagButton button, string tagName)
		{
			var text = tagName.Translate();
			var label = new UILabel(new CGRect(0, s_imageSize, s_buttonSizeX, 20));
			label.Text = text;
			label.TextAlignment = UITextAlignment.Center;
			label.SetStyleClass("tag-button-text");

			button.AddSubview(label);
		}

		/// <summary>
		/// Creates the buttons list.
		/// </summary>
		void CreateButtonsList()
		{
			m_buttonNamesList = new List<string>();
			m_buttonNamesList.Add(s_recomendationTagName);
			m_buttonNamesList.Add(s_firstsTagName);
			m_buttonNamesList.Add(s_everydayTagName);

			foreach (var item in Enum.GetValues(typeof(TagType)))
			{
				m_buttonNamesList.Add(item.ToString());
			}
		}

		/// <summary>
		/// Clears the selected tag.
		/// </summary>
		public void ClearSelectedTag()
		{
			DeselectAllTags();
			m_selectedTag = string.Empty;
		}

		/// <summary>
		/// Gos the back to moment.
		/// </summary>
		public void GoBackToMoment()
		{
			LeftBarButtonAction();
		}

		/// <summary>
		/// Updates the height of the view tags.
		/// </summary>
		public void UpdateViewTagsHeight()
		{
			var height = m_eventListViewSource.GetHeightForRow(tblView, NSIndexPath.FromRowSection(0, 0)) * tblView.NumberOfRowsInSection(0);
			var minHeight = UIScreen.MainScreen.Bounds.Height - (height + 64f);
			if (minHeight > m_tagsHeight)
			{
				minHeight = m_tagsHeight;
			}
			else if (minHeight < 0f)
			{
				minHeight = 0f;
			}
			m_minTagsHeight = minHeight;

			tblHeightConstraint.Constant = UIScreen.MainScreen.Bounds.Height - m_minTagsHeight;
		}

		/// <summary>
		/// Gets the height of the view tags.
		/// </summary>
		/// <value>The height of the view tags.</value>
		public nfloat ViewTagsHeight {
			get {
				return m_tagsHeight;
			}
		}

		/// <summary>
		/// Gets all events.
		/// </summary>
		/// <value>All events.</value>
		public IList<Event> AllEvents {
			get {
				return m_events;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BeeBaby.EventListViewController"/> show everyday events.
		/// </summary>
		/// <value><c>true</c> if show everyday events; otherwise, <c>false</c>.</value>
		public bool ShowEverydayEvents { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BeeBaby.EventListViewController"/> show firsts events.
		/// </summary>
		/// <value><c>true</c> if show firsts events; otherwise, <c>false</c>.</value>
		public bool ShowFirstsEvents { get; set; }
	}
}