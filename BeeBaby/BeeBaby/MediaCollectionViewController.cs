using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BeBabby.ResourcesProviders;
using System.Collections.Generic;

namespace BeBabby
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
		partial void btnAddMediaFromLibrary(UIBarButtonItem sender)
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
		partial void btnNextStep(UIBarButtonItem sender)
		{
			var imageNames = GetSelectedImagesNames();

			//TODO: Remover quando a seleção de imagens estiver correta.
			for (int i = 0; i < imageNames.Count; i++) {
				imageNames[i] = imageNames[i].Split('/').Last();
			}
				
			m_imageProvider.SavePermanentImages(imageNames);

			PerformSegue("segueEventStep", sender);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			CollectionView.RegisterClassForCell(typeof(CollectionViewCell), s_cellId);

		}

		public override void ViewWillAppear(bool animated)
		{
			m_images = m_imageProvider.GetImagesForCurrentMoment();
				
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
