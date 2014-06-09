using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public abstract class TouchArea
	{
		const int s_inset = -10;

		private TouchArea()
		{
		}
	
		/// <summary>
		/// Determines if is point inside the specified point bounds.
		/// </summary>
		/// <returns><c>true</c> if is point inside the specified point bounds; otherwise, <c>false</c>.</returns>
		/// <param name="point">Point.</param>
		/// <param name="bounds">Bounds.</param>
		public static bool IsPointInside(PointF point, RectangleF bounds)
		{
			UIEdgeInsets insets = new UIEdgeInsets(s_inset, s_inset, s_inset, s_inset);
			return insets.InsetRect(bounds).Contains(point);
		}
	}
}