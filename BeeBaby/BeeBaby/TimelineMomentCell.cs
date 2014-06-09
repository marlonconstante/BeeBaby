using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

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
		public string LabelAge {
			get {
				return lblAge.Text;
			}
			set {
				lblAge.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the label date.
		/// </summary>
		/// <value>The label date.</value>
		public string LabelDate {
			get {
				return lblDate.Text;
			}
			set {
				lblDate.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the label event.
		/// </summary>
		/// <value>The name of the label event.</value>
		public string LabelEventName {
			get {
				return lblEventName.Text;
			}
			set {
				lblEventName.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the label where.
		/// </summary>
		/// <value>The label where.</value>
		public string LabelWhere {
			get {
				return lblWhere.Text;
			}
			set {
				lblWhere.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the view photos.
		/// </summary>
		/// <value>The view photos.</value>
		public UIScrollView ViewPhotos {
			get {
				return vwPhotos;
			}
			set {
				vwPhotos = value;
			}
		}
	}
}