using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public partial class Label : UILabel
	{
		public Label(IntPtr handle) : base(handle)
		{
			InitDefaultValues();
		}

		public Label(RectangleF frame) : base(frame)
		{
			InitDefaultValues();
		}

		/// <summary>
		/// Inits the default values.
		/// </summary>
		void InitDefaultValues()
		{
			MaxHeight = 300f;
		}

		/// <summary>
		/// Updates the height of the line.
		/// </summary>
		/// <param name="lineHeight">Line height.</param>
		void UpdateLineHeight(float lineHeight)
		{
			NSMutableParagraphStyle style = new NSMutableParagraphStyle();
			style.MinimumLineHeight = lineHeight;
			style.MaximumLineHeight = lineHeight;
			style.Alignment = TextAlignment;

			NSMutableAttributedString attributedString = new NSMutableAttributedString(AttributedText);
			attributedString.AddAttribute(new NSString("NSParagraphStyle"), style, new NSRange(0, attributedString.Length));

			AttributedText = attributedString;
		}

		/// <summary>
		/// Sizes to fit.
		/// </summary>
		public override void SizeToFit()
		{
			if (TextSize.Height > Frame.Height)
			{
				base.SizeToFit();
			}
		}

		/// <summary>
		/// Gets the size of the text.
		/// </summary>
		/// <value>The size of the text.</value>
		public SizeF TextSize
		{
			get {
				return StringSize(Text, Font, new SizeF(Frame.Width, MaxHeight));
			}
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public override string Text {
			get {
				return base.Text;
			}
			set {
				base.Text = value;
				UpdateLineHeight(Font.PointSize);
				SizeToFit();
			}
		}

		/// <summary>
		/// Gets or sets the height of the max.
		/// </summary>
		/// <value>The height of the max.</value>
		public float MaxHeight {
			get;
			set;
		}
	}
}