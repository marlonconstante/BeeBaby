using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BeeBaby.ResourcesProviders;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public partial class MediaCollectionViewController : UICollectionViewController
	{
		UIImagePickerController m_picker;
		IList<UIImage> m_images;
		static NSString s_cellId = new NSString("GalleryCell");
		ImageProvider m_imageProvider;

		public MediaCollectionViewController(IntPtr handle) : base(handle)
		{
			m_imageProvider = new ImageProvider();
		}

		/// <summary>
		/// Buttons the add media from library.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void AddMediaFromLibrary(UIBarButtonItem sender)
		{
			var imagePickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.SavedPhotosAlbum);
			m_picker = imagePickerProvider.GetUIImagePickerController();

			PresentViewController(m_picker, false, null);
		}

		/// <summary>
		/// Gets the selected images names.
		/// </summary>
		/// <returns>The selected images names.</returns>
		IList<string> GetSelectedImagesNames()
		{
			return m_imageProvider.GetTemporaryImagesNamesForCurrentMoment();
		}

		/// <summary>
		/// Buttons the next step.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void NextStep(UIBarButtonItem sender)
		{
			var imageNames = GetSelectedImagesNames();

			//TODO: Remover quando a seleção de imagens estiver correta.
			for (int i = 0; i < imageNames.Count; i++)
			{
				imageNames[i] = imageNames[i].Split('/').Last();
			}
				
			m_imageProvider.SavePermanentImages(imageNames);

			PerformSegue("segueEventStep", sender);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			CollectionView.RegisterClassForCell(typeof(CollectionViewCell), s_cellId);

			btnNextStep.Title = "Next".Translate();
			btnAddMediaFromLibrary.Title = "AddFromLib".Translate();
		}

		public override void ViewWillAppear(bool animated)
		{
			m_images = m_imageProvider.GetImagesForCurrentMoment(true);
				
			this.CollectionView.ReloadData();

			base.ViewWillAppear(animated);
		}

		public override int NumberOfSections(UICollectionView collectionView)
		{
			return 1;
		}

		public override int GetItemsCount(UICollectionView collectionView, int section)
		{
			return m_images.Count;
		}

		public override UICollectionViewCell GetCell(UICollectionView collectionView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			CollectionViewCell cell = (CollectionViewCell)collectionView.DequeueReusableCell(s_cellId, indexPath);

			if (cell == null)
			{
				cell = new CollectionViewCell(base.Handle);
			}

			cell.BackgroundColor = UIColor.White;
			cell.Image = m_images[indexPath.Item];

			return cell;
		}
	}
}
