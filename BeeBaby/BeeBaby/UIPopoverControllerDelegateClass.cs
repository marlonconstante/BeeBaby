using System;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public class UIPopoverControllerDelegateClass : UIPopoverControllerDelegate
	{
		private UIViewController _viewController;

		public UIPopoverControllerDelegateClass (UIViewController viewController)
		{
			_viewController = viewController;
		}
	}
}

