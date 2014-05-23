using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public partial class Label : UILabel
	{
		public Label(IntPtr handle) : base(handle)
		{
			UpdateLineHeight(Font.PointSize);
		}

		/// <summary>
		/// Updates the height of the line.
		/// </summary>
		/// <param name="lineHeight">Line height.</param>
		public void UpdateLineHeight(float lineHeight)
		{
			NSMutableParagraphStyle style = new NSMutableParagraphStyle();
			style.MinimumLineHeight = lineHeight;
			style.MaximumLineHeight = lineHeight;

			NSMutableAttributedString attributedString = new NSMutableAttributedString(AttributedText);
			attributedString.AddAttribute(new NSString("NSParagraphStyle"), style, new NSRange(0, attributedString.Length));

			AttributedText = attributedString;
		}
	}
}