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
			//cm.LabelEventName = moment.Event.Description;
			momentCell.LabelWhere = string.Format("{0} - {1}", moment.Position.Longitude, moment.Position.Latitude);
			momentCell.LabelWho = indexPath.Row.ToString();

			var provider = new ImageProvider(moment);
			m_images = provider.GetImagesForCurrentMoment(false, true);
			provider = null;
//			moment = null;

			momentCell.ViewPhotos.Frame = new RectangleF(0, 0, (MediaBase.ImageThumbnailWidth) * m_images.Count, MediaBase.ImageThumbnailHeight);

			var i = 0;

			foreach (var image in m_images)
			{
				var xCoord = i * MediaBase.ImageThumbnailWidth;

				using (var uiImageView = new UIThumbnailImageView(new Rectangle(xCoord, 0, MediaBase.ImageThumbnailWidth, MediaBase.ImageThumbnailHeight)))
				{
					uiImageView.Image = image.Image;
					uiImageView.UserInteractionEnabled = true;
					uiImageView.Moment = moment;
					uiImageView.ImageName = image.FileName;

					var gestureRecognizer = new UITapGestureRecognizer();
					gestureRecognizer.NumberOfTapsRequired = 2; // double tap
					gestureRecognizer.AddTarget(this, new MonoTouch.ObjCRuntime.Selector("DoubleTapSelector"));
					uiImageView.AddGestureRecognizer(gestureRecognizer);

					//momentCell.ViewPhotos.AddGestureRecognizer(gestureRecognizer); // detect when the scrollView is double-tapped

					momentCell.ViewPhotos.AddSubview(uiImageView);
				}
				i++;
			}

			m_images = null;

			return momentCell;
		}

		[MonoTouch.Foundation.Export("DoubleTapSelector")]
		public void OnDoubleTap(UIGestureRecognizer sender)
		{
			var view = sender.View as UIThumbnailImageView;
			var provider = new ImageProvider(view.Moment);
			UIImage image = provider.GetImage(view.ImageName);
			Console.WriteLine(string.Format("Tapped: {0} of {1}", view.Moment.Id, view.ImageName)); 


			_imageView = new UIImageView(new RectangleF(0, 0, 50, 50));
			_imageView.Image = image;
		
			m_viewController.View.AddSubview(_imageView);
			m_viewController.View.AccessibilityViewIsModal = true;


			UIView.BeginAnimations("slideAnimation");

			UIView.SetAnimationDuration(1);
			UIView.SetAnimationCurve(UIViewAnimationCurve.Linear);
			UIView.SetAnimationRepeatCount(0);
			UIView.SetAnimationRepeatAutoreverses(false);
			UIView.SetAnimationDelegate(this);
			UIView.SetAnimationDidStopSelector(new Selector("slideAnimationFinished:"));


			_imageView.Frame = new RectangleF(UIScreen.MainScreen.Bounds.Left, UIScreen.MainScreen.Bounds.Top, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
			_imageView.Image = image;
			_imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
			_imageView.Center = new PointF(UIScreen.MainScreen.Bounds.Width / 2, UIScreen.MainScreen.Bounds.Height / 2);

			UIView.CommitAnimations();

		}
	}
}