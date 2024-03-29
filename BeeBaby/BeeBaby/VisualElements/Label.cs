using System;
using Foundation;
using UIKit;
using CoreGraphics;
using System.Drawing;

namespace BeeBaby.VisualElements
{
	public partial class Label : UILabel
	{
		public Label(IntPtr handle) : base(handle)
		{
			InitDefaultValues();
		}

		public Label(CGRect frame) : base(frame)
		{
			InitDefaultValues();
		}

		/// <summary>
		/// Inits the default values.
		/// </summary>
		void InitDefaultValues()
		{
			IsAutoAdjustSize = false;
			Lines = 0;
			LineHeight = Font.PointSize;
			MaxHeight = 300f;
		}

		/// <summary>
		/// Updates the height of the line.
		/// </summary>
		void UpdateLineHeight()
		{
			NSMutableParagraphStyle style = new NSMutableParagraphStyle();
			style.MinimumLineHeight = LineHeight;
			style.MaximumLineHeight = LineHeight;
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
			var frame = Frame;
			var height = TextSize.Height;
			if (IsAutoAdjustSize || height > frame.Height)
			{
				frame.Height = height;
			}
			Frame = frame;
		}

		/// <summary>
		/// Gets the size of the text.
		/// </summary>
		/// <value>The size of the text.</value>
		public CGSize TextSize
		{
			get {
				return StringSize(Text, Font, new CGSize(Frame.Width, MaxHeight));
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
				UpdateLineHeight();
				SizeToFit();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is auto adjust size.
		/// </summary>
		/// <value><c>true</c> if this instance is auto adjust size; otherwise, <c>false</c>.</value>
		public bool IsAutoAdjustSize {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the height of the line.
		/// </summary>
		/// <value>The height of the line.</value>
		public nfloat LineHeight {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the height of the max.
		/// </summary>
		/// <value>The height of the max.</value>
		public nfloat MaxHeight {
			get;
			set;
		}
	}
}