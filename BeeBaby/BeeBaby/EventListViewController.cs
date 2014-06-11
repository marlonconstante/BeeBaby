using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Domain.Moment;
using Application;
using Skahal.Infrastructure.Framework.Globalization;
using System.Drawing;
using PixateFreestyleLib;

namespace BeeBaby
{
	public partial class EventListViewController : NavigationViewController
	{
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

		IList<Event> m_events;
		EventService m_eventService;
		string m_selectedTag;
		IList<string> m_buttonNamesList;
		EventListViewSource m_eventListViewSource;

		const float s_buttonSizeX = 106f;
		const float s_buttonSizeY = 100f;
		const float s_imageSize = 70f;
		const float s_buttonTitleHeight = 20f;

		const string s_recomendationTagName = "Recomendation";
		const string s_firstsTagName = "Firsts";
		const string s_everydayTagName = "Everyday";


		public EventListViewController(IntPtr handle) : base(handle)
		{
			CreateButtonsList();
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			m_eventService = new EventService();

			var allEvents = m_eventService.GetAllEvents();
			m_events = LoadEvents(allEvents);
			m_eventListViewSource = new EventListViewSource(this, m_events);
			schBar.Delegate = new EventTableSearchBarDelegate(this, m_eventListViewSource, allEvents);
			tblView.Source = m_eventListViewSource;

			m_selectedTag = string.Empty;

			ControlEvents.Add(new ControlEvent(scrView, ScrollEvent, ControlEventType.Scrolled));

			ConfigureScrollView();
			AddButtons();

			View.AddSubview(scrView);
		}
			
		/// <summary>
		/// Scrolls the event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void ScrollEvent(object sender, EventArgs e)
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
				//var tag = (TagType)Enum.Parse(typeof(TagType), item);
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
		UITagButton CreateButton(float x, float y, string tagName)
		{
			var button = new UITagButton(new RectangleF(x, y, s_buttonSizeX, s_buttonSizeY));
			button.BackgroundColor = UIColor.Clear;
			button.TagName = tagName;
			SetTitle(button, tagName);
			SetImage(button, tagName);
			button.TouchUpInside += (sender, e) => SelectTag2(button);
			button.MultipleTouchEnabled = true;
			return button;
		}

		void SetImage(UITagButton button, string tagName)
		{
			var imageName = "Familia.png";//string.Format("{0}.png", tagName);
			var iconImage = new UIImage(imageName);
			const float x = (s_buttonSizeX - s_imageSize) / 2;
			var imageView = new UIImageView(new RectangleF(x, 0, s_imageSize, s_imageSize));

			imageView.ContentMode = UIViewContentMode.ScaleToFill;
			imageView.Image = iconImage;
			button.AddSubview(imageView);
		}

		/// <summary>
		/// Configures the scroll view.
		/// </summary>
		void ConfigureScrollView()
		{
			var numberOfTags = Enum.GetNames(typeof(TagType)).Length;
			var scrollWidth = (float)(Math.Truncate(numberOfTags / 6f) + 1) * scrView.Frame.Size.Width;

			scrView.ContentSize = new SizeF(scrollWidth, s_buttonSizeY * 2);
			scrView.UserInteractionEnabled = true;
			scrView.MultipleTouchEnabled = true;
			scrView.PagingEnabled = true;
			scrView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

			pcrPager.Pages = (int)Math.Floor(scrView.ContentSize.Width / scrView.Frame.Size.Width);
		}

		/// <summary>
		/// Loads the events.
		/// </summary>
		/// <returns>The events.</returns>
		/// <param name="events">Events.</param>
		public IList<Event> LoadEvents(IEnumerable<Event> events)
		{
			var eventType = ShowFirstsEvents ? EventType.Achivment : EventType.Everyday;
			return m_eventService.GetEventsOrdered(CurrentContext.Instance.CurrentBaby, eventType).ToList();
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			TitleScreen = "Event".Translate();
		}

		/// <summary>
		/// Selects the tag.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void SelectTag2(UIButton sender)
		{
			var button = (UITagButton)sender;

			if (m_selectedTag != string.Empty && m_selectedTag != button.TagName)
			{
				DeselectAllTags();
				FilterTableByTag((UITagButton)sender);
				sender.Selected = true;
				sender.BackgroundColor = UIColor.Blue;

			}
			else if (m_selectedTag != string.Empty)
			{
				SetViewSource(m_events);
				tblView.ReloadData();
				m_selectedTag = string.Empty;
				sender.Selected = false;
				sender.BackgroundColor = UIColor.Clear;
			}
			else
			{
				FilterTableByTag((UITagButton)sender);
				sender.Selected = true;
				sender.BackgroundColor = UIColor.Blue;
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
					item.BackgroundColor = UIColor.Clear;
					button.Selected = false;
					break;
				}
			}
		}

		/// <summary>
		/// Filters the table by tag.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void FilterTableByTag(UITagButton sender)
		{
			m_selectedTag = sender.TagName;

			List<Event> filtredEvents = new List<Event>();

			if (m_selectedTag == s_firstsTagName)
			{
			}
			else if (m_selectedTag == s_recomendationTagName)
			{
			}
			else if (m_selectedTag == s_everydayTagName)
			{
			}
			else
			{
				filtredEvents  = m_events.Where(e => e.Tag.ToString() == m_selectedTag).ToList();
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
		}

		/// <summary>
		/// Sets the title.
		/// </summary>
		/// <param name="button">Button.</param>
		/// <param name="tagName">Tag name.</param>
		void SetTitle(UITagButton button, string tagName)
		{
			var text = tagName.Translate();
			var label = new UILabel(new RectangleF(0, s_imageSize, s_buttonSizeX, 20));
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

	}
}