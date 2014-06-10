using System;
using MonoTouch.UIKit;

namespace BeeBaby
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
			UIViewController topViewController = (window != null) ? window.RootViewController : null;

			if (topViewController != null)
			{
				while (topViewController.PresentedViewController != null)
				{
					topViewController = topViewController.PresentedViewController;
				}

				if (topViewController is INavigationController)
				{
					topViewController = ((INavigationController) topViewController).GetCurrentViewController();
				}
			}

			return topViewController;
		}
	}
}