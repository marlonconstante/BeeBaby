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
using MonoTouch.ObjCRuntime;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public class TimelineViewSource : UITableViewSource
	{
		const string s_cellIdentifier = "MomentCell";
		UIViewController m_viewController;
		IList<Moment> m_tableItems;
		Baby m_baby;
		UIViewFullscreen m_vwFullscreen;

		public TimelineViewSource(UIViewController viewController, IList<Moment> moments, Baby baby)
		{
			m_viewController = viewController;
			m_tableItems = moments;
			m_baby = baby;
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

		/// <Docs>Table view requesting the cell.</Docs>
		/// <summary>
		/// Gets the cell.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// Request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell(s_cellIdentifier);

			return PopulateMomentCell(cell, indexPath);
		}

		/// <summary>
		/// Populates the moment cell.
		/// </summary>
		/// <returns>The moment cell.</returns>
		/// <param name="cell">Cell.</param>
		/// <param name="indexPath">Index path.</param>
		UITableViewCell PopulateMomentCell(UITableViewCell cell, NSIndexPath indexPath)
		{
			Moment moment = m_tableItems[indexPath.Row] as Moment;
			Console.WriteLine(string.Format("{0} -> {1}", indexPath.Row, moment.Id));
			TimelineMomentCell momentCell = cell as TimelineMomentCell;

			momentCell.LabelAge = Baby.FormatAge(m_baby.BirthDateTime, moment.Date);
			momentCell.LabelDate = moment.Date.ToString("LongDateMask".Translate(), System.Globalization.DateTimeFormatInfo.CurrentInfo);
			momentCell.LabelEventName = moment.Event.Description;
			momentCell.LabelWhere = string.Format("{0} - {1}", moment.Position.Longitude, moment.Position.Latitude);

			var imageProvider = new ImageProvider(moment.Id);
			IList<ImageModel> images = imageProvider.GetImages(false, true);

			var scrollWidth = images.Count * MediaBase.ImageThumbnailSize;
			momentCell.ViewPhotos.ContentSize = new SizeF(scrollWidth, MediaBase.ImageThumbnailSize);

			var index = 0;
			foreach (var image in images)
			{
				using (var imageView = new UIImageViewClickable(new RectangleF(index * MediaBase.ImageThumbnailSize, 0f, MediaBase.ImageThumbnailSize, MediaBase.ImageThumbnailSize)))
				{
					imageView.Image = image.Image;
					imageView.UserInteractionEnabled = true;
					imageView.MultipleTouchEnabled = true;
					imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
					imageView.OnClick += () => {
						if (m_vwFullscreen == null)
						{
							m_vwFullscreen = new UIViewFullscreen();
						}
						m_vwFullscreen.SetImage(imageProvider.GetImage(image.FileName), moment);
						m_vwFullscreen.Show();
					};

					momentCell.ViewPhotos.AddSubview(imageView);
				}
				index++;
			}

			return momentCell;
		}
	}
}