using System;
using MonoTouch.UIKit;
using BeeBaby.ResourcesProviders;
using Skahal.Infrastructure.Framework.Globalization;
using Application;
using System.Drawing;
using Domain.Moment;
using ELCPicker;
using System.Diagnostics;

namespace BeeBaby
{
	public partial class MediaViewController : NavigationViewController
	{
		ImageCollectionViewSource m_collectionViewSource;

		public MediaViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			FlurryAnalytics.Flurry.LogEvent("Escolher Fotos: Entrou na tela.", true);

			UpdateImageCollectionView();
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			FlurryAnalytics.Flurry.EndTimedEvent("Escolher Fotos: Entrou na tela.", null);

			base.ViewWillDisappear(animated);
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			TitleScreen = "ChoosePhotos".Translate();
			btnAddMediaFromLibrary.SetTitle("ImportPhotos".Translate(), UIControlState.Normal);
			btnNextStep.SetTitle("WantThese".Translate(), UIControlState.Normal);
		}

		/// <summary>
		/// Buttons the add media from library.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void AddMediaFromLibrary(UIButton sender)
		{
			FlurryAnalytics.Flurry.LogEvent("Camera: Abriu o album do iPhone.");

//			var imagePickerDelegate = new MomentImagePickerDelegate(CurrentContext.Instance.Moment);
//			var mediaPickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.SavedPhotosAlbum, imagePickerDelegate);
//			var picker = mediaPickerProvider.GetUIImagePickerController();
//

			var picker = ELCImagePickerViewController.Instance;
			picker.MaximumImagesCount = 15;
			picker.Completion.ContinueWith(t =>
			{
				if (t.IsCanceled || t.Exception != null)
				{
					picker.Dismiss();
					//picker.DismissViewController(true, null);
					// no pictures for you!
					Debug.WriteLine("IsCanceled");
				}
				else
				{
					Debug.WriteLine("OK");
					picker.Dismiss();

					// t.Result is a List<AssetResult>
				}
			});
			 
			PresentViewController(picker, true, null);
		}

		/// <summary>
		/// Buttons the next step.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void NextStep(UIButton sender)
		{
			if (CurrentContext.Instance.Moment.SelectedMediaNames.Count == 0)
			{
				new UIAlertView("IllustrateMoment".Translate(), (IsMediaFlow() ? "ImportAlbum" : "TakePictureOrImportAlbum").Translate(), null, "GotIt".Translate(), null).Show();
			}
			else
			{
				ShowProgressWhilePerforming(() =>
				{
					if (IsMediaFlow())
					{
						var moment = CurrentContext.Instance.Moment;
						moment.MediaCount = moment.SelectedMediaNames.Count;

						((MomentNavigationController)NavigationController).SaveCurrentMoment();
					}
					else
					{
						PerformSegue("segueSelectEvent", sender);
					}
				}, false);
			}
		}

		/// <summary>
		/// Updates the image collection view.
		/// </summary>
		void UpdateImageCollectionView()
		{
			bool initialize = m_collectionViewSource == null;
			if (initialize)
			{
				m_collectionViewSource = new ImageCollectionViewSource(this);
				clnView.Source = m_collectionViewSource;
			}
			m_collectionViewSource.ReloadData(clnView, initialize && IsMediaFlow());
		}
	}
}
