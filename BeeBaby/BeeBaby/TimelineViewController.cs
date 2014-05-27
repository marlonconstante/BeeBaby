using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using Domain.Baby;
using Application;
using BeeBaby.ResourcesProviders;

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
			new ImageProvider().DeleteTemporaryFiles();

			var baby = CurrentContext.Instance.CurrentBaby;
			var moments = new MomentService().GetAllMoments(baby);

			lblBabyName.Text = baby.Name;
			lblBabyAge.Text = baby.AgeInWords;

			tblView.Source = new TimelineViewSource(this, moments.ToList(), baby);
		}
	}
}