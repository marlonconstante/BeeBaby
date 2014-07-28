using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BeeBaby.ResourcesProviders;

namespace BeeBaby
{
	public partial class MediaNavigationController : MomentNavigationController
	{
		public MediaNavigationController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Close navigation.
		/// </summary>
		public override void Close()
		{
			base.Close();

			new ImageProvider().DeleteFiles(true);
		}
	}
}