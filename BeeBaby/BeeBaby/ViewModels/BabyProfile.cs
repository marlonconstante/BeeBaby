using System;
using UIKit;
using Domain.Baby;
using BeeBaby.Util;
using BeeBaby.Media;

namespace BeeBaby.ViewModels
{
	public class BabyProfile : IDisposable
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

		/// <summary>
		/// Releases all resource used by the <see cref="BeeBaby.ViewModels.BabyProfile"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="BeeBaby.ViewModels.BabyProfile"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="BeeBaby.ViewModels.BabyProfile"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="BeeBaby.ViewModels.BabyProfile"/>
		/// so the garbage collector can reclaim the memory that the <see cref="BeeBaby.ViewModels.BabyProfile"/> was occupying.</remarks>
		public void Dispose()
		{
			Discard.ReleaseProperties(this);
		}
	}
}