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
		bool m_isFiltredByTag;

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
			m_events = allEvents
				.Where(e => (ShowFirstsEvents && e.Kind == EventType.Achivment) ||
			(ShowEverydayEvents && e.Kind == EventType.Everyday))
				.ToList();
					
			EventListViewSource eventListViewSource = new EventListViewSource(this, m_events);
			schBar.Delegate = new EventTableSearchBarDelegate(eventListViewSource, allEvents);
			tblView.Source = eventListViewSource;

			m_isFiltredByTag = false;

			SetTitle(btnTag1, TagType.Sono);
			SetTitle(btnTag2, TagType.Sorriso);
			SetTitle(btnTag3, TagType.Banho);
			SetTitle(btnTag4, TagType.Brincadeiras);
			SetTitle(btnTag5, TagType.Passeio);
			SetTitle(btnTag6, TagType.Familia);
			SetTitle(btnTag7, TagType.Colo);
			SetTitle(btnTag8, TagType.Eventos);
			SetTitle(btnTag9, TagType.Corpinho);

		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			TitleScreen = "Event".Translate();
		}

		partial void SelectTag1(UIButton sender)
		{
			SelectTag(sender);
		}



		void SelectTag(UIButton sender)
		{
			if (m_isFiltredByTag)
			{
				SetViewSource(m_events);
				tblView.ReloadData();
				m_isFiltredByTag = false;
			}
			else
			{
				FilterTableByTag(sender);
			}
		}

		void FilterTableByTag(UIButton sender)
		{
			var selectedValue = (TagType)Enum.Parse(typeof(TagType), sender.TitleLabel.Text);
			var filtredEvents = m_events.Where(e => e.Tag == selectedValue).ToList();
			SetViewSource(filtredEvents);
			tblView.ReloadData();
			m_isFiltredByTag = true;
		}

		void SetViewSource(IList<Event> events)
		{
			EventListViewSource eventListViewSource = new EventListViewSource(this, events);
			tblView.Source = eventListViewSource;
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