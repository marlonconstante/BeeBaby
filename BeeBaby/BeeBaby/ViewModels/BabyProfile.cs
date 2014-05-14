using System;
using MonoTouch.UIKit;
using Domain.Baby;

namespace BeeBaby.ViewModels
{
	public class BabyProfile
	{
		public BabyProfile()
		{
		}

		/// <summary>
		/// Gets or sets the baby.
		/// </summary>
		/// <value>The baby.</value>
		public Baby Baby { get; set; }

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The image.</value>
		public UIImage Image { get; set; }

		/// <summary>
		/// Gets or sets the delegate.
		/// </summary>
		/// <value>The delegate.</value>
		public BabyImagePickerDelegate Delegate { set; get; }
	}
}