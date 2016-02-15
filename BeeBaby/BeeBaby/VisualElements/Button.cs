using System;

using Foundation;
using UIKit;
using CoreGraphics;
using BeeBaby.Util;
using System.Drawing;

namespace BeeBaby.VisualElements
{
	public partial class Button : UIButton
	{
		public Button(IntPtr handle) : base(handle)
		{
			InitDefaultValues();
		}

		public Button(RectangleF frame) : base(frame)
		{
			InitDefaultValues();
		}

		/// <summary>
		/// Inits the default values.
		/// </summary>
		void InitDefaultValues()
		{
			ExtraTouchArea = 10;
		}

		/// <summary>
		/// Points the inside.
		/// </summary>
		/// <returns><c>true</c>, if inside was pointed, <c>false</c> otherwise.</returns>
		/// <param name="point">Point.</param>
		/// <param name="ev">Event.</param>
		public override bool PointInside(CGPoint point, UIEvent ev)
		{
			if (IsIncreaseTouchArea())
			{
				return TouchArea.IsPointInside(point, Bounds, ExtraTouchArea);
			}
			else
			{
				return base.PointInside(point, ev);
			}
		}

		/// <summary>
		/// Determines whether this instance is increase touch area.
		/// </summary>
		/// <returns><c>true</c> if this instance is increase touch area; otherwise, <c>false</c>.</returns>
		public virtual bool IsIncreaseTouchArea()
		{
			return true;
		}

		/// <summary>
		/// Gets or sets the extra touch area.
		/// </summary>
		/// <value>The extra touch area.</value>
		public int ExtraTouchArea {
			get;
			set;
		}
	}
}