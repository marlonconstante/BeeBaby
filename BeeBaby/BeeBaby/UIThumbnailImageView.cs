using System;
using MonoTouch.UIKit;
using Domain.Moment;
using System.Drawing;

namespace BeeBaby
{
	public class UIThumbnailImageView : UIImageView
	{
		public UIThumbnailImageView(RectangleF frame) : base(frame)
		{
		}
		public Moment Moment
		{
			get;
			set;
		}

		public string ImageName
		{
			get;
			set;
		}

		public UIView TargetView
		{
			get;
			set;
		}
	}
}

