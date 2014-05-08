using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.SlideoutNavigation;
using System.Drawing;

namespace BeeBaby
{
	public partial class MenuViewController : UINavigationController
	{
		public MenuViewController(IntPtr handle) : base(handle)
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

			Menu = new SlideoutNavigationController();
			Menu.View.MultipleTouchEnabled = true;
			Menu.View.UserInteractionEnabled = true;
			Menu.RightMenuEnabled = false;
			Menu.DisplayNavigationBarOnLeftMenu = false;
			Menu.ShadowOpacity = 0.1f;
			Menu.SlideHeight = 9999f;
			Menu.TopView = controller;
			Menu.MenuViewLeft = new LeftMenuViewController();

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