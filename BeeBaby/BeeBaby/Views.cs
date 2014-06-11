using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public abstract class Views
	{
		private Views()
		{
		}

		/// <summary>
		/// Gets the subviews.
		/// </summary>
		/// <returns>The subviews.</returns>
		/// <param name="view">View.</param>
		public static List<UIView> GetSubviews(UIView view)
		{
			List<UIView> subviews = new List<UIView>();
			foreach (var subview in view.Subviews)
			{
				subviews.AddRange(GetSubviews(subview));
				subviews.Add(subview);
			}
			return subviews;
		}
	}
}