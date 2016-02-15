using System;
using Foundation;
using Domain.Baby;
using UIKit;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using Domain.Moment;
using Application;
using BeeBaby.ResourcesProviders;
using System.IO;
using BeeBaby.Progress;
using Domain.Media;
using BeeBaby.Backup;
using System.Collections.Generic;
using Domain.Synchronization;

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
				RunActions(1.5f, "BackupMomentAndChangeSizePictures", BackupMomentAndChangeSizePictures);
				RunActions(1.5f, "DeleteThumbnailPhotoProfile", DeleteThumbnailPhotoProfile);
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
		/// Backups the moment and change size pictures.
		/// </summary>
		static void BackupMomentAndChangeSizePictures()
		{
			var filePaths = new List<string>();
			foreach (var moment in new MomentService().FindAllMoments())
			{
				var imageProvider = new ImageProvider(moment.Id);
				foreach (var filePath in imageProvider.GetFileNames())
				{
					using (var image = imageProvider.LoadImage(filePath))
					{
						var size = image.Size;
						var maxSizeInPixels = Math.Max(size.Width, size.Height) * image.CurrentScale;
						if (maxSizeInPixels > MediaBase.FullScreenImageMaxSizeInPixels)
						{
							imageProvider.SavePermanentImageOnApp(image, Path.GetFileNameWithoutExtension(filePath), false); 
						}
					}

					var imageName = Path.GetFileName(filePath);
					var thumbnailImageName = imageProvider.GetThumbnailImageName(imageName);

					filePaths.Add(imageProvider.GetRelativeFilePath(thumbnailImageName));
					filePaths.Add(imageProvider.GetRelativeFilePath(imageName));
				}

				var momentBackup = new MomentBackup(moment);
				momentBackup.Save();

				filePaths.Add(momentBackup.RelativeFilePath);
			}
			new FileUploadService().InsertFilePaths(filePaths);
		}

		/// <summary>
		/// Deletes the thumbnail photo profile.
		/// </summary>
		static void DeleteThumbnailPhotoProfile()
		{
			var baby = CurrentContext.Instance.CurrentBaby;
			if (baby != null)
			{
				var imageProvider = new ImageProvider(baby.Id);

				var imageName = imageProvider.GetImageNameWithExtension(MediaBase.PhotoProfileName);
				var thumbnailImageName = imageProvider.GetThumbnailImageName(imageName);

				imageProvider.DeletePermanentFile(thumbnailImageName);

				if (FileHandle.IsValid(Path.Combine(FileHandle.RootFolderPath, baby.Id, imageName)))
				{
					new FileUploadService().InsertFilePaths(new string[] { imageProvider.GetRelativeFilePath(imageName) });
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
		static NSUserDefaults UserDefaults {
			get {
				return NSUserDefaults.StandardUserDefaults;
			}
		}
	}
}