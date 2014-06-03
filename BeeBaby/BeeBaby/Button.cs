using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public partial class Button : UIButton
	{
		public Button(IntPtr handle) : base(handle)
		{
		}

		public Button(RectangleF frame) : base(frame)
		{
		}

		/// <summary>
		/// Points the inside.
		/// </summary>
		/// <returns><c>true</c>, if inside was pointed, <c>false</c> otherwise.</returns>
		/// <param name="point">Point.</param>
		/// <param name="ev">Event.</param>
		public override bool PointInside(PointF point, UIEvent ev)
		{
			return TouchArea.IsPointInside(point, Bounds);
		}
	}
}