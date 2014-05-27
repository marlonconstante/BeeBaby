// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public partial class EventsListTabViewController : UITabBarController
	{
		UIViewController tab1, tab2;
		UIStoryboard m_board;
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
			tab1 = view;
			tab1.Title = "EveryDay".Translate();

			view2.ShowEverydayEvents = false;
			view2.ShowFirstsEvents = true;
			tab2 = view2;
			tab2.Title = "Firsts".Translate();
			var tabs = new UIViewController []{
				tab1, tab2
			};
			ViewControllers = tabs;
		}
	}
}