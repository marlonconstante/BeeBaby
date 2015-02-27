using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BeeBaby.Util;

namespace BeeBaby.Navigations
{
	public partial class ConfigNavigationController : UINavigationController, INavigationController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Navigations.ConfigNavigationController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public ConfigNavigationController (IntPtr handle) : base (handle)
		{
		}

		/// <summary>
		/// Gets the supported interface orientations.
		/// </summary>
		/// <returns>The supported interface orientations.</returns>
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.Portrait;
		}

		/// <summary>
		/// Gets the current view controller.
		/// </summary>
		/// <returns>The current view controller.</returns>
		public UIViewController GetCurrentViewController()
		{
			return TopViewController;
		}

		/// <summary>
		/// Close this instance.
		/// </summary>
		public void Close()
		{
			PresentingViewController.DismissViewController(true, () => {
				Discard.ReleaseNavigation(this);
			});
		}
	}
}