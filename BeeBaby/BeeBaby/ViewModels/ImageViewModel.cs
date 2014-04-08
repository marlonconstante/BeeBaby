using System;
using MonoTouch.UIKit;

namespace BeeBaby.ViewModels
{
	public class ImageViewModel
	{
		public ImageViewModel ()
		{
		}

		public UIImage Image {
			get;
			set;
		}

		public string FileName {
			get;
			set;
		}
	}
}

