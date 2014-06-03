using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public partial class Button : UIButton
	{
		const int s_inset = -10;

		public Button(IntPtr handle) : base(handle)
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
			UIEdgeInsets insets = new UIEdgeInsets(s_inset, s_inset, s_inset, s_inset);
			return insets.InsetRect(Bounds).Contains(point);
		}
	}
}