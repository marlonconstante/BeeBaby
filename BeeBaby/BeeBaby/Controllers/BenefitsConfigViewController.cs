using System;

using Foundation;
using UIKit;
using CoreGraphics;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using BeeBaby.Navigations;
using Parse;
using System.Drawing;

namespace BeeBaby.Controllers
{
	public partial class BenefitsConfigViewController : NavigationViewController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Controllers.BenefitsConfigViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public BenefitsConfigViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did layout subviews.
		/// </summary>
		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			if (scrView.ContentSize == CGSize.Empty)
			{
				scrView.ContentSize = new CGSize(320f, 504f);
			}
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			lblThankYou.Text = "ThankYou".Translate();
			lblAdvantages.Text = "EternalizeMomentsAndShare".Translate();
			lblObservations.Text = "SynchronizeMoments".Translate();
			btnContinue.SetTitle("Continue".Translate(), UIControlState.Normal);
			btnLogOut.SetTitle("LogOut".Translate(), UIControlState.Normal);
		}

		/// <summary>
		/// Lefts the bar button frame.
		/// </summary>
		/// <returns>The bar button frame.</returns>
		public override CGRect LeftBarButtonFrame()
		{
			return CGRect.Empty;
		}

		/// <summary>
		/// Lefts the bar button action.
		/// </summary>
		public override void LeftBarButtonAction()
		{
		}

		/// <summary>
		/// Continue the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Continue(UIButton sender)
		{
			Close();
		}

		/// <summary>
		/// Logs the out.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void LogOut(UIButton sender)
		{
			Close(() => ParseUser.LogOut());
		}

		/// <summary>
		/// Close the specified beforeAction.
		/// </summary>
		/// <param name="beforeAction">Before action.</param>
		void Close(Action beforeAction = null)
		{
			ShowProgressWhilePerforming(() => {
				if (beforeAction != null)
				{
					beforeAction();
				}

				((INavigationController) NavigationController).Close();
			}, false);
		}
	}
}