using System;
using MonoTouch.UIKit;
using BeeBaby.ResourcesProviders;
using System.Drawing;
using MonoTouch.FacebookConnect;
using Skahal.Infrastructure.Framework.Globalization;
using Domain.Moment;
using PixateFreestyleLib;

namespace BeeBaby
{
	public class UIViewFullscreen : UIView
	{
		UIImage m_image;
		UIScrollViewImage m_scrollImage;
		Moment m_moment;

		public UIViewFullscreen()
		{
			BackgroundColor = UIColor.Black;

			m_scrollImage = new UIScrollViewImage();
			AddSubview(m_scrollImage);
			LoadOrientationNotification();
		}

		/// <summary>
		/// Sets the image.
		/// </summary>
		/// <param name="image">Image.</param>
		/// <param name="moment">Moment.</param>
		public void SetImage(UIImage image, Moment moment)
		{
			m_image = image;
			m_moment = moment;
		}

		/// <summary>
		/// Show this instance.
		/// </summary>
		public void Show()
		{
			var window = UIApplication.SharedApplication.Windows[0];
			Frame = window.Frame;
			m_scrollImage.Frame = Frame;
			m_scrollImage.SetImage(m_image);
			m_scrollImage.OnSingleTap += () => {
				Hide();
			};
				
			UIButton button = new UIButton(UIButtonType.RoundedRect);
			button.Frame = new RectangleF(20, 20, 100, 50);
			button.SetTitle("Share".Translate(), UIControlState.Normal);
			button.TouchUpInside += (sender, e) => ShareImage(m_image, m_moment); 
			button.SetStyleClass("share-button");

			AddSubview(button);

			window.AddSubview(this);

			Alpha = 0f;
			UIView.Animate(AnimationDuration, () => {
				Alpha = 1f;
			});
		}

		/// <summary>
		/// Shares the image.
		/// </summary>
		/// <param name="sourceImage">Source image.</param>
		/// <param name="moment">Moment.</param>
		void ShareImage(UIImage sourceImage, Moment moment)
		{
			ActionProgress actionProgress = new ActionProgress(() => {
				var image = new ImageProvider().CreateImageForShare(sourceImage, moment);
				m_scrollImage.SetImage(image);
				bool ios6ShareDialog = FBDialogs.CanPresentOSIntegratedShareDialog(FBSession.ActiveSession);
				if (ios6ShareDialog)
				{
					FBDialogs.PresentOSIntegratedShareDialogModally(UIApplication.SharedApplication.Windows[0].RootViewController
						, null, image, null, (result, error) => {
						if (error != null)
							InvokeOnMainThread(() => new UIAlertView("Error", error.Description, null, "Ok", null).Show());
						else if (result == FBOSIntegratedShareDialogResult.Succeeded)
							InvokeOnMainThread(() => new UIAlertView("Success".Translate(), "Moment successfully posted to Facebook".Translate(), null, "Ok", null).Show());
					});
				}
				else
				{
					Console.WriteLine("Erro ao dar Share no Facebook.");
				}
			});
			actionProgress.Execute();
		}

		/// <summary>
		/// Hide this instance.
		/// </summary>
		public void Hide()
		{
			if (Superview != null)
			{
				if (!UseAnimation)
				{
					RemoveFromSuperview();
				}
				else
				{
					Alpha = 1f;
					UIView.Animate(AnimationDuration, () => {
						Alpha = 0f;
					}, () => {
						RemoveFromSuperview();
					});
				}
			}
		}

		/// <summary>
		/// Loads the orientation notification.
		/// </summary>
		void LoadOrientationNotification()
		{
			var orientationNotification = OrientationNotification.Add(m_scrollImage);
			orientationNotification.RotationFinished += () => {
				m_scrollImage.Frame = Frame;
				m_scrollImage.SetImage(m_image);
			};
		}

		/// <summary>
		/// The use animation.
		/// </summary>
		public bool UseAnimation = true;
		/// <summary>
		/// The duration of the animation.
		/// </summary>
		public float AnimationDuration = 0.3f;
	}
}