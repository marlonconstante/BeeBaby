using System;
using MonoTouch.Foundation;
using Domain.Baby;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;
using Domain.Moment;
using Application;
using BeeBaby.ResourcesProviders;
using System.IO;
using BeeBaby.Progress;
using Domain.Media;

namespace BeeBaby.Util
{
	/// <summary>
	/// Release control.
	/// </summary>
	public static class ReleaseControl
	{
		/// <summary>
		/// Checks for updates.
		/// </summary>
		public static void CheckForUpdates()
		{
			new ActionProgress(() => {
			RunActions(1.1f, "MediaCount", UpdateMediaCount);
			RunActions(1.3f, "VersionAlert", ShowNewVersionAlert);
			RunActions(1.5f, "SizePictures", UpdateSizePictures);
			}, false).Execute("Wait".Translate());
		}

		/// <summary>
		/// Runs the actions.
		/// </summary>
		/// <param name="version">Version.</param>
		/// <param name="key">The Key associated with the version.</param>
		/// <param name="actions">Actions to run.</param>
		static void RunActions(float version, string key, params Action[] actions)
		{
			var latestVersion = UserDefaults.FloatForKey(key);
			if (latestVersion < version)
			{
				foreach (var action in actions)
				{
					action();
				}
				UserDefaults.SetFloat(version, key);
				UserDefaults.Synchronize();
			}
		}

		/// <summary>
		/// Updates the media count.
		/// </summary>
		static void UpdateMediaCount()
		{
			var momentService = new MomentService();
			foreach (var moment in momentService.FindAllMoments())
			{
					moment.MediaCount = new ImageProvider(moment.Id).GetFileNames().Count;
					momentService.SaveMoment(moment);
			}
		}

		/// <summary>
		/// Updates the size pictures.
		/// </summary>
		static void UpdateSizePictures()
		{
			foreach (var moment in new MomentService().FindAllMoments())
			{
				var imageProvider = new ImageProvider(moment.Id);
				foreach (var fileName in imageProvider.GetFileNames())
				{
					using (var image = imageProvider.LoadImage(fileName))
					{
						var size = image.Size;
						var maxSizeInPixels = Math.Max(size.Width, size.Height) * image.CurrentScale;
						if (maxSizeInPixels > MediaBase.FullScreenImageMaxSizeInPixels)
						{
							imageProvider.SavePermanentImageOnApp(image, Path.GetFileNameWithoutExtension(fileName), false); 
						}
					}
				}
			}
		}

		/// <summary>
		/// Shows the new version alert.
		/// </summary>
		static void ShowNewVersionAlert()
		{
			//new UIAlertView("WhatsNew".Translate(), "Version-1.3-ChangeLog".Translate(), null, "GotIt".Translate(), null).Show();
		}

		/// <summary>
		/// Gets the user defaults.
		/// </summary>
		/// <value>The user defaults.</value>
		static NSUserDefaults UserDefaults
		{
			get
			{
				return NSUserDefaults.StandardUserDefaults;
			}
		}
	}
}