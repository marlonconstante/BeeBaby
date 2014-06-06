using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Domain.Moment;
using Application;
using Skahal.Infrastructure.Framework.Globalization;
using System.Drawing;

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
		EventListViewSource m_eventListViewSource;
		const float s_buttonSize = 106f;

		public EventListViewController(IntPtr handle) : base(handle)
		{
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
			schBar.Delegate = new EventTableSearchBarDelegate(m_eventListViewSource, this, allEvents);
			tblView.Source = m_eventListViewSource;

			m_selectedTag = string.Empty;

			ConfigureScrollView();

			AddButtons();

			View.AddSubview(scrView);
		}

		/// <summary>
		/// Adds the buttons.
		/// </summary>
		void AddButtons()
		{
			var up = true;
			var x = 0f;
			var y = 0f;
			var valuesAsArray = Enum.GetValues(typeof(TagType));
			foreach (var item in valuesAsArray)
			{
				var button = CreateButton(x, y, (TagType)item);
				scrView.AddSubview(button);
				if (up)
				{
					y = s_buttonSize;
				}
				else
				{
					x = x + s_buttonSize;
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
		/// <param name="tag">Tag.</param>
		UIButton CreateButton(float x, float y, TagType tag)
		{
			var button = new UIButton(new RectangleF(x, y, s_buttonSize, s_buttonSize));
			button.BackgroundColor = UIColor.Clear;
			SetTitle(button, tag);
			SetImage(button, tag);
			button.TouchUpInside += (sender, e) => FilterTableByTag(button);
			button.MultipleTouchEnabled = true;
			return button;
		}

		void SetImage(UIButton button, TagType tag)
		{
//			var tagName = Enum.GetName(typeof(TagType), tag);
			var imageName = "Familia.png";//string.Format("{0}.png", tagName);
			var iconImage = new UIImage(imageName);
			var imageSize = 70f;
			var x = (s_buttonSize - imageSize) / 2;
			var imageView = new UIImageView(new RectangleF(x, 10, imageSize, imageSize));
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
			var scrollWidth = numberOfTags * s_buttonSize;

			scrView.ContentSize = new SizeF(scrollWidth, s_buttonSize * 2);
			scrView.UserInteractionEnabled = true;
			scrView.MultipleTouchEnabled = true;
			scrView.PagingEnabled = true;
			scrView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
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
		partial void SelectTag(MonoTouch.UIKit.UIButton sender)
		{
			if (m_selectedTag != string.Empty && m_selectedTag != sender.TitleLabel.Text)
			{
				DeselectAllTags();
				FilterTableByTag(sender);
				sender.Selected = true;
			}
			else if (m_selectedTag != string.Empty)
			{
				SetViewSource(m_events);
				tblView.ReloadData();
				m_selectedTag = string.Empty;
				sender.Selected = false;
			}
			else
			{
				FilterTableByTag(sender);
				sender.Selected = true;
			}
		}

		/// <summary>
		/// Deselects all tags.
		/// </summary>
		public void DeselectAllTags()
		{
//			btnTag1.Selected = false;
//			btnTag2.Selected = false;
//			btnTag3.Selected = false;
//			btnTag4.Selected = false;
//			btnTag5.Selected = false;
//			btnTag6.Selected = false;
//			btnTag7.Selected = false;
//			btnTag8.Selected = false;
//			btnTag9.Selected = false;
		}

		/// <summary>
		/// Filters the table by tag.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void FilterTableByTag(UIButton sender)
		{
//			var selectedValue = (TagType)Enum.Parse(typeof(TagType), sender.TitleLabel.Text);
//			var filtredEvents = m_events.Where(e => e.Tag == selectedValue).ToList();
//			SetViewSource(filtredEvents);
//			tblView.ReloadData();
//			m_selectedTag = sender.TitleLabel.Text;
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
		/// <param name="tag">Tag.</param>
		void SetTitle(UIButton button, TagType tag)
		{
			var y = s_buttonSize - 20;
			var text = Enum.GetName(typeof(TagType), tag).Translate();
			var label = new UILabel(new RectangleF(0, y, s_buttonSize, 20));
			label.Text = text;
			label.TextAlignment = UITextAlignment.Center;

			button.AddSubview(label);
		}
	}
}