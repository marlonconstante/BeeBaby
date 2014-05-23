using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using BeeBaby.ResourcesProviders;
using MonoTouch.FacebookConnect;
using Skahal.Infrastructure.Framework.Globalization;
using Domain.Baby;

namespace BeeBaby
{
	public partial class FullscreenViewController : BaseViewController
	{
		Moment m_moment;

		public FullscreenViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Gets the supported interface orientations.
		/// </summary>
		/// <returns>The supported interface orientations.</returns>
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.All;
		}

		/// <summary>
		/// Determines whether this instance is show status bar.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		public override bool IsShowStatusBar()
		{
			return false;
		}

		/// <summary>
		/// Close the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Close(UIButton sender)
		{
			ShowProgressWhilePerforming(() => {
				DismissViewController(true, null);
			}, false);
		}

		/// <summary>
		/// Share the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Share(UIButton sender)
		{
			ShowProgressWhilePerforming(() => {
				var image = new ImageProvider().CreateImageForShare(imgPhoto.Image, m_moment);
				imgPhoto.Image = image;

				bool ios6ShareDialog = FBDialogs.CanPresentOSIntegratedShareDialog(FBSession.ActiveSession);
				if (ios6ShareDialog)
				{
					FBDialogs.PresentOSIntegratedShareDialogModally(UIApplication.SharedApplication.Windows[0].RootViewController
						,null, image, null, (result, error) => {
							if (error != null)
								InvokeOnMainThread(() => new UIAlertView("Error", error.Description, null, "Ok", null).Show());
							else if (result == FBOSIntegratedShareDialogResult.Succeeded)
								InvokeOnMainThread(() => new UIAlertView("Success".Translate(), "Moment successfully posted to Facebook".Translate(), null, "Ok", null).Show());
						});
				}
				else
				{
					Console.WriteLine("Erro ao dar Share no Facebook.");
				}
			});
		}

		/// <summary>
		/// Sets the information.
		/// </summary>
		/// <param name="moment">Moment.</param>
		/// <param name="baby">Baby.</param>
		/// <param name="photo">Photo.</param>
		public void SetInformation(Moment moment, Baby baby, UIImage photo)
		{
			m_moment = moment;

			imgPhoto.Image = photo;
			lblAge.Text = baby.AgeInWords;
			lblEvent.Text = moment.Event.Description;
		}
	}
}