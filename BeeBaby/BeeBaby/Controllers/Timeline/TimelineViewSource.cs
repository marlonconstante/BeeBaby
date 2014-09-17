using System;
using MonoTouch.UIKit;
using Domain.Moment;
using System.Collections.Generic;
using MonoTouch.Foundation;
using BeeBaby.ResourcesProviders;
using Domain.Media;
using System.Drawing;
using Domain.Baby;
using BeeBaby.ViewModels;
using Skahal.Infrastructure.Framework.Globalization;
using Application;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using BeeBaby.Util;

namespace BeeBaby
{
	public class TimelineViewSource : TableViewSource
	{
		const string s_cellIdentifier = "MomentCell";
		TimelineViewController m_viewController;
		IList<IMoment> m_tableItems;
		Baby m_baby;
		FullscreenViewController m_fullscreenController;

		public TimelineViewSource(TimelineViewController viewController, IList<IMoment> moments, Baby baby)
		{
			m_viewController = viewController;
			m_tableItems = moments;
			m_baby = baby;

			LoadFullscreenViewController();
		}

		/// <summary>
		/// Loads the fullscreen view controller.
		/// </summary>
		void LoadFullscreenViewController()
		{
			if (m_fullscreenController == null)
			{
				var board = UIStoryboard.FromName("MainStoryboard", null);
				m_fullscreenController = (FullscreenViewController) board.InstantiateViewController("FullscreenViewController");
			}
		}

		/// <summary>
		/// Moments at indexPath.
		/// </summary>
		/// <returns>The <see cref="Domain.Moment.Moment"/>.</returns>
		/// <param name="indexPath">Index path.</param>
		public IMoment MomentAt(NSIndexPath indexPath)
		{
			return m_tableItems[indexPath.Row];
		}

		/// <summary>
		/// Removes the row.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public void RemoveRow(UITableView tableView, NSIndexPath indexPath)
		{
			var moment = MomentAt(indexPath) as Moment;

			new MomentService().RemoveMoment(moment);
			new ImageProvider(moment.Id).DeleteFiles(false);

			m_tableItems.RemoveAt(indexPath.Row);
			tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
		}

		/// <Docs>Table view displaying the rows.</Docs>
		/// <summary>
		/// Rowses the in section.
		/// </summary>
		/// <returns>The in section.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="section">Section.</param>
		public override int RowsInSection(UITableView tableView, int section)
		{
			return m_tableItems.Count;
		}

		/// <Docs>Scroll view where the scrolling occurred.</Docs>
		/// <summary>
		/// Scrolled the specified scrollView.
		/// </summary>
		/// <param name="scrollView">Scroll view.</param>
		public override void Scrolled(UIScrollView scrollView)
		{
			m_viewController.HidePopover();
		}

		/// <Docs>Table view requesting the cell.</Docs>
		/// <summary>
		/// Gets the cell.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(s_cellIdentifier) as TimelineMomentCell;
			ReleasePhotos(cell);
			UpdateMomentCell(tableView, cell, indexPath);
			return cell;
		}

		/// <summary>
		/// Updates the moment cell.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="cell">Cell.</param>
		/// <param name="indexPath">Index path.</param>
		void UpdateMomentCell(UITableView tableView, TimelineMomentCell cell, NSIndexPath indexPath)
		{
			var moment = MomentAt(indexPath);

			cell.LabelAge = string.Concat("AtAge".Translate(), " ", Baby.FormatAge(m_baby.BirthDateTime, moment.MomentDate));
			cell.LabelDate = string.Concat("InDate".Translate(), " ", moment.MomentDate.ToString("LongDateMask".Translate(), System.Globalization.DateTimeFormatInfo.CurrentInfo));
			cell.LabelEventName = moment.EventDescription;
			cell.LabelWhere = string.Concat("At".Translate(), " ", Location.NameOrDefault(moment.LocationName));

			var scrollWidth = moment.MomentMediaCount * MediaBase.ImageThumbnailSize;
			cell.ViewPhotos.ContentSize = new SizeF(scrollWidth, MediaBase.ImageThumbnailSize);

			cell.HasDescription = !string.IsNullOrEmpty(moment.MomentDescription);
			cell.HasOptions = moment is Moment;
			cell.IncreaseOptionsTouchArea();

			InvokeInBackground(() =>
			{
				Thread.Sleep(150);

				var imageProvider = new ImageProvider(moment.MomentId);
				IList<ImageModel> images = imageProvider.getMomentImages(moment, true);

				InvokeOnMainThread(() =>
				{
					if (IsVisibleRow(tableView, indexPath))
					{
						cell.LoadStyles(moment.EventTagName);
						cell.ViewPhotos.AddSubviews(GetPhotos(moment, images));
					}
				});
			});
		}

		/// <summary>
		/// Gets the photos.
		/// </summary>
		/// <returns>The photos.</returns>
		/// <param name="moment">Moment.</param>
		/// <param name="images">Images.</param>
		MomentImageView[] GetPhotos(IMoment moment, IList<ImageModel> images)
		{
			var photos = new List<MomentImageView>();

			foreach (var image in images)
			{
				var imageView = new MomentImageView(new RectangleF(photos.Count * MediaBase.ImageThumbnailSize, 0f, MediaBase.ImageThumbnailSize, MediaBase.ImageThumbnailSize));

				imageView.Moment = moment;
				imageView.ItemIndex = photos.Count;
				imageView.Image = image.Image;
				imageView.UserInteractionEnabled = true;
				imageView.MultipleTouchEnabled = true;
				imageView.ContentMode = UIViewContentMode.Center;
				imageView.Opaque = true;

				var proxy = new EventProxy<TimelineViewSource, EventArgs>(this);
				proxy.Action = (target, sender, args) =>
				{
					ActionProgress actionProgress = new ActionProgress(() =>
					{
						var momentImageView = (MomentImageView) sender;
						target.m_viewController.PresentViewController(target.m_fullscreenController, true, null);
						target.m_fullscreenController.SetInformation(momentImageView.Moment, momentImageView.ItemIndex);
					}, false);
					actionProgress.Execute();
				};
				imageView.Clicked += proxy.HandleEvent;

				photos.Add(imageView);
			}

			return photos.ToArray();
		}

		/// <summary>
		/// Releases the photos.
		/// </summary>
		/// <param name="cell">Cell.</param>
		void ReleasePhotos(TimelineMomentCell cell)
		{
			foreach (var view in cell.ViewPhotos.Subviews)
			{
				Discard.ReleaseSubviews(view);
			}
		}
	}
}