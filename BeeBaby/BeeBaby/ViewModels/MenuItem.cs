using System;
using MonoTouch.Foundation;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace BeeBaby
{
	public class MenuItem
	{
		public MenuItem(string title, string iconStyleClass, Action action)
		{
			Title = title;
			IconStyleClass = iconStyleClass;
			Action = action;
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the icon style class.
		/// </summary>
		/// <value>The icon style class.</value>
		public string IconStyleClass {
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