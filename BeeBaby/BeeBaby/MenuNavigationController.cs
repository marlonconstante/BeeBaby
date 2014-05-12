using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.SlideoutNavigation;

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
			UIViewController controller = (UIViewController) board.InstantiateViewController("TimelineViewController");
			UIViewController menuViewController = (UIViewController) board.InstantiateViewController("MenuViewController");

			Menu = new SlideoutNavigationController();
			Menu.View.MultipleTouchEnabled = true;
			Menu.View.UserInteractionEnabled = true;
			Menu.RightMenuEnabled = false;
			Menu.DisplayNavigationBarOnLeftMenu = false;
			Menu.ShadowOpacity = 0.1f;
			Menu.SlideWidth = 250f;
			Menu.SlideHeight = 999f;
			Menu.TopView = controller;
			Menu.MenuViewLeft = menuViewController;

			Window = UIApplication.SharedApplication.Windows[0];
			Window.RootViewController = Menu;
			Window.MakeKeyAndVisible();
		}

		/// <summary>
		/// Gets or sets the window.
		/// </summary>
		/// <value>The window.</value>
		public UIWindow Window {
			get;
			set;
		}

		/// <summary>
		/// Gets the menu.
		/// </summary>
		/// <value>The menu.</value>
		public SlideoutNavigationController Menu {
			get;
			private set;
		}
	}
}