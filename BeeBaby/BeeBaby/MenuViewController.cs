using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;
using Application;
using Domain.Baby;
using BeeBaby.Util;
using System.Drawing;
using System.Collections.Generic;

namespace BeeBaby
{
	public partial class MenuViewController : UIViewController
	{
		public MenuViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
				
			EdgesForExtendedLayout = UIRectEdge.None;

			tblView.TableHeaderView = new ProfileView(new RectangleF(0f, 0f, 250f, 195f));
			tblView.Source = new MenuViewSource(this, GetMenuItems());
		}

		/// <summary>
		/// Gets the menu items.
		/// </summary>
		/// <returns>The menu items.</returns>
		IList<MenuItem> GetMenuItems()
		{
			UIStoryboard board = UIStoryboard.FromName("MainStoryboard", null);
			UIViewController controller = (UIViewController) board.InstantiateViewController("TimelineViewController");
			UIViewController dealsController = (UIViewController) board.InstantiateViewController("DealsViewController");
		
			IList<MenuItem> menuItems = new List<MenuItem>();
			menuItems.Add(new MenuItem("Timeline".Translate(), "timeline", () => {
				//Vir selecionado e fazer pop???
				NavigationController.PushViewController(controller, true);
			}));
			menuItems.Add(new MenuItem("ProductsForYourChild".Translate(), "market", () => {
				NavigationController.PushViewController(dealsController, true);
			}));
			menuItems.Add(new MenuItem("MyProfile".Translate(), "profile", () => {
				NavigationController.PushViewController(controller, true);
			}));
			menuItems.Add(new MenuItem("InviteFriends".Translate(), "invite", () => {
				NavigationController.PushViewController(controller, true);
			}));
			menuItems.Add(new MenuItem("ManageFamily".Translate(), "family", () => {
				NavigationController.PushViewController(controller, true);
			}));
			menuItems.Add(new MenuItem("Configurations".Translate(), "gear", () => {
				NavigationController.PushViewController(controller, true);
			}));
			menuItems.Add(new MenuItem("About".Translate(), "about", () => {
				NavigationController.PushViewController(controller, true);
			}));
			menuItems.Add(new MenuItem("Exit".Translate(), "logoff", () => {
				NavigationController.PushViewController(controller, true);
			}));

			return menuItems;
		}
	}
}