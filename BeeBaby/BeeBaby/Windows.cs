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
			}

			return topViewController;
		}
	}
}