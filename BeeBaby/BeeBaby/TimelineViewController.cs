using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Domain;
using BigTed;

namespace BeeBaby
{
	public partial class TimelineViewController : UIViewController
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
			BTProgressHUD.Dismiss(); //shows the spinner
		}

		/// <summary>
		/// Inits the timeline.
		/// </summary>
		private void initTimeline()
		{
			var momentService = new MomentService();
			var moments = momentService.GetAllMoments();

			List<IAggregateRoot> items = new List<IAggregateRoot>();
			items.AddRange(moments.ToList());
			items.Add(new Event());

			tblView.Source = new TimelineViewSource(this, items);
		}
	}
}