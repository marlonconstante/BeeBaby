using System;
using MonoTouch.UIKit;
using System.Drawing;
using PixateFreestyleLib;
using Domain.Media;
using BeeBaby.ResourcesProviders;
using Application;
using System.Collections.Generic;
using BeeBaby.ViewModels;
using Domain.Baby;

namespace BeeBaby
{
	public partial class ProfileView : UIView
	{
		UIImage m_photoProfile;
		const float s_profilePadding = 10f;

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
			m_photoProfile = UIImage.FromFile("photo-profile.png");
			AddBackgroundProfile();
			AddSubview(BuildViewProfiles());
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
		/// Gets the baby profiles.
		/// </summary>
		/// <returns>The baby profiles.</returns>
		IList<BabyProfile> GetBabyProfiles()
		{
			IList<BabyProfile> babyProfiles = new List<BabyProfile>();
			foreach (var baby in new BabyService().GetAllBabys())
			{
				var imagePickerDelegate = new BabyImagePickerDelegate(baby);
				babyProfiles.Add(new BabyProfile() {
					Baby = baby,
					Image = GetPhotoProfile(imagePickerDelegate.ImageProvider),
					Delegate = imagePickerDelegate
				});
			}
			return babyProfiles;
		}

		/// <summary>
		/// Gets the photo profile.
		/// </summary>
		/// <returns>The photo profile.</returns>
		/// <param name="imageProvider">Image provider.</param>
		UIImage GetPhotoProfile(ImageProvider imageProvider)
		{
			UIImage photoProfile = imageProvider.GetImage(MediaBase.PhotoProfileName, true);
			if (photoProfile == null)
			{
				photoProfile = m_photoProfile;
			}
			return photoProfile;
		}

		/// <summary>
		/// Builds the view profiles.
		/// </summary>
		/// <returns>The view profiles.</returns>
		UIScrollView BuildViewProfiles()
		{
			IList<BabyProfile> babyProfiles = GetBabyProfiles();

			var scrollTotalWidth = babyProfiles.Count * (MediaBase.PhotoProfileSize + s_profilePadding) + s_profilePadding;
			var scrollWidth = Math.Min(Frame.Width, scrollTotalWidth);
			var scrollX = (Frame.Width / 2f) - (scrollWidth / 2f);
			var scrollY = (Frame.Height / 2f) - (MediaBase.PhotoProfileSize / 2f);
			var imagePosition = (MediaBase.PhotoProfileSize - MediaBase.PhotoProfileInnerSize) / 2;
			var imageFrame = new RectangleF(imagePosition, imagePosition, MediaBase.PhotoProfileInnerSize, MediaBase.PhotoProfileInnerSize);

			UIScrollView scrollView = new UIScrollView(new RectangleF(scrollX, scrollY, scrollWidth, MediaBase.PhotoProfileSize + s_profilePadding));
			scrollView.ContentSize = new SizeF(scrollTotalWidth, MediaBase.PhotoProfileSize + s_profilePadding);

			var index = 0;
			foreach (var babyProfile in babyProfiles)
			{
				using (var imageView = BuildImageView(imageFrame, babyProfile.Image))
				{
					imageView.OnClick += () => {
						babyProfile.Delegate.CompletionHandler = () => {
							imageView.Image = GetPhotoProfile(babyProfile.Delegate.ImageProvider);
						};

						var mediaPickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.SavedPhotosAlbum, babyProfile.Delegate);
						var m_picker = mediaPickerProvider.GetUIImagePickerController();

						Window.RootViewController.PresentViewController(m_picker, false, null);
					};
					imageView.Layer.CornerRadius = 45f;

					UIView view = new UIView(new RectangleF(index * (MediaBase.PhotoProfileSize + s_profilePadding) + s_profilePadding, 0f, MediaBase.PhotoProfileSize, MediaBase.PhotoProfileSize));
					view.SetStyleClass("photo-profile");
					view.AddSubview(imageView);

					scrollView.AddSubview(view);
				}
				index++;
			}

			return scrollView;
		}

		/// <summary>
		/// Builds the image view.
		/// </summary>
		/// <returns>The image view.</returns>
		/// <param name="frame">Frame.</param>
		/// <param name="image">Image.</param>
		UIImageViewClickable BuildImageView(RectangleF frame, UIImage image)
		{
			UIImageViewClickable imageView = new UIImageViewClickable(frame);
			imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
			imageView.ClipsToBounds = true;
			imageView.Image = image;
			return imageView;
		}
	}
}