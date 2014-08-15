using System;
using MonoTouch.Foundation;
using Domain.Baby;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;
using Domain.Moment;
using Application;
using BeeBaby.ResourcesProviders;

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
			RunActions(1.1f, "MediaCount", UpdateMediaCount);
			RunActions(1.2f, "VersionAlert", ShowNewVersionAlert);
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
				if (!CurrentContext.Instance.Moment.Equals(moment))
				{
					moment.MediaCount = new ImageProvider(moment.Id).GetFileNames(false).Count;
					momentService.SaveMoment(moment);
				}
			}
		}

		/// <summary>
		/// Shows the new version alert.
		/// </summary>
		static void ShowNewVersionAlert()
		{
			new UIAlertView("WhatsNew".Translate(), "Version-1.2-ChangeLog".Translate(), null, "GotIt".Translate(), null).Show();
		}

		/// <summary>
		/// Gets the user defaults.
		/// </summary>
		/// <value>The user defaults.</value>
		static NSUserDefaults UserDefaults {
			get {
				return NSUserDefaults.StandardUserDefaults;
			}
		}
	}
}