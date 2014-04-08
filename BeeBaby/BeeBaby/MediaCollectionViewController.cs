using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BeeBaby.ResourcesProviders;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Globalization;
using Application;
using BeeBaby.ViewModels;

namespace BeeBaby
{
	public partial class MediaCollectionViewController : UICollectionViewController
	{
		private static NSString m_cellIdentifier = new NSString("GalleryCell");
		private UIImagePickerController m_picker;
		private IList<ImageViewModel> m_images;
		private ImageProvider m_imageProvider;

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
		/// Buttons the next step.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void NextStep(UIBarButtonItem sender)
		{
//			var imageNames = GetSelectedImagesNames();
//
//			//TODO: Remover quando a seleção de imagens estiver correta.
//			for (int i = 0; i < imageNames.Count; i++)
//			{
//				imageNames[i] = imageNames[i].Split('/').Last();
//			}
//				
//			m_imageProvider.SavePermanentImages(imageNames);

			PerformSegue("segueEventStep", sender);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

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

		public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
		{
			var imageView = m_images [indexPath.Item];

			// Request a recycled cell to save memory
			CollectionViewCell cell = (CollectionViewCell) collectionView.DequeueReusableCell(m_cellIdentifier, indexPath);
			cell.GetImagePhoto().Image = imageView.Image;
			cell.MediaName = imageView.FileName;
			return cell;
		}

		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			CollectionViewCell cell = (CollectionViewCell) collectionView.CellForItem(indexPath);
			cell.UpdateStatus();

			if (cell.IsSelected) {
				CurrentContext.Instance.Moment.SelectedMediaPaths.Add(cell.MediaName);
			} else {
				CurrentContext.Instance.Moment.SelectedMediaPaths.Remove(cell.MediaName);
			}
		}
	}
}
