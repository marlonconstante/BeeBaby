﻿using System;
using MonoTouch.UIKit;
using System.Drawing;
using PixateFreestyleLib;
using BeeBaby.Util;

namespace BeeBaby.VisualElements
{
	public class NavigationButtonItem : UIBarButtonItem
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.VisualElements.NavigationButtonItem"/> class.
		/// </summary>
		/// <param name="frame">Frame.</param>
		/// <param name="paddingLeft">Padding left.</param>
		/// <param name="styleClass">Style class.</param>
		public NavigationButtonItem(RectangleF frame, float paddingLeft, string styleClass) : this(frame, CreateButton(frame, paddingLeft, styleClass))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.VisualElements.NavigationButtonItem"/> class.
		/// </summary>
		/// <param name="frame">Frame.</param>
		/// <param name="button">Button.</param>
		public NavigationButtonItem(RectangleF frame, Button button)
		{
			Button = button;
			AddCustomView(frame);
		}

		/// <summary>
		/// Creates the button.
		/// </summary>
		/// <returns>The button.</returns>
		/// <param name="frame">Frame.</param>
		/// <param name="paddingLeft">Padding left.</param>
		/// <param name="styleClass">Style class.</param>
		static Button CreateButton(RectangleF frame, float paddingLeft, string styleClass)
		{
			frame.X += paddingLeft;

			var button = new Button(frame);
			button.SetStyleClass(styleClass);

			return button;
		}

		/// <summary>
		/// Adds the custom view.
		/// </summary>
		/// <param name="frame">Frame.</param>
		void AddCustomView(RectangleF frame)
		{
			CustomView = new View(frame);
			CustomView.AddSubview(Button);
		}

		/// <summary>
		/// Gets the button.
		/// </summary>
		/// <value>The button.</value>
		public Button Button {
			get;
			private set;
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