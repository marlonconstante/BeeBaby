using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public abstract class TouchArea
	{
		private TouchArea()
		{
		}
	
		/// <summary>
		/// Determines if is point inside the specified point bounds inset.
		/// </summary>
		/// <returns><c>true</c> if is point inside the specified point bounds inset; otherwise, <c>false</c>.</returns>
		/// <param name="point">Point.</param>
		/// <param name="bounds">Bounds.</param>
		/// <param name="inset">Inset.</param>
		public static bool IsPointInside(PointF point, RectangleF bounds, int inset)
		{
			UIEdgeInsets insets = new UIEdgeInsets(-inset, -inset, -inset, -inset);
			return insets.InsetRect(bounds).Contains(point);
		}
	}
}