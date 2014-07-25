using System;
using MonoTouch.UIKit;
using Application;

namespace BeeBaby
{
	public class MomentNavigationController : UINavigationController, INavigationController
	{
		public MomentNavigationController(IntPtr handle) : base(handle)
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
		/// Close navigation.
		/// </summary>
		public void Close()
		{
			CurrentContext.Instance.Moment = null;
			CurrentContext.Instance.SelectedEvent = null;

			PresentingViewController.DismissViewController(false, null);
			Discard.ReleaseNavigation(this);
		}
	}
}