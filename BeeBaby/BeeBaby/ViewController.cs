using System;
using MonoTouch.UIKit;
using BigTed;
using MonoTouch.Foundation;

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

			NavigationController.NavigationBarHidden = IsNavigationBarHidden();
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

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
		/// Determines whether this instance is navigation bar hidden.
		/// </summary>
		/// <returns><c>true</c> if this instance is navigation bar hidden; otherwise, <c>false</c>.</returns>
		public virtual bool IsNavigationBarHidden()
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
			var gestureRecognizer = new UITapGestureRecognizer(() => View.EndEditing(true));
			gestureRecognizer.CancelsTouchesInView = false;
			View.AddGestureRecognizer(gestureRecognizer);
		}
	}
}