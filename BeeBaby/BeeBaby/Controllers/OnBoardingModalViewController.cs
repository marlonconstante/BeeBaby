using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BeeBaby.Controllers;
using PixateFreestyleLib;

namespace BeeBaby.Controllers
{
	public partial class OnBoardingModalViewController : ModalViewController
	{
		public OnBoardingModalViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Close the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Close(UIButton sender)
		{
			Hide();
		}
	}
}
