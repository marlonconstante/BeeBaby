using System;
using MonoTouch.UIKit;
using Domain.Moment;
using System.Collections.Generic;
using Application;
using MonoTouch.Foundation;
using Skahal.Infrastructure.Framework.Domain;
using BeeBaby.ResourcesProviders;
using Domain.Media;
using System.Drawing;

namespace BeeBaby
{
	public class TimelineViewSource : UITableViewSource
	{
		private UIViewController m_viewController;
		private IList<IAggregateRoot> m_tableItems;

		public TimelineViewSource(UIViewController viewController, IList<IAggregateRoot> items)
		{
			m_viewController = viewController;
			m_tableItems = items;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section
		/// </summary>
		public override int RowsInSection(UITableView tableview, int section)
		{
			return m_tableItems.Count;
		}

		/// <summary>
		/// Called by the TableView to determine the estimated height of the row
		/// </summary>
		public override float EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return GetHeight(indexPath);
		}

		/// <summary>
		/// Called by the TableView to determine the row height
		/// </summary>
		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return GetHeight(indexPath);
		}

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row
		/// </summary>
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			string cellIdentifier = GetCellIdentifier(indexPath);

			// Request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);

			switch (cellIdentifier)
			{
				case "MomentCell":
					return PopulateMomentCell(cell, indexPath);
					;

				case "EventCell":
					TimelineEventCell ce = cell as TimelineEventCell;
					return ce;
			}

			return cell;
		}

		UITableViewCell PopulateMomentCell(UITableViewCell cell, NSIndexPath indexPath)
		{
			Moment moment = m_tableItems[indexPath.Row] as Moment;
			TimelineMomentCell cm = cell as TimelineMomentCell;
			cm.LabelAge = "30 Dias";
			cm.LabelDate = moment.Date.ToString();
			//cm.LabelEventName = moment.Event.Description;
			cm.LabelWhere = string.Format("{0} - {1}", moment.Position.Longitude, moment.Position.Latitude);
			cm.LabelWho = "Com a Vovó.";

			var provider = new ImageProvider(moment);
			var images = provider.GetImagesForCurrentMoment(true);
			cm.ViewPhotos.Frame = new System.Drawing.RectangleF(0, 0, MediaBase.ImageThumbnailWidth * images.Count, MediaBase.ImageThumbnailHeight);

			var i = 0;

			foreach (var image in images)
			{
				var x = i * MediaBase.ImageThumbnailWidth;
				var uiImageView = new UIImageView(new Rectangle(x, 0, MediaBase.ImageThumbnailWidth, MediaBase.ImageThumbnailHeight));
				uiImageView.Image = image.Image;
				cm.ViewPhotos.AddSubview(uiImageView);

				i++;
			}

			return cm;
		}

		/// <summary>
		/// Get the cell identifier
		/// </summary>
		private string GetCellIdentifier(NSIndexPath indexPath)
		{
			switch (m_tableItems[indexPath.Row].GetType().Name)
			{
				case "Moment":
					return "MomentCell";
				case "Event":
					return "EventCell";
				default:
					return "";
			}
		}

		/// <summary>
		/// Get the row height
		/// </summary>
		private float GetHeight(NSIndexPath indexPath)
		{
			switch (m_tableItems[indexPath.Row].GetType().Name)
			{
				case "Moment":
					return 255f;
				case "Event":
					return 56f;
				default:
					return 0;
			}
		}
	}
}