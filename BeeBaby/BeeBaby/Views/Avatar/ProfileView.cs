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
using BeeBaby.Util;
using BeeBaby.Progress;

namespace BeeBaby
{
	public partial class ProfileView : AvatarView
	{
		public ProfileView(IntPtr handle) : base(handle, AvatarTemplate.Photo)
		{
		}

		public ProfileView(RectangleF frame) : base(frame, AvatarTemplate.PhotoAndDescription)
		{
		}

		/// <summary>
		/// Gets the name of the background image.
		/// </summary>
		/// <returns>The background image name.</returns>
		protected override string GetBackgroundImageName()
		{
			return "background-profile";
		}

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <returns>The description.</returns>
		protected override string GetDescription()
		{
			return string.Concat(CurrentContext.Instance.CurrentBaby.Name, " >");
		}

		/// <summary>
		/// Gets the description style class.
		/// </summary>
		/// <returns>The description style class.</returns>
		protected override string GetDescriptionStyleClass()
		{
			return "baby-label";
		}

		/// <summary>
		/// Action the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		protected override void Action(UIView sender)
		{
			if (Template == AvatarTemplate.PhotoAndDescription)
			{
				OpenBabyViewController();
			}
			else
			{
				var actionProgress = new ActionProgress(() => {
					var babyImageView = (BabyImageView) sender;
					var imagePickerDelegate = babyImageView.BabyProfile.Delegate;
					imagePickerDelegate.CompletionHandler = () => {
						babyImageView.Image = GetPhotoProfile(imagePickerDelegate.ImageProvider);
					};

					var mediaPickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.SavedPhotosAlbum, imagePickerDelegate);
					var picker = mediaPickerProvider.GetUIImagePickerController();

					var viewController = Windows.GetTopViewController(Window);
					viewController.PresentViewController(picker, false, null);
				}, false);
				actionProgress.Execute();
			}
		}

		/// <summary>
		/// Builds the photo.
		/// </summary>
		/// <returns>The photo.</returns>
		protected override UIView BuildPhoto()
		{
			if (Template == AvatarTemplate.PhotoAndDescription)
			{
				return BuildViewProfile(BuildBabyProfile(CurrentContext.Instance.CurrentBaby));
			}
			else
			{
				var babyProfiles = GetBabyProfiles();

				var totalWidth = babyProfiles.Count * (MediaBase.PhotoProfileSize + Padding) + Padding;
				var width = Math.Min(InitialFrame.Width, totalWidth);
				var x = (InitialFrame.Width / 2f) - (width / 2f);

				var scrollView = new UIScrollView(new RectangleF(x, 0f, width, InitialFrame.Height));
				scrollView.ContentSize = new SizeF(totalWidth, InitialFrame.Height);

				var index = 0;
				foreach (var babyProfile in babyProfiles)
				{
					scrollView.AddSubview(BuildViewProfile(babyProfile, index));
					index++;
				}

				return scrollView;
			}
		}

		/// <summary>
		/// Opens the baby view controller.
		/// </summary>
		void OpenBabyViewController()
		{
			var viewController = Window.RootViewController;
			if (viewController is SlideoutNavigationController)
			{
				var slideoutNavigation = (SlideoutNavigationController) viewController;
				var menu = (MenuViewController) slideoutNavigation.MenuViewLeft;
				menu.PushViewController("BabyViewController", true);
			}
		}

		/// <summary>
		/// Gets the photo profile.
		/// </summary>
		/// <returns>The photo profile.</returns>
		/// <param name="imageProvider">Image provider.</param>
		UIImage GetPhotoProfile(ImageProvider imageProvider)
		{
			var photoProfile = imageProvider.GetImage(MediaBase.PhotoProfileName, true);
			if (photoProfile == null)
			{
				photoProfile = UIImage.FromFile("photo-profile.png");
			}
			return photoProfile;
		}

		/// <summary>
		/// Gets the baby profiles.
		/// </summary>
		/// <returns>The baby profiles.</returns>
		IList<BabyProfile> GetBabyProfiles()
		{
			var babyProfiles = new List<BabyProfile>();
			foreach (var baby in new BabyService().GetAllBabys())
			{
				babyProfiles.Add(BuildBabyProfile(baby));
			}
			return babyProfiles;
		}

		/// <summary>
		/// Builds the baby profile.
		/// </summary>
		/// <returns>The baby profile.</returns>
		/// <param name="baby">Baby.</param>
		BabyProfile BuildBabyProfile(Baby baby)
		{
			var imagePickerDelegate = new BabyImagePickerDelegate(baby);
			var babyProfile = new BabyProfile() {
				Baby = baby,
				Image = GetPhotoProfile(imagePickerDelegate.ImageProvider),
				Delegate = imagePickerDelegate
			};

			return babyProfile;
		}

		/// <summary>
		/// Builds the view profile.
		/// </summary>
		/// <returns>The view profile.</returns>
		/// <param name="babyProfile">Baby profile.</param>
		/// <param name="index">Index.</param>
		UIView BuildViewProfile(BabyProfile babyProfile, int index = -1)
		{
			var x = (InitialFrame.Width / 2f) - (MediaBase.PhotoProfileSize / 2f);
			var y = 0f;
			if (Template == AvatarTemplate.Photo)
			{
				y = (InitialFrame.Height / 2f) - (MediaBase.PhotoProfileSize / 2f);
			}

			if (index >= 0)
			{
				x = index * (MediaBase.PhotoProfileSize + Padding) + Padding;
			}

			var view = new UIView(new RectangleF(x, y, MediaBase.PhotoProfileSize, MediaBase.PhotoProfileSize));
			view.SetStyleClass("photo-profile");
			view.AddSubview(BuildImageViewProfile(babyProfile));

			return view;
		}

		/// <summary>
		/// Builds the image view profile.
		/// </summary>
		/// <returns>The image view profile.</returns>
		/// <param name="babyProfile">Baby profile.</param>
		BabyImageView BuildImageViewProfile(BabyProfile babyProfile)
		{
			var position = (MediaBase.PhotoProfileSize - MediaBase.PhotoProfileInnerSize) / 2;
			var frame = new RectangleF(position, position, MediaBase.PhotoProfileInnerSize, MediaBase.PhotoProfileInnerSize);

			var imageView = new BabyImageView(frame);
			imageView.BabyProfile = babyProfile;
			imageView.Layer.CornerRadius = MediaBase.PhotoProfileInnerSize / 2;
			imageView.ContentMode = UIViewContentMode.ScaleAspectFill;

			UpdateImageView(imageView, babyProfile.Image, Template == AvatarTemplate.Photo);

			return imageView;
		}
	}
}