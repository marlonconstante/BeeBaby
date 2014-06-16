﻿using System;
using MonoTouch.UIKit;
using BigTed;
using MonoTouch.Foundation;
using System.Drawing;
using System.Collections.Generic;

namespace BeeBaby
{
	public abstract class BaseViewController : UIViewController
	{
		public BaseViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			AddEditingTapGestureRecognizer();

			TranslateLabels();
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
		/// Gets the supported orientation views.
		/// </summary>
		/// <returns>The supported orientation views.</returns>
		public virtual IEnumerable<UIView> GetSupportedOrientationViews()
		{
			return new UIView[] { };
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
		/// Determines whether this instance is show status bar.
		/// </summary>
		/// <returns><c>true</c> if this instance is show status bar; otherwise, <c>false</c>.</returns>
		public virtual bool IsShowStatusBar()
		{
			return true;
		}

		/// <summary>
		/// Determines whether this instance is contains menu.
		/// </summary>
		/// <returns><c>true</c> if this instance is contains menu; otherwise, <c>false</c>.</returns>
		public bool IsContainsMenu()
		{
			var viewController = RootViewController;
			return viewController.GetType() == typeof(SlideoutNavigationController) && viewController.PresentedViewController == null;
		}

		/// <summary>
		/// Gets or sets the root view controller.
		/// </summary>
		/// <value>The root view controller.</value>
		public UIViewController RootViewController {
			get {
				return UIApplication.SharedApplication.Windows[0].RootViewController;
			}
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

		/// <summary>
		/// Updates the status bar.
		/// </summary>
		void UpdateStatusBar()
		{
			UIApplication.SharedApplication.SetStatusBarHidden(!IsShowStatusBar(), UIStatusBarAnimation.None);
		}

		/// <summary>
		/// Dispose the specified disposing.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Discard.ReleaseSubviews(View);
				Discard.ReleaseOutlets(this);
				Discard.ReleaseProperties(this);
				Discard.ReleaseFields(this);
			}

			base.Dispose(disposing);
		}
	}
}