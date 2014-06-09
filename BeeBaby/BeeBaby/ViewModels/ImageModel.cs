using System;
using MonoTouch.UIKit;

namespace BeeBaby.ViewModels
{
	public class ImageModel : IDisposable
	{
		public ImageModel()
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

		/// <summary>
		/// Releases all resource used by the <see cref="BeeBaby.ViewModels.ImageModel"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="BeeBaby.ViewModels.ImageModel"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="BeeBaby.ViewModels.ImageModel"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="BeeBaby.ViewModels.ImageModel"/>
		/// so the garbage collector can reclaim the memory that the <see cref="BeeBaby.ViewModels.ImageModel"/> was occupying.</remarks>
		public void Dispose()
		{
			Discard.ReleaseProperties(this);
		}
	}
}