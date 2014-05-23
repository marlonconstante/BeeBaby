using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;

namespace BeeBaby
{
	public partial class FullscreenViewController : BaseViewController
	{
		public FullscreenViewController (IntPtr handle) : base (handle)
		{
		}

		/// <summary>
		/// Gets the supported interface orientations.
		/// </summary>
		/// <returns>The supported interface orientations.</returns>
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.All;
		}

		/// <summary>
		/// Determines whether this instance is show status bar.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		public override bool IsShowStatusBar()
		{
			return false;
		}

		/// <summary>
		/// Gets or sets the moment.
		/// </summary>
		/// <value>The moment.</value>
		public Moment Moment {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the photo.
		/// </summary>
		/// <value>The photo.</value>
		public UIImage Photo {
			get {
				return imgPhoto.Image;
			}
			set {
				imgPhoto.Image = value;
			}
		}
	}
}