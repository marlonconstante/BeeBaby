using System;
using UIKit;
using Domain.Moment;
using System.Collections.Generic;
using Foundation;
using BeeBaby.ResourcesProviders;
using Domain.Media;
using CoreGraphics;
using Domain.Baby;
using BeeBaby.ViewModels;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using Application;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using BeeBaby.Util;
using BeeBaby.Proxy;
using BeeBaby.Progress;
using BeeBaby.VisualElements;
using Domain.Synchronization;

namespace BeeBaby.Controllers
{
	public class TimelineViewSource : TableViewSource
	{
		const string s_cellIdentifier = "MomentCell";
		const int s_maxMomentCacheSize = 15;

		TimelineViewController m_viewController;
		FullscreenViewController m_fullscreenController;

		IList<IMoment> m_moments;
		Baby m_baby;

		IDictionary<string, UIImage> m_tagIcons = new Dictionary<string, UIImage>();
		SortedDictionary<int, IList<ImageModel>> m_momentPhotos = new SortedDictionary<int, IList<ImageModel>>();

		public TimelineViewSource(TimelineViewController viewController)
		{
			m_viewController = viewController;

			LoadTagIcons();
			LoadFullscreenViewController();
		}

		/// <summary>
		/// Loads the tag icons.
		/// </summary>
		void LoadTagIcons()
		{
			var names = Enum.GetNames(typeof(TagType)).Select((tag) => tag.ToLower()).ToList();
			names.Add("firsts");

			foreach (var name in names)
			{
				var image = UIImage.FromFile(string.Format("{0}-round.png", name));
				m_tagIcons.Add(name, image);
			}
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
		/// Load the moment photos.
		/// </summary>
		/// <param name="imageViews">Image views.</param>
		void LoadMomentPhotos(List<MomentImageView> imageViews)
		{
			var moment = imageViews.FirstOrDefault().Moment;
			var key = m_moments.IndexOf(moment);
			if (!m_momentPhotos.ContainsKey(key))
			{
				Thread.Sleep(300);

				if (m_momentPhotos.Count >= s_maxMomentCacheSize)
				{
					var firstKey = m_momentPhotos.Keys.First();
					var lastKey = m_momentPhotos.Keys.Last();
					var targetKey = Math.Abs(firstKey - key) > Math.Abs(lastKey - key) ? firstKey : lastKey;

					RemoveMomentPhotos(targetKey);
				}

				var imageProvider = new ImageProvider(moment.MomentId);
				m_momentPhotos.Add(key, imageProvider.getMomentImages(moment, true));
			}

			SetMomentPhotos(key, imageViews);
		}

		/// <summary>
		/// Set the moment photos.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="imageViews">Image views.</param>
		void SetMomentPhotos(int key, List<MomentImageView> imageViews)
		{
			IList<ImageModel> images;
			if (m_momentPhotos.TryGetValue(key, out images))
			{
				foreach (var imageView in imageViews)
				{
					InvokeOnMainThread(() => {
						var activityIndicator = imageView.Subviews[0] as UIActivityIndicatorView;
						activityIndicator.StopAnimating();

						if (images.Count > imageView.ItemIndex)
						{
							imageView.Image = images[imageView.ItemIndex].Image;
						}
					});
					Thread.Sleep(100);
				}
			}
		}

		/// <summary>
		/// Remove the moment photos.
		/// </summary>
		/// <param name="key">Key.</param>
		void RemoveMomentPhotos(int key)
		{
			ReleaseMomentPhotos(key);
			m_momentPhotos.Remove(key);
		}

		/// <summary>
		/// Clear the moment photos.
		/// </summary>
		void ClearMomentPhotos()
		{
			foreach (var key in m_momentPhotos.Keys)
			{
				ReleaseMomentPhotos(key);
			}
			m_momentPhotos.Clear();
		}

		/// <summary>
		/// Release the moment photos.
		/// </summary>
		/// <param name="key">Key.</param>
		void ReleaseMomentPhotos(int key)
		{
			IList<ImageModel> images;
			if (m_momentPhotos.TryGetValue(key, out images))
			{
				Discard.ReleaseFields(images);
			}
		}

		/// <summary>
		/// Moments at indexPath.
		/// </summary>
		/// <returns>The <see cref="Domain.Moment.Moment"/>.</returns>
		/// <param name="indexPath">Index path.</param>
		public IMoment MomentAt(NSIndexPath indexPath)
		{
			return m_moments[indexPath.Row];
		}

		/// <summary>
		/// Removes the row.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public void RemoveRow(UITableView tableView, NSIndexPath indexPath)
		{
			var moment = MomentAt(indexPath) as Moment;

			if (!moment.IsTemplate())
			{
				new MomentService().RemoveMoment(moment);
				var filePaths = new ImageProvider(moment.Id).DeleteFiles(false);
				new FileUploadService().InsertFilePaths(filePaths);
			}

			ClearMomentPhotos();

			m_moments.RemoveAt(indexPath.Row);
			tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
		}

		/// <Docs>Table view displaying the rows.</Docs>
		/// <summary>
		/// Rowses the in section.
		/// </summary>
		/// <returns>The in section.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="section">Section.</param>
		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return m_moments.Count;
		}

		/// <summary>
		/// Gets the height for row.
		/// </summary>
		/// <returns>The height for row.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 301f;
		}

		/// <summary>
		/// Estimateds the height.
		/// </summary>
		/// <returns>The height.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 301f;
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
			return tableView.DequeueReusableCell(s_cellIdentifier);
		}

		/// <Docs>Table view containing the row.</Docs>
		/// <param name="indexPath">Location of the row.</param>
		/// <paramref name="indexPath"></paramref>
		/// <remarks>Use this method to override properties of the cell before it is rendered (such as selection status or background
		/// color). After this method has been called, the table view will only modify the Alpha and Frame properties as it
		/// animates them (if required).</remarks>
		/// <summary>
		/// Wills the display.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="cell">Cell.</param>
		public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			var moment = MomentAt(indexPath);
			var momentCell = cell as TimelineMomentCell;

			momentCell.LabelAge = string.Concat("AtAge".Translate(), " ", Baby.FormatAge(m_baby.BirthDateTime, moment.MomentDate));
			momentCell.LabelDate = string.Concat("InDate".Translate(), " ", moment.MomentDate.ToString("LongDateMask".Translate(), System.Globalization.DateTimeFormatInfo.CurrentInfo));
			momentCell.LabelEventName = moment.EventDescription;
			momentCell.LabelWhere = string.Concat("At".Translate(), " ", Location.NameOrDefault(moment.LocationName));

			var scrollWidth = moment.MomentMediaCount * MediaBase.ImageThumbnailSize;
			momentCell.ViewPhotos.ContentSize = new CGSize(scrollWidth, MediaBase.ImageThumbnailSize);
			momentCell.ViewPhotos.AddSubviews(GetViewPhotos(moment));

			momentCell.HasDescription = !string.IsNullOrEmpty(moment.MomentDescription);
			momentCell.HasOptions = moment is Moment;
			momentCell.IncreaseOptionsTouchArea();

			UIImage image;
			if (m_tagIcons.TryGetValue(moment.EventTagName, out image))
			{
				momentCell.TagIcon = image;
			}
		}

		/// <summary>
		/// Gets the view photos.
		/// </summary>
		/// <returns>The view photos.</returns>
		/// <param name="moment">Moment.</param>
		MomentImageView[] GetViewPhotos(IMoment moment)
		{
			var photos = new List<MomentImageView>();

			for (var index = 0; index < moment.MomentMediaCount; index++)
			{
				var imageView = new MomentImageView(new CGRect(index * MediaBase.ImageThumbnailSize, 0f, MediaBase.ImageThumbnailSize, MediaBase.ImageThumbnailSize));

				imageView.Moment = moment;
				imageView.ItemIndex = index;
				imageView.BackgroundColor = UIColor.FromRGB(166, 166, 159);
				imageView.UserInteractionEnabled = true;
				imageView.MultipleTouchEnabled = true;
				imageView.ContentMode = UIViewContentMode.Center;
				imageView.Opaque = true;

				var activityIndicator = new UIActivityIndicatorView(imageView.Bounds);
				imageView.AddSubview(activityIndicator);
				activityIndicator.StartAnimating();

				var proxy = new EventProxy<TimelineViewSource, EventArgs>(this);
				proxy.Action = (target, sender, args) =>
				{
					var actionProgress = new ActionProgress(() =>
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

			InvokeInBackground(() => {
				LoadMomentPhotos(photos);
			});

			return photos.ToArray();
		}

		/// <summary>
		/// Reloads the data.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="moments">Moments.</param>
		/// <param name="baby">Baby.</param>
		public void ReloadData(UITableView tableView, IList<IMoment> moments, Baby baby)
		{
			m_moments = moments;
			m_baby = baby;

			ClearMomentPhotos();
			tableView.ReloadData();
		}
	}
}