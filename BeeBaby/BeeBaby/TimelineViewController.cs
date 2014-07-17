using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using Domain.Baby;
using Application;
using BeeBaby.ResourcesProviders;
using BeeBaby.Util;
using Domain.User;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public partial class TimelineViewController : NavigationViewController
	{
		static bool s_openCamera = true;
		TimelineViewSource m_tableSource;

		public TimelineViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			FlurryAnalytics.Flurry.LogEvent("Momento: Timeline.", true);

			CurrentContext.Instance.ReloadMoments = true;

			LoadUser();
			LoadBaby();
		}

		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			LoadEvents();

			if (s_openCamera)
			{
				OpenCamera();
				s_openCamera = false;
			}
			else
			{
				InitTimeline();
			}
		}

		/// <summary>
		/// Views the did disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidDisappear(bool animated)
		{
			FlurryAnalytics.Flurry.EndTimedEvent("Momento: Timeline.", null);
			base.ViewDidDisappear(animated);
		}

		/// <summary>
		/// Loads the user.
		/// </summary>
		void LoadUser()
		{
			CurrentContext.Instance.User = new UserService().LoadUser();
		}

		/// <summary>
		/// Loads the baby.
		/// </summary>
		void LoadBaby()
		{
			new ImageProvider().DeleteFiles(true);

			CurrentContext.Instance.CurrentBaby = PreferencesEditor.LoadLastUsedBaby();
		}

		/// <summary>
		/// Loads the events.
		/// </summary>
		void LoadEvents()
		{
			if (CurrentContext.Instance.AllEvents.Count == 0)
			{
				CurrentContext.Instance.AllEvents = new EventService().GetAllEvents().ToList();
			}
		}

		/// <summary>
		/// Inits the timeline.
		/// </summary>
		void InitTimeline()
		{
			if (CurrentContext.Instance.ReloadMoments)
			{
				var baby = CurrentContext.Instance.CurrentBaby;
				var moments = new MomentService().GetAllMoments(baby);

				lblBabyName.Text = baby.Name;
				lblBabyAge.Text = baby.AgeInWords;

				m_tableSource = new TimelineViewSource(this, moments.ToList(), baby);
				tblView.Source = m_tableSource;
				tblView.ReloadData();

				CurrentContext.Instance.ReloadMoments = false;
			}
		}

		/// <summary>
		/// Removes the row.
		/// </summary>
		/// <param name="cell">Cell.</param>
		public void RemoveRow(TimelineMomentCell cell)
		{
			//TODO: Internacionalizar mensagens
			var alertView = new UIAlertView("Confirmação de exclusão".Translate(), "Tem certeza que quer remover este momento?".Translate(), null, null, "Sim".Translate(), "Não".Translate());
			alertView.Clicked += (object sender, UIButtonEventArgs e) => {
				if (e.ButtonIndex == 0)
				{
			m_tableSource.RemoveRow(tblView, tblView.IndexPathForCell(cell));
			if (tblView.NumberOfRowsInSection(0) == 0)
			{
				OpenCamera();
			}
				}
			};
			alertView.Show();
		}
	}
}