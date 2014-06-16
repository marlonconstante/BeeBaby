using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using Domain.Baby;
using Application;
using BeeBaby.ResourcesProviders;
using BeeBaby.Util;

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

			LoadBaby();
		}

		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			InitTimeline();
		}

		/// <summary>
		/// Loads the baby.
		/// </summary>
		void LoadBaby()
		{
			new ImageProvider().DeleteTemporaryFiles();

			CurrentContext.Instance.CurrentBaby = PreferencesEditor.LoadLastUsedBaby();
		}

		/// <summary>
		/// Inits the timeline.
		/// </summary>
		/// 
		void InitTimeline()
		{
			var baby = CurrentContext.Instance.CurrentBaby;
			if (baby == null)
			{
				OpenCamera();
			}
			else
			{
				var moments = new MomentService().GetAllMoments(baby);

				lblBabyName.Text = baby.Name;
				lblBabyAge.Text = baby.AgeInWords;

				tblView.Source = new TimelineViewSource(this, moments.ToList(), baby);
				tblView.ReloadData();
			}
		}
	}
}