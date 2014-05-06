using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public partial class DealsViewController : NavigationViewController
	{
		public DealsViewController (IntPtr handle) : base (handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			vwWeb.LoadRequest(new NSUrlRequest(new NSUrl("http://grouplighthouse.com")));
		}
	}
}