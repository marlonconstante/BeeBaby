using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using BeeBaby.ResourcesProviders;
using Skahal.Infrastructure.Framework.Globalization;
using Domain.Baby;
using BeeBaby.Activity;
using SwipeViewer;
using System.Collections.Generic;
using BeeBaby.ViewModels;

namespace BeeBaby
{
	public partial class FullscreenViewController : BaseViewController
	{
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
			InitSwipeView();
		}

		/// <summary>
		/// Adds the single tap gesture recognizer.
		/// </summary>
		void AddSingleTapGestureRecognizer()
		{
			var singleTap = new UITapGestureRecognizer(() => {
				ShowOrHideSubviews();
			});
			singleTap.NumberOfTapsRequired = 1;
			View.AddGestureRecognizer(singleTap);
		}

		/// <summary>
		/// Inits the swipe view.
		/// </summary>
		void InitSwipeView()
		{
			vwSwipe.DataSource = new SwipeViewDataSource();
			vwSwipe.Delegate = new SwipeViewDelegate();
		}

		/// <summary>
		/// Show or hide subviews.
		/// </summary>
		public void ShowOrHideSubviews()
		{
			foreach (var view in View.Subviews)
			{
				if (view.GetType() != typeof(SwipeView))
				{
					view.Hidden = !view.Hidden;
				}
			}
		}

		/// <summary>
		/// Wills the rotate.
		/// </summary>
		/// <param name="toInterfaceOrientation">To interface orientation.</param>
		/// <param name="duration">Duration.</param>
		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate(toInterfaceOrientation, duration);
			vwSwipe.ChangeScrollOffset = false;
		}

		/// <summary>
		/// Dids the rotate.
		/// </summary>
		/// <param name="fromInterfaceOrientation">From interface orientation.</param>
		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate(fromInterfaceOrientation);
			vwSwipe.ChangeScrollOffset = true;
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
			ShowProgressWhilePerforming(() => {
				PresentingViewController.DismissViewController(true, () => {
					InvokeInBackground(() => {
						ReleaseImages();
					});
				});
			}, false);
		}

		/// <summary>
		/// Share the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Share(UIButton sender)
		{
			ShowProgressWhilePerforming(() => {
				var imageModel = Images[vwSwipe.CurrentItemIndex] as ImageModel;
				if (!imageModel.Changed)
				{
					imageModel.Image = new ImageProvider().CreateImageForShare(imageModel.Image, m_moment);
					vwSwipe.ReloadItemAtIndex(vwSwipe.CurrentItemIndex);
				}

				var instagramActivity = new InstagramActivity();
				instagramActivity.IncludeURL = false;
				instagramActivity.PresentFromView = View;

				var shareText = m_moment.Event.Description + ((m_moment.Description.Length > 0) ? " - " + m_moment.Description : string.Empty);
				var shareUrl = new NSUrl(string.Empty);
				var activityItems = new NSObject[]{ (NSString) shareText, shareUrl, imageModel.Image };
				var applicationActivities = new UIActivity[]{ instagramActivity };
				var activityViewController = new UIActivityViewController(activityItems, applicationActivities);

				activityViewController.CompletionHandler += (activityTitle, close) => {
					if (activityTitle != null && activityTitle.ToString().Equals("UIActivityTypePostToInstagram"))
					{
						instagramActivity.DocumentController.PresentOpenInMenu(View.Bounds, View, true);
					}
				};
				PresentViewController(activityViewController, true, () => {
					Console.WriteLine("Action Completed");
				});
			});
		}

		/// <summary>
		/// Releases the images.
		/// </summary>
		void ReleaseImages()
		{
			foreach (var image in Images)
			{
				image.Dispose();
			}
			Images.Clear();
		}

		/// <summary>
		/// Gets the images.
		/// </summary>
		/// <value>The images.</value>
		public List<ImageModel> Images {
			get {
				return ((SwipeViewDataSource) vwSwipe.DataSource).Images;
			}
		}

		/// <summary>
		/// Sets the information.
		/// </summary>
		/// <param name="moment">Moment.</param>
		/// <param name="baby">Baby.</param>
		/// <param name="photo">Photo.</param>
		public void SetInformation(Moment moment, Baby baby, int itemIndex)
		{
			Images.AddRange(new ImageProvider(moment.Id).GetImages());
			vwSwipe.CurrentItemIndex = itemIndex;
			vwSwipe.ReloadData();

			lblAge.Text = Baby.FormatAge(baby.BirthDateTime, moment.Date);

			lblEvent.Text = moment.Event.Description;

			m_moment = moment;
		}
	}
}
