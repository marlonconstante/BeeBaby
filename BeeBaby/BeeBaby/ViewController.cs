using System;
using MonoTouch.UIKit;
using BigTed;
using MonoTouch.Foundation;
using System.Drawing;
using PixateFreestyleLib;

namespace BeeBaby
{
	public class ViewController : UIViewController
	{
		public ViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			KeyboardNotification.Add(this);

			AddEditingTapGestureRecognizer();

			TranslateLabels();

			AddTitleView();
			AddLeftBarButtonItem();
			AddRightBarButtonItem();
		}

		/// <summary>
		/// Adds the title view.
		/// </summary>
		void AddTitleView()
		{
			UIImageView imageView = new UIImageView(new RectangleF(0f, 0f, 82f, 36f));
			imageView.SetStyleClass("bee-baby");
			NavigationItem.TitleView = imageView;
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

			UpdateStatusBar();
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			EndEditing();

			// Shows the spinner
			BTProgressHUD.Show();
		}

		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			// Dismiss the spinner
			BTProgressHUD.Dismiss();
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public virtual void TranslateLabels()
		{
		}

		/// <summary>
		/// Starts the editing.
		/// </summary>
		public virtual void StartEditing()
		{
		}

		/// <summary>
		/// Ends the editing.
		/// </summary>
		public virtual void EndEditing()
		{
			View.EndEditing(true);
		}

		/// <summary>
		/// Updates the status bar.
		/// </summary>
		void UpdateStatusBar()
		{
			var navigationBarHidden = IsNavigationBarHidden();
			UIApplication.SharedApplication.SetStatusBarHidden(navigationBarHidden, UIStatusBarAnimation.None);
			NavigationController.NavigationBarHidden = navigationBarHidden;
		}

		/// <summary>
		/// Determines whether this instance is navigation bar hidden.
		/// </summary>
		/// <returns><c>true</c> if this instance is navigation bar hidden; otherwise, <c>false</c>.</returns>
		public virtual bool IsNavigationBarHidden()
		{
			return false;
		}

		/// <summary>
		/// Determines whether this instance is keyboard animation.
		/// </summary>
		/// <returns><c>true</c> if this instance is keyboard animation; otherwise, <c>false</c>.</returns>
		public virtual bool IsKeyboardAnimation()
		{
			return false;
		}

		/// <summary>
		/// Shows the progress while performing.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="closeProgressWhenFinished">If set to <c>true</c> close progress when finished.</param>
		public void ShowProgressWhilePerforming(NSAction action, bool closeProgressWhenFinished = true)
		{
			ActionProgress actionProgress = new ActionProgress(action, closeProgressWhenFinished);
			actionProgress.Execute();
		}

		/// <summary>
		/// Adds the editing tap gesture recognizer.
		/// </summary>
		void AddEditingTapGestureRecognizer()
		{
			var gestureRecognizer = new UITapGestureRecognizer(() => EndEditing());
			gestureRecognizer.Delegate = new GestureRecognizerDelegate();
			gestureRecognizer.CancelsTouchesInView = false;
			View.AddGestureRecognizer(gestureRecognizer);
		}
	}
}