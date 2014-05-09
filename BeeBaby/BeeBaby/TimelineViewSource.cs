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

namespace BeeBaby
{
	public class TimelineViewSource : UITableViewSource
	{
		const string s_cellIdentifier = "MomentCell";
		UIViewController m_viewController;
		IList<Moment> m_tableItems;
		Baby m_baby;
		IList<ImageViewModel> m_images;
		UIImageView _imageView;
		UIViewFullscreen vMain;

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
			momentCell.LabelDate = moment.Date.ToString("M", System.Globalization.DateTimeFormatInfo.CurrentInfo);
			momentCell.LabelEventName = moment.Event.Description;
			momentCell.LabelWhere = string.Format("{0} - {1}", moment.Position.Longitude, moment.Position.Latitude);
			momentCell.LabelWho = indexPath.Row.ToString();

			var provider = new ImageProvider(moment);
			m_images = provider.GetImagesForCurrentMoment(false, true);
			var viewWidth = (MediaBase.ImageThumbnailSize) * m_images.Count;
			momentCell.ViewPhotos.ContentSize = new SizeF(viewWidth, MediaBase.ImageThumbnailSize);

			var i = 0;

			foreach (var image in m_images)
			{
				var xCoord = i * MediaBase.ImageThumbnailSize;

				using (var uiImageView = new UIImageViewClickable(new Rectangle(xCoord, 0, MediaBase.ImageThumbnailSize, MediaBase.ImageThumbnailSize)))
				{
					uiImageView.Image = image.Image;
					uiImageView.UserInteractionEnabled = true;
					uiImageView.MultipleTouchEnabled = true;
					uiImageView.ContentMode = UIViewContentMode.ScaleAspectFill;
					uiImageView.OnClick += () => {
						if (vMain == null)
						{
							vMain = new UIViewFullscreen();
						}
						vMain.SetImage(provider.GetImage(image.FileName), moment);
						vMain.Show();
					};

					momentCell.ViewPhotos.AddSubview(uiImageView);
				}
				i++;
			}

			m_images = null;

			return momentCell;
		}
	}
}