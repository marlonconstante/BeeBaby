using System;

using Foundation;
using UIKit;
using BeeBaby.VisualElements;

namespace BeeBaby.Controllers
{
	public partial class EventViewCell : TableViewCell
	{
		public EventViewCell(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Gets or sets the event description.
		/// </summary>
		/// <value>The event description.</value>
		public string EventDescription {
			get {
				return lblEventDesc.Text;
			}
			set {
				lblEventDesc.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the tag icon.
		/// </summary>
		/// <value>The tag icon.</value>
		public UIImage TagIcon {
			get {
				return imgEventTagIcon.Image;
			}
			set {
				imgEventTagIcon.Image = value;
			}
		}
	}
}
