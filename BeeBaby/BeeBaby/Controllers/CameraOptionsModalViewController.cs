using System;

using Foundation;
using UIKit;
using CoreGraphics;
using PixateFreestyleLib;
using System.Threading;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using System.Drawing;

namespace BeeBaby.Controllers
{
	public partial class CameraOptionsModalViewController : ModalViewController
	{
		public CameraOptionsModalViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Loads the view.
		/// </summary>
		public override void LoadView()
		{
			base.LoadView();

			btnTakePhotos.SetTitle("TakePhotos".Translate(), UIControlState.Normal);
			btnImportPhotos.SetTitle("ImportPhotos".Translate(), UIControlState.Normal);
		}

		/// <summary>
		/// Show this instance.
		/// </summary>
		public override void Show()
		{
			base.Show();

			InvokeInBackground(() => {
				Thread.Sleep(300);

				InvokeOnMainThread(() => {
					vwPopover.RemoveStyleClass("fadeOut");
					vwPopover.AddStyleClass("bubble");
				});
			});
		}

		/// <summary>
		/// Hide this instance.
		/// </summary>
		public override void Hide()
		{
			base.Hide();

			vwPopover.RemoveStyleClass("bubble");
			vwPopover.AddStyleClass("fadeOut");
		}

		/// <summary>
		/// Takes the photos.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void TakePhotos(UIButton sender)
		{
			ShowProgressWhilePerforming(() => {
				RootViewController.PerformSegue("segueCamera", sender);
			}, false);
		}

		/// <summary>
		/// Imports the photos.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void ImportPhotos(UIButton sender)
		{
			ShowProgressWhilePerforming(() => {
				RootViewController.PerformSegue("segueMedia", sender);
			}, false);
		}

		/// <summary>
		/// Gets the frame.
		/// </summary>
		/// <value>The frame.</value>
		public override RectangleF Frame {
			get {
				return new RectangleF(0f, 0f, base.Frame.Width, base.Frame.Height - 40f);
			}
		}
	}
}