// This file has been autogenerated from a class added in the UI designer.

using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using Domain.Media;
using PixateFreestyleLib;

namespace BeeBaby
{
	public partial class CollectionViewCell : UICollectionViewCell
	{
		public CollectionViewCell(IntPtr handle) : base(handle)
		{
			IsSelected = false;
		}

		/// <summary>
		/// Gets the image photo.
		/// </summary>
		/// <returns>The image photo.</returns>
		public UIImageView GetImagePhoto()
		{
			return (UIImageView) this.ViewWithTag(1);
		}

		/// <summary>
		/// Gets the view overlay.
		/// </summary>
		/// <returns>The view overlay.</returns>
		private UIView GetViewOverlay()
		{
			return this.ViewWithTag(2);
		}

		/// <summary>
		/// Gets the image checkmark.
		/// </summary>
		/// <returns>The image checkmark.</returns>
		private UIImageView GetImageCheckmark()
		{
			return (UIImageView) this.ViewWithTag(3);
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
			GetViewOverlay().Hidden = !IsSelected;
			GetImageCheckmark().Hidden = !IsSelected;
		}
	}
}