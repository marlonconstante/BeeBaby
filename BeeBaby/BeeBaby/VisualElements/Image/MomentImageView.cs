using System;
using CoreGraphics;
using Domain.Moment;
using UIKit;
using BeeBaby.ResourcesProviders;
using System.Drawing;

namespace BeeBaby.VisualElements
{
	public class MomentImageView : UIImageViewClickable
	{
		public MomentImageView(CGRect frame) : base(frame)
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
		public IMoment Moment {
			get;
			set;
		}
	}
}