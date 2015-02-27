using System;
using System.Drawing;

namespace BeeBaby.Controllers
{
	public partial class ConfigViewController : NavigationViewController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Controllers.ConfigViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public ConfigViewController (IntPtr handle) : base (handle)
		{
		}

		/// <summary>
		/// Views the did layout subviews.
		/// </summary>
		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			if (scrView.ContentSize == SizeF.Empty)
			{
				scrView.ContentSize = new SizeF(320f, 504f);
			}
		}
	}
}