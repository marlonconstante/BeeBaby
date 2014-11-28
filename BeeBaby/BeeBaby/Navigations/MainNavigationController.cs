using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BeeBaby.Navigations
{
	public partial class MainNavigationController : UINavigationController, INavigationController
	{
		public MainNavigationController (IntPtr handle) : base (handle)
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
	}
}