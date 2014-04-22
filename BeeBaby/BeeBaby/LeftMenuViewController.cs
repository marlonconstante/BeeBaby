using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public class LeftMenuViewController : DialogViewController
	{
		public LeftMenuViewController() 
			: base(UITableViewStyle.Plain, new RootElement(""))
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			UIStoryboard board = UIStoryboard.FromName("MainStoryboard", null);
			UIViewController controller = (UIViewController) board.InstantiateViewController("TimelineViewController");

			Root.Add(new Section() {
				new StyledStringElement("Home", () => NavigationController.PushViewController(controller, true)),
				new StyledStringElement("About", () => {
					NavigationController.PushViewController(controller, true);
				}),
				new StyledStringElement("Stuff", () => {
					NavigationController.PushViewController(controller, true);
				}),
				new StyledStringElement("Full Screen", () => {
					NavigationController.PushViewController(controller, true);
				})
			});
		}
	}
}

