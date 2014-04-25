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
		UIViewController m_viewController;
		IList<Moment> m_tableItems;
		Baby m_baby;
		IList<ImageViewModel> m_images;
		const string s_cellIdentifier = "MomentCell";
		UIImageView _imageView;
		UIViewFullscreen vMain;

		public TimelineViewSource(UIViewController viewController, IList<Moment> items, Baby baby)
		{
			m_viewController = viewController;
			m_tableItems = items;
			m_baby = baby;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section
		/// </summary>
		public override int RowsInSection(UITableView tableview, int section)
		{
			return m_tableItems.Count;
		}

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row
		/// </summary>
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// Request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell(s_cellIdentifier);

			return PopulateMomentCell(cell, indexPath);
		}

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
			var viewWidth = (MediaBase.ImageThumbnailWidth) * m_images.Count;
			momentCell.ViewPhotos.ContentSize = new SizeF(viewWidth, MediaBase.ImageThumbnailHeight);

			var i = 0;

			foreach (var image in m_images)
			{
				var xCoord = i * MediaBase.ImageThumbnailWidth;

				using (var uiImageView = new UIImageViewClickable(new Rectangle(xCoord, 0, MediaBase.ImageThumbnailWidth, MediaBase.ImageThumbnailHeight)))
				{
					uiImageView.Image = image.Image;
					uiImageView.UserInteractionEnabled = true;
					uiImageView.MultipleTouchEnabled = true;
					uiImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
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