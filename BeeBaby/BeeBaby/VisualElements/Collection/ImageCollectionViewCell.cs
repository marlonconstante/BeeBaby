using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using Domain.Media;
using PixateFreestyleLib;

namespace BeeBaby.VisualElements
{
	public partial class ImageCollectionViewCell : CollectionViewCell
	{
		public ImageCollectionViewCell(IntPtr handle) : base(handle)
		{
			IsSelected = false;
		}

		/// <summary>
		/// Gets or sets the photo.
		/// </summary>
		/// <value>The photo.</value>
		public UIImage Photo {
			get
			{
				return imgPhoto.Image;
			}
			set
			{
				imgPhoto.Image = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the media.
		/// </summary>
		/// <value>The name of the media.</value>
		public string MediaName { set; get; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is selected.
		/// </summary>
		/// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
		public bool IsSelected { get; set; }

		/// <summary>
		/// Updates the status.
		/// </summary>
		public void UpdateStatus()
		{
			vwOverlay.Hidden = !IsSelected;
			imgCheckmark.Hidden = !IsSelected;
		}
	}
}