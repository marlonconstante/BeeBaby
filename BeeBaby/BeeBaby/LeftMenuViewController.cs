using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;
using Application;
using Domain.Baby;

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


			UIViewController dealsController = (UIViewController) board.InstantiateViewController("DealsViewController");

			Root.Add(new Section() {
				new StyledStringElement("Home", () => NavigationController.PushViewController(controller, true)),
				new StyledStringElement("About", () => {
					NavigationController.PushViewController(controller, true);
				}),
				new StyledStringElement("Deals".Translate(), () => {
					NavigationController.PushViewController(dealsController, true);
				}),
				new StyledStringElement("Full Screen", () => {
					NavigationController.PushViewController(controller, true);
				}),
				new StyledStringElement("Baby 1", () => {
					CurrentContext.Instance.CurrentBaby = new BabyService().GetBaby("1");
				}),
				new StyledStringElement("Baby 2", () => {
					CurrentContext.Instance.CurrentBaby = new BabyService().GetBaby("2");
				})

			});
		}
	}
}

