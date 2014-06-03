using System;
using MonoTouch.UIKit;
using System.Drawing;
using PixateFreestyleLib;

namespace BeeBaby
{
	public class NavigationButtonItem : UIBarButtonItem
	{
		public NavigationButtonItem(RectangleF frame, float paddingLeft, EventHandler eventHandler, string styleClass) :
			base(BuildButtonItem(frame, paddingLeft, eventHandler, styleClass))
		{
		}

		/// <summary>
		/// Builds the button item.
		/// </summary>
		/// <returns>The button item.</returns>
		/// <param name="frame">Frame.</param>
		/// <param name="paddingLeft">Padding left.</param>
		/// <param name="eventHandler">Event handler.</param>
		/// <param name="styleClass">Style class.</param>
		static View BuildButtonItem(RectangleF frame, float paddingLeft, EventHandler eventHandler, string styleClass)
		{
			var buttonFrame = frame;
			buttonFrame.X += paddingLeft;

			Button button = new Button(buttonFrame);
			button.SetStyleClass(styleClass);
			button.TouchUpInside += eventHandler;

			View view = new View(frame);
			view.AddSubview(button);
			return view;
		}
	}
}