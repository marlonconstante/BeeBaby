using System;
using CoreGraphics;
using UIKit;
using System.Drawing;

namespace BeeBaby.Util
{
	public abstract class TouchArea
	{
		private TouchArea()
		{
		}

		/// <summary>
		/// Determines if is point inside the specified point bounds extraTouchArea.
		/// </summary>
		/// <returns><c>true</c> if is point inside the specified point bounds extraTouchArea; otherwise, <c>false</c>.</returns>
		/// <param name="point">Point.</param>
		/// <param name="bounds">Bounds.</param>
		/// <param name="extraTouchArea">Extra touch area.</param>
		public static bool IsPointInside(PointF point, RectangleF bounds, int extraTouchArea)
		{
			UIEdgeInsets insets = new UIEdgeInsets(-extraTouchArea, -extraTouchArea, -extraTouchArea, -extraTouchArea);
			return insets.InsetRect(bounds).Contains(point);
		}
	}
}