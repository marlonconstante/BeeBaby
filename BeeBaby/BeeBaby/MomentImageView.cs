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
		/// Gets or sets the index of the item.
		/// </summary>
		/// <value>The index of the item.</value>
		public int ItemIndex {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the moment.
		/// </summary>
		/// <value>The moment.</value>
		public Moment Moment {
			get;
			set;
		}
	}
}