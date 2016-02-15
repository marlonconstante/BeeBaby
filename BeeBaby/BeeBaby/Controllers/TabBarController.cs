using System;

using Foundation;
using UIKit;
using CoreGraphics;
using PixateFreestyleLib;
using System.Drawing;

namespace BeeBaby.Controllers
{
	public partial class TabBarController : UITabBarController
	{
		CameraOptionsModalViewController m_cameraOptionsModal;

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

			LoadModalViewController();
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

			CloseCameraOptions(this, EventArgs.Empty);
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
		/// Loads the modal view controller.
		/// </summary>
		void LoadModalViewController()
		{
			if (m_cameraOptionsModal == null)
			{
				var board = UIStoryboard.FromName("MainStoryboard", null);
				m_cameraOptionsModal = (CameraOptionsModalViewController) board.InstantiateViewController("CameraOptionsModalViewController");
				m_cameraOptionsModal.LoadView();
			}
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
			TabBar.ItemSpacing = View.Bounds.Width / 3f;

			CameraButton.Center = new PointF(TabBar.Center.X, TabBar.Center.Y - 6f);
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
		/// Opens the camera options.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void OpenCameraOptions(object sender, EventArgs args)
		{
			if (m_cameraOptionsModal.IsVisible)
			{
				CloseCameraOptions(sender, args);
			}
			else
			{
				m_cameraOptionsModal.Show();
			}
		}

		/// <summary>
		/// Closes the camera options.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void CloseCameraOptions(object sender, EventArgs args)
		{
			m_cameraOptionsModal.Hide();
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
	}
}