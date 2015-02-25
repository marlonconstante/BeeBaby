using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using BeeBaby.ResourcesProviders;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using Domain.Baby;
using BeeBaby.Activity;
using SwipeViewer;
using System.Collections.Generic;
using BeeBaby.ViewModels;
using BeeBaby.VisualElements;

namespace BeeBaby.Controllers
{
	public partial class FullscreenViewController : BaseViewController
	{
		IMoment m_moment;
		bool m_showProgress = true;

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
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			vwSwipe.ChangeScrollOffset = true;
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
			vwSwipe.DataSource = new SwipeViewerDataSource();
			vwSwipe.Delegate = new SwipeViewerDelegate();
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
			vwSwipe.ScrollToItemAtIndex(vwSwipe.CurrentItemIndex, 0d);
		}

		/// <summary>
		/// Dids the rotate.
		/// </summary>
		/// <param name="fromInterfaceOrientation">From interface orientation.</param>
		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate(fromInterfaceOrientation);

			vwSwipe.ChangeScrollOffset = true;
			vwSwipe.LayoutSubviews();
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
			vwSwipe.ChangeScrollOffset = false;
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

				var whatsAppActivity = new WhatsAppActivity();
				whatsAppActivity.IncludeURL = false;
				whatsAppActivity.PresentFromView = View;


				var shareText = m_moment.EventDescription + ((m_moment.MomentDescription.Length > 0) ? " - " + m_moment.MomentDescription : string.Empty);
				var shareUrl = new NSUrl(string.Empty);
				var activityItems = new NSObject[]{ (NSString) shareText, shareUrl, imageModel.Image };
				var applicationActivities = new UIActivity[]{ instagramActivity, whatsAppActivity };
				var activityViewController = new UIActivityViewController(activityItems, applicationActivities);

				activityViewController.CompletionHandler += (activityTitle, close) => {
					if (activityTitle != null && activityTitle.ToString().Equals("UIActivityTypePostToInstagram"))
					{
						instagramActivity.DocumentController.PresentOpenInMenu(View.Bounds, View, true);
					}
					else if(activityTitle != null && activityTitle.ToString().Equals("UIActivityTypePostToWhatsApp"))
					{
						whatsAppActivity.DocumentController.PresentOpenInMenu(View.Bounds, View, true);
					}

					m_showProgress = true;
				};
				PresentViewController(activityViewController, true, () => {
					m_showProgress = false;
				});
			});
		}

		/// <summary>
		/// Determines whether this instance is show progress.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		public override bool IsShowProgress()
		{
			return m_showProgress;
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
				return ((SwipeViewerDataSource) vwSwipe.DataSource).Images;
			}
		}

		/// <summary>
		/// Sets the information.
		/// </summary>
		/// <param name="moment">Moment.</param>
		/// <param name="itemIndex">Item index.</param>
		public void SetInformation(IMoment moment, int itemIndex)
		{
			Images.AddRange(new ImageProvider(moment.MomentId).getMomentImages(moment));
			vwSwipe.ReloadData();
			vwSwipe.CurrentItemIndex = itemIndex;

			lblAge.Text = Baby.FormatAge(moment.BabyBirthDateTime, moment.MomentDate);

			lblEvent.Text = moment.EventDescription;

			m_moment = moment;
		}
	}
}
