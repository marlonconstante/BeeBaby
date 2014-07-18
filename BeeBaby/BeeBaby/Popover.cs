using System;
using WEPopover;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public class Popover : WEPopoverController
	{
		public Popover(UIView view)
		{
			ContentViewController = new UIViewController() {
				View = view
			};
			ContentSize = view.Frame.Size;
			Properties = DefaultProperties();
		}

		/// <summary>
		/// Show the specified rectangle and view.
		/// </summary>
		/// <param name="rectangle">Rectangle.</param>
		/// <param name="view">View.</param>
		public void Show(RectangleF rectangle, UIView view)
		{
			base.PresentFromRect(rectangle, view, UIPopoverArrowDirection.Up, true);
			if (ContentSize.Height != ContentViewController.View.Frame.Height)
			{
				base.PresentFromRect(rectangle, view, UIPopoverArrowDirection.Down, true);
			}
		}

		/// <summary>
		/// Defaults the properties.
		/// </summary>
		/// <returns>The properties.</returns>
		WEPopoverContainerViewProperties DefaultProperties()
		{
			return new WEPopoverContainerViewProperties {
				LeftBackgroundMargin = 1f,
				RightBackgroundMargin = 1f,
				TopBackgroundMargin = 1f,
				BottomBackgroundMargin = 1f,
				LeftContentMargin = 0f,
				RightContentMargin = 0f,
				TopContentMargin = 0f,
				BottomContentMargin = 0f,
				ArrowMargin = 0f
			};	
		}
	}
}