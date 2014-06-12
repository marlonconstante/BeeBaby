using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BeeBaby.ResourcesProviders;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Globalization;
using Application;
using BeeBaby.ViewModels;
using Domain.Moment;
using System.Drawing;
using BeeBaby.Util;
using Domain.Baby;

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

			UpdateImageCollectionView();
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			TitleScreen = "ChoosePhotos".Translate();
			btnAddMediaFromLibrary.SetTitle("Albums".Translate(), UIControlState.Normal);
			btnNextStep.SetTitle("WantThese".Translate(), UIControlState.Normal);
		}

		/// <summary>
		/// Lefts the bar button frame.
		/// </summary>
		/// <returns>The bar button frame.</returns>
		public override RectangleF LeftBarButtonFrame()
		{
			return new RectangleF(0f, 0f, 22f, 22f);
		}

		/// <summary>
		/// Lefts the bar button action.
		/// </summary>
		public override void LeftBarButtonAction()
		{
			ShowProgressWhilePerforming(() => {
				CurrentContext.Instance.Moment = new MomentService().CreateMoment();

				UpdateImageCollectionView(true);
			});
		}

		/// <summary>
		/// Lefts the bar button style class.
		/// </summary>
		/// <returns>The bar button style class.</returns>
		public override string LeftBarButtonStyleClass()
		{
			return "cancel";
		}

		/// <summary>
		/// Buttons the add media from library.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void AddMediaFromLibrary(UIButton sender)
		{
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
					if (CurrentContext.Instance.CurrentBaby == null)
					{
						var baby = new BabyService().CreateBaby();

						CurrentContext.Instance.CurrentBaby = baby;
						PreferencesEditor.SaveLastUsedBaby(baby.Id);

						PerformSegue("segueBaby", sender);
					}
					else
					{
						CurrentContext.Instance.Moment.Babies.Add(CurrentContext.Instance.CurrentBaby);
						new MomentService().SaveMoment(CurrentContext.Instance.Moment);
						PerformSegue("segueSelectEvent", sender);
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
				m_collectionViewSource = new ImageCollectionViewSource(this);
				clnView.Source = m_collectionViewSource;
			}
			m_collectionViewSource.ReloadData(clnView);
		}
	}
}
