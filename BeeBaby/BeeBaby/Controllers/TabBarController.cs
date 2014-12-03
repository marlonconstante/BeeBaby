using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using PixateFreestyleLib;
using BeeBaby.Progress;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby.Controllers
{
	public partial class TabBarController : UITabBarController
	{
		public TabBarController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			SetImageInsets();
			AddCameraButton();
			AddCameraOptions();
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			ViewControllerSelected += CloseCameraOptions;
			CameraButton.TouchUpInside += OpenCameraOptions;
			ButtonTakePhotos.TouchUpInside += TakePhotos;
			ButtonImportPhotos.TouchUpInside += TakePhotos;
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			ViewControllerSelected -= CloseCameraOptions;
			CameraButton.TouchUpInside -= OpenCameraOptions;
			ButtonTakePhotos.TouchUpInside -= TakePhotos;
			ButtonImportPhotos.TouchUpInside -= TakePhotos;
		}

		/// <summary>
		/// Views the will layout subviews.
		/// </summary>
		public override void ViewWillLayoutSubviews()
		{
			base.ViewWillLayoutSubviews();

			UpdateTabBar();
		}

		/// <summary>
		/// Updates the tab bar.
		/// </summary>
		void UpdateTabBar()
		{
			TabBar.ShadowImage = EmptyImage;
			TabBar.BackgroundImage = EmptyImage;
			TabBar.BackgroundColor = UIColor.FromRGB(241, 241, 233);
			TabBar.Frame = TabBarFrame;

			TabBar.ItemPositioning = UITabBarItemPositioning.Centered;
			TabBar.ItemSpacing = View.Bounds.Width / 2.5f;

			CameraButton.Center = TabBar.Center;
		}

		/// <summary>
		/// Sets the image insets.
		/// </summary>
		void SetImageInsets()
		{
			foreach (var item in TabBar.Items)
			{
				item.ImageInsets = new UIEdgeInsets(6f, 0f, -6f, 0f);
				item.Image = item.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
			}
		}

		/// <summary>
		/// Adds the camera button.
		/// </summary>
		void AddCameraButton()
		{
			CameraButton.SetStyleClass("tab-bar-camera");

			View.Add(CameraButton);
		}

		/// <summary>
		/// Adds the camera options.
		/// </summary>
		void AddCameraOptions()
		{
			CameraOptions.Frame = new RectangleF(0f, View.Frame.Height - 80.5f, View.Frame.Width, 40f);
			CameraOptions.SetStyleClass("camera-options");

			ButtonTakePhotos.SetStyleClass("take-photos");
			ButtonTakePhotos.SetTitle("TakePhotos".Translate(), UIControlState.Normal);
			CameraOptions.Add(ButtonTakePhotos);

			ButtonImportPhotos.SetStyleClass("import-photos");
			ButtonImportPhotos.SetTitle("ImportPhotos".Translate(), UIControlState.Normal);
			CameraOptions.Add(ButtonImportPhotos);

			Arrow.SetStyleClass("arrow");
			CameraOptions.Add(Arrow);

			View.Add(CameraOptions);
		}

		/// <summary>
		/// Opens the camera options.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void OpenCameraOptions(object sender, EventArgs args)
		{
			if (CameraOptions.Alpha == 0f)
			{
				CameraOptions.SetStyleClass("camera-options bubble");
			}
			else
			{
				CloseCameraOptions(sender, args);
			}
		}

		/// <summary>
		/// Closes the camera options.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void CloseCameraOptions(object sender, EventArgs args)
		{
			CameraOptions.SetStyleClass("camera-options fadeOut");
		}

		/// <summary>
		/// Takes the photos.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void TakePhotos(object sender, EventArgs args)
		{
			var actionProgress = new ActionProgress(() => {
				RootViewController.PerformSegue("segueCamera", sender as NSObject);
			}, false);
			actionProgress.Execute();
		}

		/// <summary>
		/// Gets the tab bar frame.
		/// </summary>
		/// <value>The tab bar frame.</value>
		RectangleF TabBarFrame {
			get {
				var frame = TabBar.Frame;
				frame.Height = 40;
				frame.Y = View.Frame.Height - frame.Height;
				return frame;
			}
		}

		/// <summary>
		/// Gets the empty image.
		/// </summary>
		/// <value>The empty image.</value>
		UIImage EmptyImage {
			get;
		} = new UIImage();

		/// <summary>
		/// Gets the camera button.
		/// </summary>
		/// <value>The camera button.</value>
		UIButton CameraButton {
			get;
		} = new UIButton();

		/// <summary>
		/// Gets the camera options.
		/// </summary>
		/// <value>The camera options.</value>
		UIView CameraOptions {
			get;
		} = new UIView();

		/// <summary>
		/// Gets the button take photos.
		/// </summary>
		/// <value>The button take photos.</value>
		UIButton ButtonTakePhotos {
			get;
		} = new UIButton();

		/// <summary>
		/// Gets the button import photos.
		/// </summary>
		/// <value>The button import photos.</value>
		UIButton ButtonImportPhotos {
			get;
		} = new UIButton();

		/// <summary>
		/// Gets the arrow.
		/// </summary>
		/// <value>The arrow.</value>
		UIView Arrow {
			get;
		} = new UIView();

		/// <summary>
		/// Gets the root view controller.
		/// </summary>
		/// <value>The root view controller.</value>
		public UIViewController RootViewController {
			get {
				return UIApplication.SharedApplication.Windows[0].RootViewController;
			}
		}
	}
}