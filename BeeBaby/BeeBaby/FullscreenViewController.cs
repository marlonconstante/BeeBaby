using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using BeeBaby.ResourcesProviders;
using Skahal.Infrastructure.Framework.Globalization;
using Domain.Baby;
using BeeBaby.Activity;

namespace BeeBaby
{
	public partial class FullscreenViewController : BaseViewController
	{
		UIImage m_photo;
		Moment m_moment;

		public FullscreenViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			FlurryAnalytics.Flurry.LogEvent("Fullscreen Foto: Abriu.");

			base.ViewDidLoad();

			AddSingleTapGestureRecognizer();
		}

		/// <summary>
		/// Adds the single tap gesture recognizer.
		/// </summary>
		void AddSingleTapGestureRecognizer()
		{
			var singleTap = new UITapGestureRecognizer(() =>
			{
				ShowOrHideSubviews();
			});
			singleTap.NumberOfTapsRequired = 1;
			View.AddGestureRecognizer(singleTap);
		}

		/// <summary>
		/// Show or hide subviews.
		/// </summary>
		void ShowOrHideSubviews()
		{
			foreach (var view in View.Subviews)
			{
				if (view.GetType() != typeof(UIScrollView))
				{
					view.Hidden = !view.Hidden;
				}
			}
		}

		/// <summary>
		/// Gets the supported interface orientations.
		/// </summary>
		/// <returns>The supported interface orientations.</returns>
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.All;
		}

		/// <summary>
		/// Determines whether this instance is show status bar.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		public override bool IsShowStatusBar()
		{
			return false;
		}

		/// <summary>
		/// Close the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Close(UIButton sender)
		{
			ShowProgressWhilePerforming(() =>
			{
				PresentingViewController.DismissViewController(false, null);
				var photo = imgPhoto.Image;
				InvokeInBackground(() =>
				{
					photo.Dispose();
					m_photo.Dispose();
				});
			}, false);
		}

		/// <summary>
		/// Share the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Share(UIButton sender)
		{
			ShowProgressWhilePerforming(() =>
			{
				var instagramActivity = new InstagramActivity();
				instagramActivity.IncludeURL = false;
				instagramActivity.PresentFromView = View;

				imgPhoto.Image = new ImageProvider().CreateImageForShare(m_photo, m_moment);

				var shareText = m_moment.Event.Description + ((m_moment.Description.Length > 0) ? " - " + m_moment.Description : string.Empty);
				var shareUrl = new NSUrl(string.Empty);
				var activityItems = new NSObject[]{ (NSString)shareText, shareUrl, imgPhoto.Image };
				var applicationActivities = new UIActivity[]{ instagramActivity };
				var activityViewController = new UIActivityViewController(activityItems, applicationActivities);

				activityViewController.CompletionHandler += (activityTitle, close) =>
				{
					if (activityTitle != null && activityTitle.ToString().Equals("UIActivityTypePostToInstagram"))
					{
						instagramActivity.DocumentController.PresentOpenInMenu(View.Bounds, View, true);
					}

				};
				PresentViewController(activityViewController, true, () =>
				{
					Console.WriteLine("Action Completed");
				});
			});
		}

		/// <summary>
		/// Sets the information.
		/// </summary>
		/// <param name="moment">Moment.</param>
		/// <param name="baby">Baby.</param>
		/// <param name="photo">Photo.</param>
		public void SetInformation(Moment moment, Baby baby, UIImage photo)
		{
			imgPhoto.Image = photo;
			lblAge.Text = Baby.FormatAge(baby.BirthDateTime, moment.Date);

			lblEvent.Text = moment.Event.Description;

			m_photo = photo;
			m_moment = moment;
		}
	}
}
