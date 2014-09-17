using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using BeeBaby.Util;

namespace BeeBaby.ViewModels
{
	public class MenuItem : IDisposable
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

		/// <summary>
		/// Releases all resource used by the <see cref="BeeBaby.MenuItem"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="BeeBaby.MenuItem"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="BeeBaby.MenuItem"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="BeeBaby.MenuItem"/> so the garbage
		/// collector can reclaim the memory that the <see cref="BeeBaby.MenuItem"/> was occupying.</remarks>
		public void Dispose()
		{
			Discard.ReleaseProperties(this);
		}
	}
}