using System;
using MonoTouch.UIKit;
using System.Drawing;
using PixateFreestyleLib;

namespace BeeBaby
{
	public class NavigationButtonItem : UIBarButtonItem
	{
		public NavigationButtonItem(RectangleF frame, float paddingLeft, string styleClass)
		{
			AddCustomView(frame, paddingLeft, styleClass);
		}

		/// <summary>
		/// Adds the custom view.
		/// </summary>
		/// <param name="frame">Frame.</param>
		/// <param name="paddingLeft">Padding left.</param>
		/// <param name="styleClass">Style class.</param>
		void AddCustomView(RectangleF frame, float paddingLeft, string styleClass)
		{
			var buttonFrame = frame;
			buttonFrame.X += paddingLeft;

			Button = new Button(buttonFrame);
			Button.SetStyleClass(styleClass);

			CustomView = new View(frame);
			CustomView.AddSubview(Button);
		}

		/// <summary>
		/// Gets or sets the button.
		/// </summary>
		/// <value>The button.</value>
		public Button Button {
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
				Discard.ReleaseSubviews(CustomView);
				Discard.ReleaseProperties(this);
			}

			base.Dispose(disposing);
		}
	}
}