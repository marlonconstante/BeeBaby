using System;
using MonoTouch.UIKit;
using BigTed;
using MonoTouch.Foundation;
using System.Drawing;
using PixateFreestyleLib;

namespace BeeBaby
{
	public abstract class NavigationViewController : BaseViewController
	{
		UIView m_titleView;
		NavigationButtonItem m_leftBarButtonItem;
		NavigationButtonItem m_rightBarButtonItem;

		public NavigationViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			AddTitleView();
			AddLeftBarButtonItem();
			AddRightBarButtonItem();
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			UpdateNavigationBar();
		}

		/// <summary>
		/// Determines whether this instance is add left bar button item.
		/// </summary>
		/// <returns><c>true</c> if this instance is add left bar button item; otherwise, <c>false</c>.</returns>
		public virtual bool IsAddLeftBarButtonItem()
		{
			return IsContainsMenu() || GetType() != NavigationController.ViewControllers[0].GetType();
		}

		/// <summary>
		/// Lefts the bar button frame.
		/// </summary>
		/// <returns>The bar button frame.</returns>
		public virtual RectangleF LeftBarButtonFrame()
		{
			if (IsContainsMenu())
			{
				return new RectangleF(0f, 0f, 24f, 24f);
			}
			else
			{
				return new RectangleF(0f, 0f, 18f, 24f);
			}
		}

		/// <summary>
		/// Lefts the bar button action.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="ev">Event.</param>
		public virtual void LeftBarButtonAction(object sender, EventArgs ev)
		{
			if (IsContainsMenu())
			{
				var slideoutNavigation = (SlideoutNavigationController) RootViewController;
				slideoutNavigation.ShowMenuLeft();
			}
			else
			{
				ShowProgressWhilePerforming(() => {
					NavigationController.PopViewControllerAnimated(true);
				}, false);
			}
		}

		/// <summary>
		/// Lefts the bar button style class.
		/// </summary>
		/// <returns>The bar button style class.</returns>
		public virtual string LeftBarButtonStyleClass()
		{
			if (IsContainsMenu())
			{
				return "menu";
			}
			else
			{
				return "comeback";
			}
		}

		/// <summary>
		/// Determines whether this instance is add right bar button item.
		/// </summary>
		/// <returns><c>true</c> if this instance is add right bar button item; otherwise, <c>false</c>.</returns>
		public virtual bool IsAddRightBarButtonItem()
		{
			return true;
		}

		/// <summary>
		/// Rights the bar button frame.
		/// </summary>
		/// <returns>The bar button frame.</returns>
		public virtual RectangleF RightBarButtonFrame()
		{
			return new RectangleF(0f, 0f, 34f, 34f);
		}

		/// <summary>
		/// Rights the bar button action.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="ev">Event.</param>
		public virtual void RightBarButtonAction(object sender, EventArgs ev)
		{
			ShowProgressWhilePerforming(() => {
				if (IsContainsMenu())
				{
					RootViewController.PerformSegue("segueCamera", NavigationItem.RightBarButtonItem);
					DismissViewController(true, null);
				}
				else
				{
					NavigationController.PopToRootViewController(true);
				}
			}, false);
		}

		/// <summary>
		/// Rights the bar button style class.
		/// </summary>
		/// <returns>The bar button style class.</returns>
		public virtual string RightBarButtonStyleClass()
		{
			return "camera";
		}

		/// <summary>
		/// Determines whether this instance is translucent navigation bar.
		/// </summary>
		/// <returns><c>true</c> if this instance is translucent navigation bar; otherwise, <c>false</c>.</returns>
		public virtual bool IsTranslucentNavigationBar()
		{
			return true;
		}

		/// <summary>
		/// Updates the navigation bar.
		/// </summary>
		void UpdateNavigationBar()
		{
			NavigationController.NavigationBarHidden = !IsShowStatusBar();
			NavigationController.NavigationBar.Translucent = IsTranslucentNavigationBar();
		}

		/// <summary>
		/// Adds the title view.
		/// </summary>
		void AddTitleView()
		{
			if (TitleScreen != null)
			{
				UILabel label = new UILabel();
				label.Text = TitleScreen;
				label.SizeToFit();
				label.SetStyleClass("title-label");
				m_titleView = label;
			}
			else
			{
				m_titleView = new UIView(new RectangleF(0f, 0f, 82f, 36f));
				m_titleView.SetStyleClass("bee-baby");
			}
			NavigationItem.TitleView = m_titleView;
		}

		/// <summary>
		/// Adds the left bar button item.
		/// </summary>
		void AddLeftBarButtonItem()
		{
			if (IsAddLeftBarButtonItem())
			{
				m_leftBarButtonItem =
					new NavigationButtonItem(LeftBarButtonFrame()
					, -6f
					, LeftBarButtonStyleClass());
				ControlEvents.Add(new ControlEvent(m_leftBarButtonItem.Button, LeftBarButtonAction, ControlEventType.TouchUpInside));
			}
			NavigationItem.SetLeftBarButtonItem(m_leftBarButtonItem, true);
		}

		/// <summary>
		/// Adds the right bar button item.
		/// </summary>
		void AddRightBarButtonItem()
		{
			if (IsAddRightBarButtonItem())
			{
				m_rightBarButtonItem =
					new NavigationButtonItem(RightBarButtonFrame()
					, 6f
					, RightBarButtonStyleClass());
				ControlEvents.Add(new ControlEvent(m_rightBarButtonItem.Button, RightBarButtonAction, ControlEventType.TouchUpInside));
			}
			NavigationItem.SetRightBarButtonItem(m_rightBarButtonItem, true);
		}

		/// <summary>
		/// Gets or sets the title screen.
		/// </summary>
		/// <value>The title screen.</value>
		public string TitleScreen {
			get;
			set;
		}
	}
}