using System;
using MonoTouch.UIKit;
using BigTed;
using MonoTouch.Foundation;
using System.Drawing;
using PixateFreestyleLib;
using BeeBaby.Proxy;
using BeeBaby.Navigations;
using BeeBaby.VisualElements;

namespace BeeBaby.Controllers
{
	public abstract class NavigationViewController : BaseViewController
	{
		protected UIView TitleView { get; set; }
		protected NavigationButtonItem LeftBarButtonItem { get; set; }
		protected NavigationButtonItem RightBarButtonItem { get; set; }

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

			NavigationController.View.AddStyleClass("navigation");
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
			return !IsContainsTabs() && GetType() != NavigationController.ViewControllers[0].GetType();
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
			ShowProgressWhilePerforming(() => {
				NavigationController.PopViewControllerAnimated(true);
			}, false);
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
			return !IsContainsTabs();
		}

		/// <summary>
		/// Rights the bar button frame.
		/// </summary>
		/// <returns>The bar button frame.</returns>
		public virtual RectangleF RightBarButtonFrame()
		{
			if (IsCameraFlow())
			{
				return new RectangleF(0f, 0f, 34f, 34f);
			}
			else
			{
				return new RectangleF(0f, 0f, 24f, 24f);
			}
		}

		/// <summary>
		/// Rights the bar button action.
		/// </summary>
		public virtual void RightBarButtonAction()
		{
			if (IsCameraFlow())
			{
				OpenCamera();
			}
			else
			{
				ShowProgressWhilePerforming(() => {
					((MomentNavigationController) NavigationController).Close();
				}, false);
			}
		}

		/// <summary>
		/// Rights the bar button style class.
		/// </summary>
		/// <returns>The bar button style class.</returns>
		public virtual string RightBarButtonStyleClass()
		{
			return IsCameraFlow() ? "camera" : "cancel";
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
		/// Determines whether this instance is camera flow.
		/// </summary>
		/// <returns><c>true</c> if this instance is camera flow; otherwise, <c>false</c>.</returns>
		public bool IsCameraFlow()
		{
			return !IsMediaController() && !IsMediaFlow() && !IsEditFlow();
		}

		/// <summary>
		/// Determines whether this instance is media flow.
		/// </summary>
		/// <returns><c>true</c> if this instance is media flow; otherwise, <c>false</c>.</returns>
		public bool IsMediaFlow()
		{
			return NavigationController is MediaNavigationController;
		}

		/// <summary>
		/// Determines whether this instance is edit flow.
		/// </summary>
		/// <returns><c>true</c> if this instance is edit flow; otherwise, <c>false</c>.</returns>
		public bool IsEditFlow()
		{
			return NavigationController is EditMomentNavigationController;
		}

		/// <summary>
		/// Opens the camera.
		/// </summary>
		public void OpenCamera()
		{
			ShowProgressWhilePerforming(() => {
				if (IsContainsTabs())
				{
					RootViewController.PerformSegue("segueCamera", NavigationItem.RightBarButtonItem);
				}
				else
				{
					NavigationController.PopToRootViewController(true);
				}
			}, false);
		}

		/// <summary>
		/// Determines whether this instance is media controller.
		/// </summary>
		/// <returns><c>true</c> if this instance is media controller; otherwise, <c>false</c>.</returns>
		bool IsMediaController()
		{
			return this is MediaViewController;
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
				TitleView = label;
			}
			else
			{
				TitleView = new UIView(new RectangleF(0f, 0f, 82f, 36f));
				TitleView.SetStyleClass("bee-baby");
			}
			NavigationItem.TitleView = TitleView;
		}

		/// <summary>
		/// Adds the left bar button item.
		/// </summary>
		void AddLeftBarButtonItem()
		{
			if (IsAddLeftBarButtonItem())
			{
				LeftBarButtonItem =
					new NavigationButtonItem(LeftBarButtonFrame()
					, -6f
					, LeftBarButtonStyleClass());

				var proxy = new EventProxy<NavigationViewController, EventArgs>(this);
				proxy.Action = (target, sender, args) => {
					target.LeftBarButtonAction();
				};
				LeftBarButtonItem.Button.TouchUpInside += proxy.HandleEvent;
			}
			NavigationItem.SetLeftBarButtonItem(LeftBarButtonItem, true);
		}

		/// <summary>
		/// Adds the right bar button item.
		/// </summary>
		void AddRightBarButtonItem()
		{
			if (IsAddRightBarButtonItem())
			{
				RightBarButtonItem =
					new NavigationButtonItem(RightBarButtonFrame()
					, 6f
					, RightBarButtonStyleClass());

				var proxy = new EventProxy<NavigationViewController, EventArgs>(this);
				proxy.Action = (target, sender, args) => {
					target.RightBarButtonAction();
				};
				RightBarButtonItem.Button.TouchUpInside += proxy.HandleEvent;
			}
			NavigationItem.SetRightBarButtonItem(RightBarButtonItem, true);
		}

		/// <summary>
		/// Gets the navigation item.
		/// </summary>
		/// <value>The navigation item.</value>
		public UINavigationItem NavigationItem {
			get {
				return IsContainsTabs() ? TabBarController.NavigationItem : base.NavigationItem;
			}
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