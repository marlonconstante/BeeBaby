using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using BeeBaby.ResourcesProviders;

namespace BeeBaby
{
	public partial class TimelineMomentCell : TableViewCell
	{
		public TimelineMomentCell(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Gets or sets the label age.
		/// </summary>
		/// <value>The label age.</value>
		public string LabelAge
		{
			get
			{
				return lblAge.Text;
			}
			set
			{
				lblAge.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the label date.
		/// </summary>
		/// <value>The label date.</value>
		public string LabelDate
		{
			get
			{
				return lblDate.Text;
			}
			set
			{
				lblDate.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the label event.
		/// </summary>
		/// <value>The name of the label event.</value>
		public string LabelEventName
		{
			get
			{
				return lblEventName.Text;
			}
			set
			{
				lblEventName.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the label where.
		/// </summary>
		/// <value>The label where.</value>
		public string LabelWhere
		{
			get
			{
				return lblWhere.Text;
			}
			set
			{
				lblWhere.Text = value;
			}
		}

		public UIImage EventBadge
		{
			get
			{
				return imgEventBadge.Image;
			}
			set
			{
				var badge = ImageProvider.GenerateEventBadge(value, imgEventBadge);
				imgEventBadge.AddSubview(badge);
			}
		}


		/// <summary>
		/// Gets or sets the view photos.
		/// </summary>
		/// <value>The view photos.</value>
		public UIScrollView ViewPhotos
		{
			get
			{
				return vwPhotos;
			}
			set
			{
				vwPhotos = value;
			}
		}

		/// <summary>
		/// Opens the options.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void OpenOptions(UIButton sender)
		{
			var viewController = (TimelineViewController)Windows.GetTopViewController(Window);
			viewController.OpenOptions(this);
		}
	}
}