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
			var momentService = new MomentService();
			var baby = CurrentContext.Instance.CurrentBaby;
			var moments = momentService.GetAllMoments(baby);

			lblBabyName.Text = baby.Name;
			lblBabyAge.Text = baby.AgeInWords;

			tblView.Source = new TimelineViewSource(this, moments.ToList(), baby);
		}

		/// <summary>
		/// Rights the bar button action.
		/// </summary>
		public override void RightBarButtonAction()
		{
			ShowProgressWhilePerforming(() => {
				PerformSegue("segueCamera", NavigationItem.RightBarButtonItem);
			}, false);
		}
	}
}