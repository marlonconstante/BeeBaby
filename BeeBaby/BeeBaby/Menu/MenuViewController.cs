using System;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;
using Application;
using Domain.Baby;
using BeeBaby.Util;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using Domain.Moment;
using Infrastructure.Systems;
using BeeBaby.ViewModels;

namespace BeeBaby
{
	public partial class MenuViewController : BaseViewController
	{
		UIStoryboard m_board;
		ProfileView m_profileView;

		public MenuViewController(IntPtr handle) : base(handle)
		{
			m_board = UIStoryboard.FromName("MainStoryboard", null);
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
				
			EdgesForExtendedLayout = UIRectEdge.None;

			m_profileView = new ProfileView(new RectangleF(0f, 0f, 250f, 195f));
			tblView.TableHeaderView = m_profileView;

			tblView.Source = new MenuViewSource(this, GetMenuItems());
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			m_profileView.Redraw();
		}

		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			FlurryAnalytics.Flurry.LogEvent("Menu: Abriu");

			base.ViewDidAppear(animated);

			if (CurrentStoryboardId == null)
			{
				tblView.SelectRow(NSIndexPath.FromRowSection(0, 0), false, UITableViewScrollPosition.None);
				CurrentStoryboardId = "TimelineViewController";
			}
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			FlurryAnalytics.Flurry.LogEvent("Menu: Fechou");
			base.ViewWillDisappear(animated);
		}

		/// <summary>
		/// Determines whether this instance is log flow.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		public override bool IsLogFlow()
		{
			return false;
		}

		/// <summary>
		/// Gets the menu items.
		/// </summary>
		/// <returns>The menu items.</returns>
		IList<MenuItem> GetMenuItems()
		{
			IList<MenuItem> menuItems = new List<MenuItem>();

			menuItems.Add(new MenuItem("Timeline".Translate(), "tree", () => {
				PushViewController("TimelineViewController");
			}));
			menuItems.Add(new MenuItem("ProductsForYourChild".Translate(), "market", () => {
				PushViewController("DealsViewController");
			}));

			/************************************************************************
			menuItems.Add(new MenuItem("MyProfile".Translate(), "profile", () => {
			}));
			menuItems.Add(new MenuItem("InviteFriends".Translate(), "invite", () => {
			}));
			menuItems.Add(new MenuItem("ManageFamily".Translate(), "users", () => {
			}));
			menuItems.Add(new MenuItem("Configurations".Translate(), "gear", () => {
			}));
			menuItems.Add(new MenuItem("About".Translate(), "about", () => {
			}));
			menuItems.Add(new MenuItem("Exit".Translate(), "logoff", () => {
			}));
			************************************************************************/

			return menuItems;
		}

		/// <summary>
		/// Pushs the view controller.
		/// </summary>
		/// <param name="storyboardId">Storyboard identifier.</param>
		/// <param name="deselectRows">If set to <c>true</c> deselect rows.</param>
		public void PushViewController(string storyboardId, bool deselectRows = false)
		{
			if (CurrentStoryboardId == storyboardId)
			{
				var slideoutNavigation = (SlideoutNavigationController) RootViewController;
				slideoutNavigation.Hide(true);
			}
			else
			{
				ActionProgress actionProgress = new ActionProgress(() => {
					NavigationController.PushViewController((UIViewController) m_board.InstantiateViewController(storyboardId), false);
					if (deselectRows)
					{
						tblView.DeselectRow(tblView.IndexPathForSelectedRow, false);
					}
					CurrentStoryboardId = storyboardId;
				}, false);
				actionProgress.Execute();
			}
		}

		/// <summary>
		/// Gets or sets the current storyboard identifier.
		/// </summary>
		/// <value>The current storyboard identifier.</value>
		public string CurrentStoryboardId {
			get;
			set;
		}
	}
}