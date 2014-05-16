using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public partial class MenuNavigationController : UINavigationController
	{
		public MenuNavigationController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			UIStoryboard board = UIStoryboard.FromName("MainStoryboard", null);
			var slideoutNavigation = (SlideoutNavigationController) board.InstantiateViewController("SlideoutNavigationController");
			var menu = (MenuViewController) board.InstantiateViewController("MenuViewController");
			var timeline = (TimelineViewController) board.InstantiateViewController("TimelineViewController");

			slideoutNavigation.View.MultipleTouchEnabled = true;
			slideoutNavigation.View.UserInteractionEnabled = true;
			slideoutNavigation.RightMenuEnabled = false;
			slideoutNavigation.DisplayNavigationBarOnLeftMenu = false;
			slideoutNavigation.ShadowOpacity = 0.1f;
			slideoutNavigation.SlideWidth = 250f;
			slideoutNavigation.TopView = timeline;
			slideoutNavigation.MenuViewLeft = menu;

			var window = UIApplication.SharedApplication.Windows[0];
			window.RootViewController = slideoutNavigation;
			window.MakeKeyAndVisible();
		}
	}
}