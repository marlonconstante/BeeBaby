using System;
using MonoTouch.UIKit;
using BeeBaby.Navigations;

namespace BeeBaby.Util
{
	public abstract class Windows
	{
		private Windows()
		{
		}

		/// <summary>
		/// Tops the view controller.
		/// </summary>
		/// <returns>The view controller.</returns>
		/// <param name="window">Window.</param>
		public static UIViewController GetTopViewController(UIWindow window)
		{
			var topViewController = (window != null) ? window.RootViewController : null;

			if (topViewController != null)
			{
				while (true)
				{
					var presentedViewController = topViewController.PresentedViewController;
					if (presentedViewController == null || presentedViewController is UIImagePickerController)
					{
						break;
					}
					topViewController = presentedViewController;
				}

				if (topViewController is INavigationController)
				{
					topViewController = ((INavigationController) topViewController).GetCurrentViewController();
				}
				if (topViewController is UITabBarController)
				{
					topViewController = ((UITabBarController) topViewController).SelectedViewController;
				}
			}

			return topViewController;
		}

		/// <summary>
		/// Changes the root view controller.
		/// </summary>
		/// <param name="storyboardId">Storyboard identifier.</param>
		public static void ChangeRootViewController(string storyboardId)
		{
			UIView.AnimationsEnabled = false;

			var window = UIApplication.SharedApplication.Windows[0];
			var viewController = window.RootViewController;

			UIView.Transition(window, 0.75f, UIViewAnimationOptions.TransitionFlipFromLeft, () => {
				var board = UIStoryboard.FromName("MainStoryboard", null);
				window.RootViewController = board.InstantiateViewController(storyboardId) as UIViewController;

				UIView.AnimationsEnabled = true;
			}, () => {
				viewController.Dispose();
			});
		}
	}
}