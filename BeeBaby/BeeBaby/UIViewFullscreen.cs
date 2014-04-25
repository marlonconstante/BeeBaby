using System;
using MonoTouch.UIKit;
using BeeBaby.ResourcesProviders;
using System.Drawing;
using MonoTouch.FacebookConnect;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public class UIViewFullscreen : UIView
	{
		UIImage iImage;
		UIScrollViewImage sviMain;
		public bool UseAnimation = true;
		public float AnimationDuration = 0.3f;

		public UIViewFullscreen()
		{
			var cBackground = new UIColor(0.0f, 0.0f, 0.0f, 1.0f);
			BackgroundColor = cBackground;

			sviMain = new UIScrollViewImage();
			AddSubview(sviMain);
			LoadOrientationNotification();
		}

		/// <summary>
		/// Sets the image.
		/// </summary>
		/// <param name="image">Image.</param>
		public void SetImage(UIImage image)
		{
			iImage = image;
		}

		/// <summary>
		/// Show this instance.
		/// </summary>
		public void Show()
		{
			var window = UIApplication.SharedApplication.Windows[0];
			Frame = window.Frame;
			sviMain.Frame = window.Frame;
			sviMain.SetImage(iImage);
			sviMain.OnSingleTap += () =>
			{
				Hide();
			};
				
			UIButton button = new UIButton(UIButtonType.RoundedRect);
			button.Frame = new RectangleF(20, 20, 100, 50);
			button.SetTitle("Share", UIControlState.Normal);
			button.TouchUpInside += (sender, e) => ShareImage(iImage); 

			AddSubview(button);

			window.AddSubview(this);

			Alpha = 0f;
			UIView.Animate(AnimationDuration, () =>
			{
				Alpha = 1f;
			});
		}

		void ShareImage(UIImage sourceImage)
		{
			var image = ImageProvider.CreateImageForShare(sourceImage);
			bool ios6ShareDialog = FBDialogs.CanPresentOSIntegratedShareDialog(FBSession.ActiveSession);
			if (ios6ShareDialog)
			{
				FBDialogs.PresentOSIntegratedShareDialogModally(UIApplication.SharedApplication.Windows[0].RootViewController
					, null, image, null, (result, error) =>
				{
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
					UIView.Animate(AnimationDuration, () =>
					{
						Alpha = 0f;
					}, () =>
					{
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
			new OrientationNotification(sviMain);
		}
	}
}