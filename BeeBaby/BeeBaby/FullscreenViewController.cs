using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using BeeBaby.ResourcesProviders;
using MonoTouch.FacebookConnect;
using Skahal.Infrastructure.Framework.Globalization;
using Domain.Baby;
using Domain.Media;
using System.IO;
using System.Drawing;
using System.Linq;
using BeeBaby.Activity;

namespace BeeBaby
{
	public partial class FullscreenViewController : BaseViewController
	{
		UIImage m_photo;
		Moment m_moment;
		UIActivityViewController m_activityViewController;

		public FullscreenViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			FlurryAnalytics.Flurry.LogEvent("Fullscreen Foto: Abriu.");

			base.ViewDidLoad();

			AddSingleTapGestureRecognizer();
		}

		/// <summary>
		/// Adds the single tap gesture recognizer.
		/// </summary>
		void AddSingleTapGestureRecognizer()
		{
			var singleTap = new UITapGestureRecognizer(() =>
			{
				ShowOrHideSubviews();
			});
			singleTap.NumberOfTapsRequired = 1;
			View.AddGestureRecognizer(singleTap);
		}

		/// <summary>
		/// Show or hide subviews.
		/// </summary>
		void ShowOrHideSubviews()
		{
			foreach (var view in View.Subviews)
			{
				if (view.GetType() != typeof(UIScrollView))
				{
					view.Hidden = !view.Hidden;
				}
			}
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
			ShowProgressWhilePerforming(() =>
			{
				PresentingViewController.DismissViewController(false, null);
				var photo = imgPhoto.Image;
				InvokeInBackground(() =>
				{
					photo.Dispose();
					m_photo.Dispose();
				});
			}, false);
		}

		/// <summary>
		/// Share the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Share(UIButton sender)
		{
			ShowProgressWhilePerforming(() =>
			{
				var path = GetImagePath();
				CreateActivityViewController(path);

				m_activityViewController.CompletionHandler += (activityTitle, close) =>
				{
					if (activityTitle.ToString().Equals("com.beebaby.facebook"))
					{
						PublishOnFacebook(imgPhoto.Image);
					}
				};

				PresentViewController(m_activityViewController, true, null);

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
			imgPhoto.Image = photo;
			lblAge.Text = Baby.FormatAge(baby.BirthDateTime, moment.Date);

			lblEvent.Text = moment.Event.Description;

			m_photo = photo;
			m_moment = moment;
		}

		/// <summary>
		/// Saves image to photo album.
		/// </summary>
		void SaveToPhotoAlbum()
		{
			if (imgPhoto.Image == m_photo)
			{
				imgPhoto.Image = new ImageProvider().CreateImageForShare(m_photo, m_moment);
				imgPhoto.Image.SaveToPhotosAlbum(null);
			}
		}

		/// <summary>
		/// Publishs the on facebook.
		/// </summary>
		/// <param name="imgPhoto">Image photo.</param>
		public void PublishOnFacebook(UIImage imgPhoto)
		{
			if (FBDialogs.CanPresentOSIntegratedShareDialog(FBSession.ActiveSession))
			{
				FlurryAnalytics.Flurry.LogEvent("Fullscreen Foto: Compartilhou foto no Facebook.");
				FBDialogs.PresentOSIntegratedShareDialogModally(this, null, imgPhoto, null, (result, error) =>
				{
					if (error != null)
					{
						InvokeOnMainThread(() => new UIAlertView("Warning".Translate(), error.Description, null, "OK", null).Show());
					}
					else if (result == FBOSIntegratedShareDialogResult.Succeeded)
					{
						InvokeOnMainThread(() => new UIAlertView("Information".Translate(), "SharedMomentFacebook".Translate(), null, "OK", null).Show());
					}
				});
			}
			else
			{
				Console.WriteLine("Não foi possível compartilhar o momento no Facebook.");
			}
		}




		void CreateActivityViewController(string path)
		{
			m_activityViewController = new UIActivityViewController(
				new NSObject[]
				{
					new NSUrl(path)
				}, new UIActivity[]
				{
					new FacebookActivity(imgPhoto.Image)
				}
			)
			{
				ExcludedActivityTypes = new[]
				{
					UIActivityType.AssignToContact
				}
			};
		}

		string GetImagePath()
		{
			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Guid.NewGuid() + ".jpg");
			using (NSData imageData = imgPhoto.Image.AsJPEG(MediaBase.ImageCompressionQuality))
			{
				NSError err;
				if (!imageData.Save(path, false, out err))
				{
					Console.WriteLine("Saving of file failed: " + err.Description);
				}
			}
			return path;
		}
	}
}