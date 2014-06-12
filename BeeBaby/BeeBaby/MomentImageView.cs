using System;
using System.Drawing;
using Domain.Moment;
using MonoTouch.UIKit;
using BeeBaby.ResourcesProviders;

namespace BeeBaby
{
	public class MomentImageView : UIImageViewClickable
	{
		public MomentImageView(RectangleF frame) : base(frame)
		{
		}

		/// <summary>
		/// Gets or sets the moment.
		/// </summary>
		/// <value>The moment.</value>
		public Moment Moment {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName {
			get;
			set;
		}

		/// <summary>
		/// Gets the photo.
		/// </summary>
		/// <value>The photo.</value>
		public UIImage Photo {
			get {
				return new ImageProvider(Moment.Id).GetImage(FileName);
			}
		}
	}
}