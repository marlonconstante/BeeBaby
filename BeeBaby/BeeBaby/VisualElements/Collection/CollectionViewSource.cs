using System;
using MonoTouch.UIKit;
using BeeBaby.Util;

namespace BeeBaby.VisualElements
{
	public abstract class CollectionViewSource : UICollectionViewSource
	{
		public CollectionViewSource()
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
				Discard.ReleaseProperties(this);
				Discard.ReleaseFields(this);
			}

			base.Dispose(disposing);
		}
	}
}