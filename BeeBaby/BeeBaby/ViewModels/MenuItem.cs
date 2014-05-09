using System;
using MonoTouch.Foundation;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace BeeBaby
{
	public class MenuItem
	{
		public MenuItem(string text, Action action)
		{
			Text = text;
			Action = action;
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the action.
		/// </summary>
		/// <value>The action.</value>
		public Action Action {
			get;
			set;
		}
	}
}