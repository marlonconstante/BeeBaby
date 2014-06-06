using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public partial class EventsListTabViewController : UITabBarController
	{
		UIStoryboard m_board;
		EventListViewController m_firstTab, m_secondTab;

		public EventsListTabViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			m_board = UIStoryboard.FromName("MainStoryboard", null);

			m_firstTab = (EventListViewController)m_board.InstantiateViewController("EventList");
			m_firstTab.Title = "EveryDay".Translate();
			m_firstTab.ShowEverydayEvents = true;
			m_firstTab.ShowFirstsEvents = false;

			m_secondTab = (EventListViewController)m_board.InstantiateViewController("EventList");
			m_secondTab.Title = "Firsts".Translate();
			m_secondTab.ShowEverydayEvents = false;
			m_secondTab.ShowFirstsEvents = true;

			var tabs = new []
			{
				m_firstTab, m_secondTab
			};
			ViewControllers = tabs;
		}

		/// <summary>
		/// Gets the navigation item.
		/// </summary>
		/// <value>The navigation item.</value>
		public override UINavigationItem NavigationItem
		{
			get
			{
				var selectedTab = SelectedViewController;
				return (selectedTab != null) ? selectedTab.NavigationItem : base.NavigationItem;
			}
		}
	}
}