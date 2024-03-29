using System;
using Foundation;
using UIKit;
using PixateFreestyleLib;
using BeeBaby.VisualElements;

namespace BeeBaby.Menu
{
	public partial class MenuCell : TableViewCell
	{
		public MenuCell(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Gets or sets the icon style class.
		/// </summary>
		/// <value>The icon style class.</value>
		public string IconStyleClass {
			get {
				return imgIcon.GetStyleClass();
			}
			set {
				imgIcon.SetStyleClass(value);
			}
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title {
			get {
				return lblTitle.Text;
			}
			set {
				lblTitle.Text = value;
			}
		}
	}
}