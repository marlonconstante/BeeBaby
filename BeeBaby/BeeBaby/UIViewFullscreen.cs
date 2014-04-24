using System;
using MonoTouch.UIKit;
using BeeBaby.ResourcesProviders;

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
			iImage = ImageProvider.CreateImageForShare(image);
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
			sviMain.OnSingleTap += () => {
				Hide();
			};

			window.AddSubview(this);

			Alpha = 0f;
			UIView.Animate(AnimationDuration, () => {
				Alpha = 1f;
			});
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
			new OrientationNotification(sviMain);
		}
	}
}