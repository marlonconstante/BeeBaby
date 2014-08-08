using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public partial class View : UIView
	{
		public View(IntPtr handle) : base(handle)
		{
			InitDefaultValues();
		}

		public View(RectangleF frame) : base(frame)
		{
			InitDefaultValues();
		}

		/// <summary>
		/// Inits the default values.
		/// </summary>
		void InitDefaultValues()
		{
			TouchInset = 10;
		}

		/// <summary>
		/// Points the inside.
		/// </summary>
		/// <returns><c>true</c>, if inside was pointed, <c>false</c> otherwise.</returns>
		/// <param name="point">Point.</param>
		/// <param name="ev">Event.</param>
		public override bool PointInside(PointF point, UIEvent ev)
		{
			if (IsIncreaseTouchArea())
			{
				return TouchArea.IsPointInside(point, Bounds, TouchInset);
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
		/// Gets or sets the touch inset.
		/// </summary>
		/// <value>The touch inset.</value>
		public int TouchInset {
			get;
			set;
		}

		/// <summary>
		/// Dispose the specified disposing.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Discard.ReleaseSubviews(this);
				Discard.ReleaseOutlets(this);
				Discard.ReleaseProperties(this);
				Discard.ReleaseFields(this);
			}

			base.Dispose(disposing);
		}
	}
}