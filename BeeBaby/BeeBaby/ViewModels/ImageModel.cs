using System;
using UIKit;
using BeeBaby.Util;

namespace BeeBaby.ViewModels
{
	public class ImageModel : IDisposable
	{
		UIImage m_image;

		public ImageModel()
		{
		}

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The image.</value>
		public UIImage Image {
			get {
				return m_image;
			}
			set {
				Changed = m_image != null;
				if (Changed)
				{
					m_image.Dispose();
				}
				m_image = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BeeBaby.ViewModels.ImageModel"/> is changed.
		/// </summary>
		/// <value><c>true</c> if changed; otherwise, <c>false</c>.</value>
		public bool Changed { get; protected set; }

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