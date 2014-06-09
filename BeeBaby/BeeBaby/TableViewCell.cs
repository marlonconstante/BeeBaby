using System;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public class TableViewCell : UITableViewCell
	{
		public TableViewCell(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Dispose the specified disposing.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Discard.ReleaseSubviews(this);
				Discard.ReleaseOutlets(this);
				Discard.ReleaseProperties(this);
				Discard.ReleaseFields(this);
			}

			base.Dispose(disposing);
		}
	}
}