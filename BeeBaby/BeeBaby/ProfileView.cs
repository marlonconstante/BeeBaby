using System;
using MonoTouch.UIKit;
using System.Drawing;
using PixateFreestyleLib;
using Domain.Media;

namespace BeeBaby
{
	public partial class ProfileView : UIView
	{
		public ProfileView(IntPtr handle) : base(handle)
		{
			InitProfile();
		}

		public ProfileView(RectangleF frame) : base(frame)
		{
			InitProfile();
		}

		/// <summary>
		/// Inits the profile.
		/// </summary>
		void InitProfile()
		{
			AddBackgroundProfile();
			AddPhotoProfile();
		}

		/// <summary>
		/// Adds the background profile.
		/// </summary>
		void AddBackgroundProfile()
		{
			var frame = new RectangleF(0f, 0f, Frame.Width, Frame.Height);
			AddSubview(BuildImageView(frame, UIImage.FromFile("background-profile.png")));
			ClipsToBounds = true;
		}

		/// <summary>
		/// Adds the photo profile.
		/// </summary>
		void AddPhotoProfile()
		{
			var x = (Frame.Width / 2f) - (MediaBase.PhotoProfileSize / 2f);
			var y = (Frame.Height / 2f) - (MediaBase.PhotoProfileSize / 2f);
			var pos = (MediaBase.PhotoProfileSize - MediaBase.PhotoProfileInnerSize) / 2;

			var frame = new RectangleF(pos, pos, MediaBase.PhotoProfileInnerSize, MediaBase.PhotoProfileInnerSize);
			var imageView = BuildImageView(frame, UIImage.FromFile("photo-profile.png"), () => {

			});
			imageView.Layer.CornerRadius = 45f;

			UIView photoProfile = new UIView(new RectangleF(x, y, MediaBase.PhotoProfileSize, MediaBase.PhotoProfileSize));
			photoProfile.SetStyleClass("photo-profile");
			photoProfile.AddSubview(imageView);

			AddSubview(photoProfile);
		}

		/// <summary>
		/// Builds the image view.
		/// </summary>
		/// <returns>The image view.</returns>
		/// <param name="frame">Frame.</param>
		/// <param name="image">Image.</param>
		/// <param name="action">Action.</param>
		UIImageView BuildImageView(RectangleF frame, UIImage image, Action action = null)
		{
			UIImageViewClickable imageView = new UIImageViewClickable(frame);
			imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
			imageView.ClipsToBounds = true;
			imageView.Image = image;
			if (action != null)
			{
				imageView.OnClick += action;
			}
			return imageView;
		}
	}
}