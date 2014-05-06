﻿using System;
using MonoTouch.UIKit;
using BigTed;
using MonoTouch.Foundation;
using System.Drawing;
using PixateFreestyleLib;

namespace BeeBaby
{
	public class NavigationViewController : BaseViewController
	{
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
		/// Determines whether this instance is add left bar button item.
		/// </summary>
		/// <returns><c>true</c> if this instance is add left bar button item; otherwise, <c>false</c>.</returns>
		public virtual bool IsAddLeftBarButtonItem()
		{
			return GetType() != NavigationController.ViewControllers[0].GetType();
		}

		/// <summary>
		/// Lefts the bar button frame.
		/// </summary>
		/// <returns>The bar button frame.</returns>
		public virtual RectangleF LeftBarButtonFrame()
		{
			return new RectangleF(0f, 0f, 18f, 24f);
		}

		/// <summary>
		/// Lefts the bar button action.
		/// </summary>
		public virtual void LeftBarButtonAction()
		{
			NavigationController.PopViewControllerAnimated(true);
		}

		/// <summary>
		/// Lefts the bar button style class.
		/// </summary>
		/// <returns>The bar button style class.</returns>
		public virtual string LeftBarButtonStyleClass()
		{
			return "comeback";
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
		public virtual void RightBarButtonAction()
		{
			NavigationController.PopToRootViewController(true);
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
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			UpdateNavigationBar();
		}

		/// <summary>
		/// Updates the navigation bar.
		/// </summary>
		void UpdateNavigationBar()
		{
			NavigationController.NavigationBarHidden = !IsShowStatusBar();
		}

		/// <summary>
		/// Adds the title view.
		/// </summary>
		void AddTitleView()
		{
			UIView titleView;
			if (TitleScreen != null)
			{
				UILabel label = new UILabel();
				label.Text = TitleScreen;
				label.SizeToFit();
				label.SetStyleClass("title-label");
				titleView = label;
			}
			else
			{
				titleView = new UIView(new RectangleF(0f, 0f, 82f, 36f));
				titleView.SetStyleClass("bee-baby");
			}
			NavigationItem.TitleView = titleView;
		}

		/// <summary>
		/// Adds the left bar button item.
		/// </summary>
		void AddLeftBarButtonItem()
		{
			NavigationButtonItem navigationButtonItem = null;
			if (IsAddLeftBarButtonItem())
			{
				navigationButtonItem =
					new NavigationButtonItem(LeftBarButtonFrame()
						, -6f
						, (sender, args) => {
					LeftBarButtonAction();
				}, LeftBarButtonStyleClass());
			}
			NavigationItem.SetLeftBarButtonItem(navigationButtonItem, true);
		}

		/// <summary>
		/// Adds the right bar button item.
		/// </summary>
		void AddRightBarButtonItem()
		{
			NavigationButtonItem navigationButtonItem = null;
			if (IsAddRightBarButtonItem())
			{
				navigationButtonItem =
					new NavigationButtonItem(RightBarButtonFrame()
						, 6f
						, (sender, args) => {
					RightBarButtonAction();
				}, RightBarButtonStyleClass());
			}
			NavigationItem.SetRightBarButtonItem(navigationButtonItem, true);
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