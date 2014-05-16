﻿using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using BeeBaby.ResourcesProviders;
using Application;
using System.Collections.Generic;
using System.Threading;
using Domain.Baby;
using Domain.Media;

namespace BeeBaby
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
			ActionProgress actionProgress = new ActionProgress(() => {
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
			using (UIImage photo = (UIImage) info.ObjectForKey(UIImagePickerController.OriginalImage))
			{
				ImageProvider.SavePermanentImageOnApp(photo, MediaBase.PhotoProfileName);
			}
		}

		/// <summary>
		/// Gets or sets the completion handler.
		/// </summary>
		/// <value>The completion handler.</value>
		public NSAction CompletionHandler {
			get;
			set;
		}
	}
}