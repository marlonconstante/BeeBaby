using System;
using MonoTouch.UIKit;
using BeeBaby.ResourcesProviders;
using BigTed;
using PixateFreestyleLib;

namespace BeeBaby
{
	public class ImagePickerDelegate : UIImagePickerControllerDelegate
	{
		public ImagePickerDelegate()
		{
		}

		/// <summary>
		/// Wills the show view controller.
		/// </summary>
		/// <param name="navigationController">Navigation controller.</param>
		/// <param name="viewController">View controller.</param>
		public override void WillShowViewController(UINavigationController navigationController, UIViewController viewController, bool animated)
		{
			navigationController.View.AddStyleClass("navigation");
			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

			// Dismiss the spinner
			BTProgressHUD.Dismiss();
		}

		/// <summary>
		/// Gets or sets the image provider.
		/// </summary>
		/// <value>The image provider.</value>
		public ImageProvider ImageProvider {
			get;
			set;
		}

		/// <summary>
		/// Dispose the specified disposing.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Discard.ReleaseProperties(this);
				Discard.ReleaseFields(this);
			}

			base.Dispose(disposing);
		}
	}
}