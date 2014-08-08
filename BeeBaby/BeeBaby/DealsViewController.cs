using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Application;
using System.Text;
using Domain.Moment;

namespace BeeBaby
{
	public partial class DealsViewController : NavigationViewController
	{
		public DealsViewController(IntPtr handle) : base(handle)
		{
		}
			
		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			var baby = CurrentContext.Instance.CurrentBaby;
			var moment = new MomentService().GetLastMoment();

			var queryBuilder = new StringBuilder();
			queryBuilder.Append(string.Concat("Age=", baby.AgeInDays));
			queryBuilder.Append(string.Concat("&Gender=", baby.Gender));

			if (moment != null)
			{
				queryBuilder.Append(string.Concat("&latlng=", moment.Position.GetParameters()));
			}
			
			var builder = new UriBuilder("http://appserver.beebabyapp.com/busca.php");
			builder.Query = queryBuilder.ToString();
			
			vwWeb.LoadRequest(new NSUrlRequest(new NSUrl(builder.ToString()), NSUrlRequestCachePolicy.ReloadIgnoringLocalAndRemoteCacheData, 30));

		}
	}
}