using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Domain.Moment;
using Application;
using Skahal.Infrastructure.Framework.Globalization;

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

			SetTitle(btnTag1, TagType.Sono);
			SetTitle(btnTag2, TagType.Sorriso);
			SetTitle(btnTag3, TagType.Banho);
			SetTitle(btnTag4, TagType.Brincadeiras);
			SetTitle(btnTag5, TagType.Passeio);
			SetTitle(btnTag6, TagType.Familia);
			SetTitle(btnTag7, TagType.Colo);
			SetTitle(btnTag8, TagType.Eventos);
			SetTitle(btnTag9, TagType.Corpinho);

			NavigationController.NavigationBar.Translucent = false;
		}

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
			btnTag1.Selected = false;
			btnTag2.Selected = false;
			btnTag3.Selected = false;
			btnTag4.Selected = false;
			btnTag5.Selected = false;
			btnTag6.Selected = false;
			btnTag7.Selected = false;
			btnTag8.Selected = false;
			btnTag9.Selected = false;
		}

		/// <summary>
		/// Filters the table by tag.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void FilterTableByTag(UIButton sender)
		{
			var selectedValue = (TagType)Enum.Parse(typeof(TagType), sender.TitleLabel.Text);
			var filtredEvents = m_events.Where(e => e.Tag == selectedValue).ToList();
			SetViewSource(filtredEvents);
			tblView.ReloadData();
			m_selectedTag = sender.TitleLabel.Text;
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
			button.SetTitle(Enum.GetName(typeof(TagType), tag).Translate(), UIControlState.Normal);
		}
	}
}