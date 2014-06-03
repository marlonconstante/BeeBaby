using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public partial class EventsListTabViewController : UITabBarController
	{
		UIStoryboard m_board;
		UIViewController m_firstTab, m_secondTab;

		public EventsListTabViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			m_board = UIStoryboard.FromName("MainStoryboard", null);
			EventListViewController view = (EventListViewController)m_board.InstantiateViewController("EventList");
			EventListViewController view2 = (EventListViewController)m_board.InstantiateViewController("EventList");

			view.ShowEverydayEvents = true;
			view.ShowFirstsEvents = false;
			m_firstTab = view;
			m_firstTab.Title = "EveryDay".Translate();

			view2.ShowEverydayEvents = false;
			view2.ShowFirstsEvents = true;
			m_secondTab = view2;
			m_secondTab.Title = "Firsts".Translate();
			var tabs = new []{
				m_firstTab, m_secondTab
			};
			ViewControllers = tabs;

			TabBar.Translucent = false;
		}

		/// <summary>
		/// Gets the navigation item.
		/// </summary>
		/// <value>The navigation item.</value>
		public override UINavigationItem NavigationItem {
			get {
				var selectedTab = SelectedViewController;
				return (selectedTab != null) ? selectedTab.NavigationItem : base.NavigationItem;
			}
		}
	}
}