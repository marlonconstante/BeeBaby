using System;
using MonoTouch.UIKit;

namespace BeeBaby.ViewModels
{
	public class ImageViewModel
	{
		public ImageViewModel()
		{
		}

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The image.</value>
		public UIImage Image { get; set; }

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName { set; get; }
	}
}