using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using PixateFreestyleLib;
using System.Threading;
using Skahal.Infrastructure.Framework.PCL.Globalization;

namespace BeeBaby.Controllers
{
	public partial class ConfigOnBoardingModalViewController : ModalViewController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Controllers.ConfigOnBoardingModalViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public ConfigOnBoardingModalViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Loads the view.
		/// </summary>
		public override void LoadView()
		{
			base.LoadView();

			lblSavedPhotos.Text = "InternetSavedPhotos".Translate();
			lblStart.Text = "StartSettings".Translate();
		}

		/// <summary>
		/// Show this instance.
		/// </summary>
		public override void Show()
		{
			base.Show();

			StartAnimation();
		}

		/// <summary>
		/// Starts the animation.
		/// </summary>
		void StartAnimation()
		{
			var views = View.Subviews;
			InvokeInBackground(() => {
				foreach (var view in views)
				{
					InvokeOnMainThread(() => {
						view.SetStyleClass("bubble");
					});
					Thread.Sleep(2000);
				}
			});
		}
	}
}