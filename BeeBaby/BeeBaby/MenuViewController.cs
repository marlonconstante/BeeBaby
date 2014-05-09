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

			tblView.SeparatorInset = UIEdgeInsets.Zero;
			tblView.TableHeaderView = new ProfileView(new RectangleF(0f, 0f, 245f, 195f));
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
			menuItems.Add(new MenuItem("Home", () => {
				NavigationController.PushViewController(controller, true);
			}));
			menuItems.Add(new MenuItem("About", () => {
				NavigationController.PushViewController(controller, true);
			}));
			menuItems.Add(new MenuItem("Deals".Translate(), () => {
				NavigationController.PushViewController(dealsController, true);
			}));
			menuItems.Add(new MenuItem("Full Screen", () => {
				NavigationController.PushViewController(controller, true);
			}));
			menuItems.Add(new MenuItem("Baby 1", () => {
				CurrentContext.Instance.CurrentBaby = new BabyService().GetBaby("1");
				PreferencesEditor.SaveLastUsedBaby("1");
				NavigationController.PushViewController(controller, true);
			}));
			menuItems.Add(new MenuItem("Baby 2", () => {
				CurrentContext.Instance.CurrentBaby = new BabyService().GetBaby("2");
				PreferencesEditor.SaveLastUsedBaby("2");
				NavigationController.PushViewController(controller, true);
			}));

			return menuItems;
		}
	}
}