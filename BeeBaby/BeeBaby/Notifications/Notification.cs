using System;
using UIKit;
using BeeBaby.Util;

namespace BeeBaby.Notifications
{
	public abstract class Notification
	{
		protected Notification()
		{
		}

		/// <summary>
		/// Gets the current view controller.
		/// </summary>
		/// <value>The current view controller.</value>
		public UIViewController CurrentViewController {
			get {
				return Windows.GetTopViewController(UIApplication.SharedApplication.Windows[0]);
			}
		}
	}
}