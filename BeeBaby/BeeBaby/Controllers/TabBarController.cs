using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using PixateFreestyleLib;
using BeeBaby.Progress;

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
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			CameraButton.TouchUpInside += OpenCamera;
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			CameraButton.TouchUpInside -= OpenCamera;
		}

		/// <summary>
		/// Sets the image insets.
		/// </summary>
		void SetImageInsets()
		{
			foreach (var item in TabBar.Items)
			{
				item.ImageInsets = new UIEdgeInsets(5f, 0f, -5f, 0f);
			}
		}

		/// <summary>
		/// Adds the camera button.
		/// </summary>
		void AddCameraButton()
		{
			CameraButton.SetStyleClass("camera");
			CameraButton.Center = TabBar.Center;

			View.Add(CameraButton);
		}

		/// <summary>
		/// Opens the camera.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void OpenCamera(object sender, EventArgs args)
		{
			var actionProgress = new ActionProgress(() => {
				RootViewController.PerformSegue("segueCamera", sender as NSObject);
			}, false);
			actionProgress.Execute();
		}

		/// <summary>
		/// Gets the camera button.
		/// </summary>
		/// <value>The camera button.</value>
		UIButton CameraButton {
			get;
		} = new UIButton {
			Frame = new RectangleF(0f, 0f, 40f, 40f)
		};

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