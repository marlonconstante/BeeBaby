using System;
using UIKit;
using Application;
using Foundation;
using BeeBaby.ViewModels;
using System.Collections.Generic;
using BeeBaby.ResourcesProviders;

namespace BeeBaby.VisualElements
{
	public class ImageCollectionViewSource : CollectionViewSource
	{
		const string s_cellIdentifier = "GalleryCell";
		UIViewController m_viewController;
		ImageProvider m_imageProvider;
		IList<ImageModel> m_images;

		public ImageCollectionViewSource(UIViewController viewController)
		{
			m_viewController = viewController;
			m_imageProvider = new ImageProvider(CurrentContext.Instance.Moment.Id);
		}

		/// <summary>
		/// Returns the number of sections.
		/// </summary>
		/// <param name="collectionView">Collection view.</param>
		public override nint NumberOfSections(UICollectionView collectionView)
		{
			return 1;
		}

		/// <summary>
		/// Returns the items count.
		/// </summary>
		/// <param name="collectionView">Collection view.</param>
		/// <param name="section">Section.</param>
		public override nint GetItemsCount(UICollectionView collectionView, nint section)
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
			ImageCollectionViewCell cell = (ImageCollectionViewCell) collectionView.DequeueReusableCell(new NSString(s_cellIdentifier), indexPath);
			cell.Photo = image.Image;
			cell.MediaName = image.FileName;
			cell.IsSelected = CurrentContext.Instance.Moment.SelectedMediaNames.Contains(image.FileName);
			cell.UpdateStatus();

			return cell;
		}

		/// <summary>
		/// Event item selection cell.
		/// </summary>
		/// <param name="collectionView">Collection view.</param>
		/// <param name="indexPath">Index path.</param>
		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			ImageCollectionViewCell cell = (ImageCollectionViewCell) collectionView.CellForItem(indexPath);
			cell.IsSelected = !cell.IsSelected;
			cell.UpdateStatus();

			if (cell.IsSelected) {
				CurrentContext.Instance.Moment.SelectedMediaNames.Add(cell.MediaName);
			} else {
				CurrentContext.Instance.Moment.SelectedMediaNames.Remove(cell.MediaName);
			}
		}

		/// <summary>
		/// Reloads the data.
		/// </summary>
		/// <param name="collectionView">Collection view.</param>
		/// <param name="selectAll">If set to <c>true</c> select all.</param>
		public void ReloadData(UICollectionView collectionView, bool selectAll = false)
		{
			m_images = m_imageProvider.GetImages(true);
			collectionView.ReloadData();

			if (selectAll)
			{
				var moment = CurrentContext.Instance.Moment;
				moment.SelectedMediaNames.Clear();

				foreach (var image in m_images)
				{
					moment.SelectedMediaNames.Add(image.FileName);
				}
			}
		}
	}
}