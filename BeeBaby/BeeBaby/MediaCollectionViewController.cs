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
		private static NSString s_cellIdentifier = new NSString("GalleryCell");
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
			PerformSegue("segueEventStep", sender);
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			btnNextStep.Title = "Next".Translate();
			btnAddMediaFromLibrary.Title = "AddFromLib".Translate();
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			m_images = m_imageProvider.GetImagesForCurrentMoment(true);
				
			this.CollectionView.ReloadData();

			base.ViewWillAppear(animated);
		}

		/// <summary>
		/// Returns the number of sections.
		/// </summary>
		/// <param name="collectionView">Collection view.</param>
		public override int NumberOfSections(UICollectionView collectionView)
		{
			return 1;
		}

		/// <summary>
		/// Returns the items count.
		/// </summary>
		/// <param name="collectionView">Collection view.</param>
		/// <param name="section">Section.</param>
		public override int GetItemsCount(UICollectionView collectionView, int section)
		{
			return m_images.Count;
		}

		/// <summary>
		/// Event responsible for returning the cell from the index.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="collectionView">Collection view.</param>
		/// <param name="indexPath">Index path.</param>
		public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
		{
			var image = m_images[indexPath.Item];

			// Request a recycled cell to save memory
			CollectionViewCell cell = (CollectionViewCell)collectionView.DequeueReusableCell(s_cellIdentifier, indexPath);
			cell.GetImagePhoto().Image = image.Image;
			cell.MediaName = image.FileName;
			return cell;
		}

		/// <summary>
		/// Event item selection cell.
		/// </summary>
		/// <param name="collectionView">Collection view.</param>
		/// <param name="indexPath">Index path.</param>
		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			CollectionViewCell cell = (CollectionViewCell)collectionView.CellForItem(indexPath);
			cell.UpdateStatus();

			if (cell.IsSelected) {
				CurrentContext.Instance.Moment.SelectedMediaNames.Add(cell.MediaName);
			} else {
				CurrentContext.Instance.Moment.SelectedMediaNames.Remove(cell.MediaName);
			}
		}
	}
}
