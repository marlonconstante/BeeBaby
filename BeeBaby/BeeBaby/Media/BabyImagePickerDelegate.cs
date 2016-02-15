using System;
using UIKit;
using Foundation;
using BeeBaby.ResourcesProviders;
using Application;
using System.Collections.Generic;
using System.Threading;
using Domain.Baby;
using Domain.Media;
using BeeBaby.Progress;
using Domain.Synchronization;

namespace BeeBaby.Media
{
	public class BabyImagePickerDelegate : ImagePickerDelegate
	{
		public BabyImagePickerDelegate(Baby baby)
		{
			ImageProvider = new ImageProvider(baby.Id);
		}

		/// <summary>
		/// Finisheds the picking media.
		/// </summary>
		/// <param name="picker">Picker.</param>
		/// <param name="info">Info.</param>
		public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
		{
			var actionProgress = new ActionProgress(() => {
				SavePermanentImageOnApp(info);
				picker.DismissViewController(true, CompletionHandler);
			}, false);
			actionProgress.Execute();
		}

		/// <summary>
		/// Saves the permanent image on app.
		/// </summary>
		/// <param name="info">Info.</param>
		void SavePermanentImageOnApp(NSDictionary info)
		{
			using (var photo = (UIImage) info.ObjectForKey(UIImagePickerController.OriginalImage))
			{
				using (var thumbnail = ImageProvider.GenerateThumbnail(photo))
				{
					var fileName = ImageProvider.SavePermanentImageOnApp(thumbnail, MediaBase.PhotoProfileName, false);
					new FileUploadService().InsertFilePaths(new string[] { ImageProvider.GetRelativeFilePath(fileName) });
				}
			}
		}

		/// <summary>
		/// Gets or sets the completion handler.
		/// </summary>
		/// <value>The completion handler.</value>
		public Action CompletionHandler {
			get;
			set;
		}
	}
}