using System;
using MonoTouch.UIKit;
using Domain.Moment;
using System.Collections.Generic;
using MonoTouch.Foundation;
using BeeBaby.ResourcesProviders;
using Domain.Media;
using System.Drawing;
using Domain.Baby;
using BigTed;

namespace BeeBaby
{
	public class TimelineViewSource : UITableViewSource
	{
		UIViewController m_viewController;
		IList<Moment> m_tableItems;
		Baby m_baby;

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
			cm.LabelAge = Baby.FormatAge(m_baby.BirthDateTime, moment.Date);
			cm.LabelDate = moment.Date.ToString("M", System.Globalization.DateTimeFormatInfo.CurrentInfo);
			//cm.LabelEventName = moment.Event.Description;
			cm.LabelWhere = string.Format("{0} - {1}", moment.Position.Longitude, moment.Position.Latitude);
			cm.LabelWho = "Com a Vovó.";

			var provider = new ImageProvider(moment);
			var images = provider.GetImagesForCurrentMoment(false, true);
			cm.ViewPhotos.Frame = new RectangleF(0, 0, (MediaBase.ImageThumbnailWidth) * images.Count, MediaBase.ImageThumbnailHeight);

			var i = 0;

			foreach (var image in images)
			{
				var xCoord = i * MediaBase.ImageThumbnailWidth;
				
				var uiImageView = new UIImageView(new Rectangle(xCoord, 0, MediaBase.ImageThumbnailWidth, MediaBase.ImageThumbnailHeight));
				uiImageView.Image = image.Image;

				UITapGestureRecognizer doubletap = new UITapGestureRecognizer();
				doubletap.NumberOfTapsRequired = 2; // double tap
				doubletap.AddTarget(this, new MonoTouch.ObjCRuntime.Selector("DoubleTapSelector"));
				uiImageView.AddGestureRecognizer(doubletap); // detect when the scrollView is double-tapped

				cm.ViewPhotos.AddSubview(uiImageView);

				i++;
			}

			return cm;
		}

		[MonoTouch.Foundation.Export("DoubleTapSelector")]
		public void OnDoubleTap(UIGestureRecognizer sender)
		{
			BTProgressHUD.Show();
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