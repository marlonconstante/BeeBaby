using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Domain;
using Domain.Baby;
using MonoTouch.Dialog;
using Application;
using System.Drawing;
using MonoTouch.SlideoutNavigation;

namespace BeeBaby
{
	public partial class TimelineViewController : NavigationViewController
	{
		public TimelineViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			InitTimeline();
		}

		/// <summary>
		/// Inits the timeline.
		/// </summary>
		/// 
		void InitTimeline()
		{
			var baby = CurrentContext.Instance.CurrentBaby;
			var moments = new MomentService().GetAllMoments(baby);

			lblBabyName.Text = baby.Name;
			lblBabyAge.Text = baby.AgeInWords;

			tblView.Source = new TimelineViewSource(this, moments.ToList(), baby);
		}

		/// <summary>
		/// Determines whether this instance is add left bar button item.
		/// </summary>
		/// <returns><c>true</c> if this instance is add left bar button item; otherwise, <c>false</c>.</returns>
		public override bool IsAddLeftBarButtonItem()
		{
			return true;
		}

		/// <summary>
		/// Lefts the bar button frame.
		/// </summary>
		/// <returns>The bar button frame.</returns>
		public override RectangleF LeftBarButtonFrame()
		{
			return new RectangleF(0f, 0f, 24f, 24f);
		}

		/// <summary>
		/// Lefts the bar button action.
		/// </summary>
		public override void LeftBarButtonAction()
		{
			var menu = (SlideoutNavigationController) UIApplication.SharedApplication.Windows[0].RootViewController;
			menu.ShowMenuLeft();
		}

		/// <summary>
		/// Lefts the bar button style class.
		/// </summary>
		/// <returns>The bar button style class.</returns>
		public override string LeftBarButtonStyleClass()
		{
			return "menu";
		}

		/// <summary>
		/// Rights the bar button action.
		/// </summary>
		public override void RightBarButtonAction()
		{
			ShowProgressWhilePerforming(() => {
				PerformSegue("segueCamera", NavigationItem.RightBarButtonItem);
				DismissViewController(true, null);
			}, false);
		}
	}
}