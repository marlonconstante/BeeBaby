using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Domain;
using BigTed;
using Domain.Baby;
using FlyoutNavigation;
using MonoTouch.Dialog;

namespace BeeBaby
{
	public partial class TimelineViewController : ViewController
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

			initTimeline();

			BTProgressHUD.Dismiss();
		}

		/// <summary>
		/// Inits the timeline.
		/// </summary>
		/// 
		private void initTimeline()
		{
			var momentService = new MomentService();
			var moments = momentService.GetAllMoments();

			var baby = new BabyService().GetBaby();

			lblBabyName.Text = baby.Name;
			lblBabyAge.Text = baby.Age;

			tblView.Source = new TimelineViewSource(this, moments.ToList(), baby);
		}
	}
}