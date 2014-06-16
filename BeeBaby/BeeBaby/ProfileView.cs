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
	public partial class ProfileView : View
	{
		const float s_padding = 10f;
		bool m_menu;
		RectangleF m_frame;
		UIView m_viewProfiles;
		Button m_buttonSelectedBaby;

		public ProfileView(IntPtr handle) : base(handle)
		{
			InitProfile(false);
		}

		public ProfileView(RectangleF frame) : base(frame)
		{
			InitProfile(true);
		}

		/// <summary>
		/// Updates the baby profiles.
		/// </summary>
		public void UpdateBabyProfiles()
		{
			var baby = CurrentContext.Instance.CurrentBaby;
			if (baby != null)
			{
				if (m_viewProfiles != null)
				{
					m_viewProfiles.RemoveFromSuperview();
					Discard.ReleaseSubviews(m_viewProfiles);
				}

				m_viewProfiles = BuildViewProfiles();
				AddSubview(m_viewProfiles);

				if (m_menu)
				{
					if (m_buttonSelectedBaby != null)
					{
						m_buttonSelectedBaby.RemoveFromSuperview();
						Discard.ReleaseSubviews(m_buttonSelectedBaby);
					}

					m_buttonSelectedBaby = BuildButtonSelectedBaby(baby);
					AddSubview(m_buttonSelectedBaby);
				}
			}
		}

		/// <summary>
		/// Inits the profile.
		/// </summary>
		/// <param name="menu">If set to <c>true</c> menu.</param>
		void InitProfile(bool menu)
		{
			m_menu = menu;
			m_frame = Frame;
			AddBackgroundProfile();
			if (!menu)
			{
				UpdateBabyProfiles();
			}
		}

		/// <summary>
		/// Adds the background profile.
		/// </summary>
		void AddBackgroundProfile()
		{
			var frame = new RectangleF(0f, 0f, m_frame.Width, m_frame.Height);
			AddSubview(BuildImageView(frame, UIImage.FromFile("background-profile.png")));
			ClipsToBounds = true;
		}

		/// <summary>
		/// Opens the baby view controller.
		/// </summary>
		void OpenBabyViewController()
		{
			var rootViewController = Window.RootViewController;
			if (rootViewController.GetType() == typeof(SlideoutNavigationController))
			{
				var slideoutNavigation = (SlideoutNavigationController) rootViewController;
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
			UIImage photoProfile = imageProvider.GetImage(MediaBase.PhotoProfileName, true);
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
			IList<BabyProfile> babyProfiles = new List<BabyProfile>();
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
		/// Builds the button selected baby.
		/// </summary>
		/// <returns>The button selected baby.</returns>
		/// <param name="baby">Baby.</param>
		Button BuildButtonSelectedBaby(Baby baby)
		{
			var width = m_frame.Width - (s_padding * 2);
			var height = 64f;
			var y = m_frame.Height - height;

			Button button = new Button(new RectangleF(s_padding, y, width, height));
			button.SetStyleClass("baby-button");
			button.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
			button.VerticalAlignment = UIControlContentVerticalAlignment.Center;
			button.SetTitle(baby.Name + " >", UIControlState.Normal);

			var proxy = new EventProxy<ProfileView, EventArgs>(this);
			proxy.Action = (target, sender, args) => {
				target.OpenBabyViewController();
			};
			button.TouchUpInside += proxy.HandleEvent;

			return button;
		}

		/// <summary>
		/// Builds the view profile.
		/// </summary>
		/// <returns>The view profile.</returns>
		/// <param name="babyProfile">Baby profile.</param>
		/// <param name="index">Index.</param>
		UIView BuildViewProfile(BabyProfile babyProfile, int index = -1)
		{
			var x = (m_frame.Width / 2f) - (MediaBase.PhotoProfileSize / 2f);
			var y = (m_frame.Height / 2f) - (MediaBase.PhotoProfileSize / 2f);

			if (index >= 0)
			{
				x = index * (MediaBase.PhotoProfileSize + s_padding) + s_padding;
			}

			UIView view = new UIView(new RectangleF(x, y, MediaBase.PhotoProfileSize, MediaBase.PhotoProfileSize));
			view.SetStyleClass("photo-profile");
			view.AddSubview(BuildImageViewProfile(babyProfile));

			return view;
		}

		/// <summary>
		/// Builds the view profiles.
		/// </summary>
		/// <returns>The view profiles.</returns>
		UIView BuildViewProfiles()
		{
			if (m_menu)
			{
				return BuildViewProfile(BuildBabyProfile(CurrentContext.Instance.CurrentBaby));
			}
			else
			{
				IList<BabyProfile> babyProfiles = GetBabyProfiles();

				var totalWidth = babyProfiles.Count * (MediaBase.PhotoProfileSize + s_padding) + s_padding;
				var width = Math.Min(m_frame.Width, totalWidth);
				var x = (m_frame.Width / 2f) - (width / 2f);

				UIScrollView scrollView = new UIScrollView(new RectangleF(x, 0f, width, m_frame.Height));
				scrollView.ContentSize = new SizeF(totalWidth, m_frame.Height);

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
		/// Builds the image view profile.
		/// </summary>
		/// <returns>The image view profile.</returns>
		/// <param name="babyProfile">Baby profile.</param>
		BabyImageView BuildImageViewProfile(BabyProfile babyProfile)
		{
			var position = (MediaBase.PhotoProfileSize - MediaBase.PhotoProfileInnerSize) / 2;
			var frame = new RectangleF(position, position, MediaBase.PhotoProfileInnerSize, MediaBase.PhotoProfileInnerSize);

			BabyImageView imageView = BuildImageView(frame, babyProfile.Image);
			imageView.BabyProfile = babyProfile;
			imageView.Layer.CornerRadius = 45f;

			var proxy = new EventProxy<ProfileView, EventArgs>(this);
			proxy.Action = (target, sender, args) => {
				if (target.m_menu)
				{
					target.OpenBabyViewController();
				}
				else
				{
					var babyImageView = (BabyImageView) sender;
					var imagePickerDelegate = babyImageView.BabyProfile.Delegate;
					imagePickerDelegate.CompletionHandler = () => {
						babyImageView.Image = target.GetPhotoProfile(imagePickerDelegate.ImageProvider);
					};

					var mediaPickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.SavedPhotosAlbum, imagePickerDelegate);
					var picker = mediaPickerProvider.GetUIImagePickerController();

					Window.RootViewController.PresentViewController(picker, false, null);
				}
			};
			imageView.Clicked += proxy.HandleEvent;

			return imageView;
		}

		/// <summary>
		/// Builds the image view.
		/// </summary>
		/// <returns>The image view.</returns>
		/// <param name="frame">Frame.</param>
		/// <param name="image">Image.</param>
		BabyImageView BuildImageView(RectangleF frame, UIImage image)
		{
			BabyImageView imageView = new BabyImageView(frame);
			imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
			imageView.ClipsToBounds = true;
			imageView.Image = image;
			return imageView;
		}
	}
}