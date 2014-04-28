using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Domain;
using Domain.Baby;
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

			InitTimeline();
		}

		/// <summary>
		/// Inits the timeline.
		/// </summary>
		/// 
		void InitTimeline()
		{
			var momentService = new MomentService();
			var babyService = new BabyService();
			var moments = momentService.GetAllMoments();
			var baby = babyService.GetBaby();

			lblBabyName.Text = baby.Name;
			lblBabyAge.Text = baby.AgeInWords;

			tblView.Source = new TimelineViewSource(this, moments.ToList(), baby);
		}
	}
}