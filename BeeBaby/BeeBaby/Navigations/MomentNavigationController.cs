using System;
using MonoTouch.UIKit;
using Application;
using BeeBaby.ResourcesProviders;
using Domain.Moment;
using Infrastructure.Systems;
using BeeBaby.Util;
using BeeBaby.Controllers;
using BeeBaby.Backup;
using Domain.Synchronization;

namespace BeeBaby.Navigations
{
	public class MomentNavigationController : UINavigationController, INavigationController
	{
		public MomentNavigationController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Gets the supported interface orientations.
		/// </summary>
		/// <returns>The supported interface orientations.</returns>
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.Portrait;
		}

		/// <summary>
		/// Gets the current view controller.
		/// </summary>
		/// <returns>The current view controller.</returns>
		public UIViewController GetCurrentViewController()
		{
			return TopViewController;
		}

		/// <summary>
		/// Creates the moment.
		/// </summary>
		public void CreateMoment()
		{
			new ImageProvider().DeleteFiles(true);

			CurrentContext.Instance.Moment = new MomentService().CreateMoment();
		}

		/// <summary>
		/// Saves the current moment.
		/// </summary>
		public void SaveCurrentMoment()
		{
			var moment = CurrentContext.Instance.Moment;
			if (!moment.IsTemplate())
			{
				moment.Location = new LocationService().SaveLocation(moment.Location);

				new MomentService().SaveMoment(moment);
				RemoteDataSystem.SendMomentData(moment);
			}
			var filePaths = new ImageProvider(moment.Id).SavePermanentImages(moment.SelectedMediaNames);
			if (!moment.IsTemplate())
			{
				var momentBackup = new MomentBackup(moment);
				momentBackup.Save();

				filePaths.Add(momentBackup.RelativeFilePath);

				new FileUploadService().InsertFilePaths(filePaths);
			}
			CurrentContext.Instance.ReloadMoments = true;
			Close();
		}

		/// <summary>
		/// Close navigation.
		/// </summary>
		public virtual void Close()
		{
			CurrentContext.Instance.Moment.SelectedMediaNames.Clear();
			CurrentContext.Instance.Moment = null;
			CurrentContext.Instance.SelectedEvent = null;

			PresentingViewController.DismissViewController(true, () => {
				Discard.ReleaseNavigation(this);
			});

			new ImageProvider().DeleteFiles(true);
		}
	}
}