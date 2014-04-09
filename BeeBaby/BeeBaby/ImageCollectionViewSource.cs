﻿using System;
using MonoTouch.UIKit;
using Application;
using MonoTouch.Foundation;
using BeeBaby.ViewModels;
using System.Collections.Generic;
using BeeBaby.ResourcesProviders;

namespace BeeBaby
{
	public class ImageCollectionViewSource : UICollectionViewSource
	{
		private static NSString s_cellIdentifier = new NSString("GalleryCell");
		private UIViewController m_viewController;
		private ImageProvider m_imageProvider;
		private IList<ImageViewModel> m_images;

		public ImageCollectionViewSource(UIViewController viewController)
		{
			m_viewController = viewController;
			m_imageProvider = new ImageProvider();
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
			CollectionViewCell cell = (CollectionViewCell) collectionView.DequeueReusableCell(s_cellIdentifier, indexPath);
			cell.GetImagePhoto().Image = image.Image;
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
			CollectionViewCell cell = (CollectionViewCell) collectionView.CellForItem(indexPath);
			cell.IsSelected = !cell.IsSelected;
			cell.UpdateStatus();

			if (cell.IsSelected) {
				CurrentContext.Instance.Moment.SelectedMediaNames.Add(cell.MediaName);
			} else {
				CurrentContext.Instance.Moment.SelectedMediaNames.Remove(cell.MediaName);
			}
		}

		/// <summary>
		/// Reloads the data collection view.
		/// </summary>
		/// <param name="collectionView">Collection view.</param>
		public void ReloadData(UICollectionView collectionView)
		{
			m_images = m_imageProvider.GetImagesForCurrentMoment(true);
			collectionView.ReloadData();
		}
	}
}