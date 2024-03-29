using System;
using UIKit;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using Application;
using Domain.Baby;
using BeeBaby.Util;
using CoreGraphics;
using System.Collections.Generic;
using Foundation;
using Domain.Moment;
using Infrastructure.Systems;
using BeeBaby.ViewModels;
using BeeBaby.Progress;
using BeeBaby.Navigations;
using BeeBaby.Controllers;
using BeeBaby.VisualElements;
using Parse;

namespace BeeBaby.Menu
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

			m_profileView = new ProfileView(new CGRect(0f, 0f, 250f, 195f));
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
//			menuItems.Add(new MenuItem("InviteFriends".Translate(), "invite", () => {
//				PushViewController("InviteFriendsViewController");
//			}));
			menuItems.Add(new MenuItem("Exit".Translate(), "logoff", () => {
				var actionProgress = new ActionProgress(() => {
					ParseUser.LogOut();

					Windows.ChangeRootViewController("LoginViewController");
				}, false);
				actionProgress.Execute();
			}));

			/************************************************************************
			menuItems.Add(new MenuItem("MyProfile".Translate(), "profile", () => {
			}));
			menuItems.Add(new MenuItem("ManageFamily".Translate(), "users", () => {
			}));
			menuItems.Add(new MenuItem("Configurations".Translate(), "gear", () => {
			}));
			menuItems.Add(new MenuItem("About".Translate(), "about", () => {
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
				var actionProgress = new ActionProgress(() => {
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