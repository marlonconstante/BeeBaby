using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BeeBaby.Controllers;
using PixateFreestyleLib;
using BeeBaby.VisualElements;
using Skahal.Infrastructure.Framework.Globalization;
using System.Threading;

namespace BeeBaby.Controllers
{
	public partial class OnBoardingModalViewController : ModalViewController
	{
		public OnBoardingModalViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Loads the view.
		/// </summary>
		public override void LoadView()
		{
			base.LoadView();

			TranslateViewLabels(vwEditMoment, "EditMomentTitle", "EditMomentText");
			TranslateViewLabels(vwViewAndShare, "ViewAndShareTitle", "ViewAndShareText");
			TranslateViewLabels(vwLetsStart, "LetsStartTitle", "LetsStartText");
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
			InvokeInBackground(() => {
				Thread.Sleep(1000);
				var views = new UIView[] { vwEditMoment, vwViewAndShare, vwLetsStart };
				foreach (var view in views)
				{
					InvokeOnMainThread(() => {
						view.SetStyleClass("bubble");
					});
					Thread.Sleep(2000);
				}
			});
		}

		/// <summary>
		/// Translates the view labels.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="keyTitle">Key title.</param>
		/// <param name="keyText">Key text.</param>
		void TranslateViewLabels(UIView view, string keyTitle, string keyText)
		{
			var labelTitle = view.Subviews[0] as Label;
			var labelText = view.Subviews[1] as Label;

			labelTitle.Text = keyTitle.Translate();
			labelText.Text = keyText.Translate();
		}
	}
}