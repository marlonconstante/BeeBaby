using System;
using MonoTouch.UIKit;
using BeeBaby.ResourcesProviders;
using Skahal.Infrastructure.Framework.Globalization;
using Application;
using Domain.Moment;
using System.Drawing;

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
		/// Determines whether this instance is add right bar button item.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		public override bool IsAddRightBarButtonItem()
		{
			return new MomentService().HasValidMoments();
		}

		/// <summary>
		/// Rights the bar button action.
		/// </summary>
		public override void RightBarButtonAction()
		{
			PresentingViewController.DismissViewController(false, null);
			Discard.ReleaseNavigation(NavigationController);
		}

		/// <summary>
		/// Rights the bar button style class.
		/// </summary>
		/// <returns>The bar button style class.</returns>
		public override string RightBarButtonStyleClass()
		{
			return "tree-clear";
		}

		/// <summary>
		/// Buttons the add media from library.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void AddMediaFromLibrary(UIButton sender)
		{
			FlurryAnalytics.Flurry.LogEvent("Camera: Abriu o album do iPhone.");

			var imagePickerDelegate = new MomentImagePickerDelegate(CurrentContext.Instance.Moment);
			var mediaPickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.SavedPhotosAlbum, imagePickerDelegate);
			var picker = mediaPickerProvider.GetUIImagePickerController();

			PresentViewController(picker, false, null);
		}

		/// <summary>
		/// Buttons the next step.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void NextStep(UIButton sender)
		{
			if (CurrentContext.Instance.Moment.SelectedMediaNames.Count == 0)
			{
				new UIAlertView("IllustrateMoment".Translate(), "TakePictureOrImportAlbum".Translate(), null, "GotIt".Translate(), null).Show();
			}
			else
			{
				ShowProgressWhilePerforming(() => {
					if (CurrentContext.Instance.CurrentBaby.IsValid())
					{
						CurrentContext.Instance.Moment.Babies.Add(CurrentContext.Instance.CurrentBaby);
						new MomentService().SaveMoment(CurrentContext.Instance.Moment);
						PerformSegue("segueSelectEvent", sender);
					}
					else
					{
						PerformSegue("segueBaby", sender);
					}
				}, false);
			}
		}

		/// <summary>
		/// Updates the image collection view.
		/// </summary>
		/// <param name="reset">If set to <c>true</c> reset.</param>
		void UpdateImageCollectionView(bool reset = false)
		{
			if (reset || m_collectionViewSource == null)
			{
				if (reset)
					FlurryAnalytics.Flurry.LogEvent("Escolher Fotos: Resetou a seleção.");

				m_collectionViewSource = new ImageCollectionViewSource(this);
				clnView.Source = m_collectionViewSource;
			}
			m_collectionViewSource.ReloadData(clnView);
		}
	}
}
