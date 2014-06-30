using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Application;
using System.Text;

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

			var baby = CurrentContext.Instance.CurrentBaby;
			var queryBuilder = new StringBuilder();
			queryBuilder.Append(string.Concat("Age=", baby.AgeInDays));
			queryBuilder.Append(string.Concat("&Gender=", baby.Gender));

			var builder = new UriBuilder("http://appserver.beebabyapp.com/busca.php");
			builder.Query = queryBuilder.ToString();

			vwWeb.LoadRequest(new NSUrlRequest(new NSUrl(builder.ToString())));
		}
	}
}