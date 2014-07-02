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

		/// <summary>
		/// Gets the next views.
		/// </summary>
		/// <returns>The next views.</returns>
		/// <param name="view">View.</param>
		public static List<UIView> GetNextViews(UIView view)
		{
			var nextViews = new List<UIView>();
			bool addSubview = false;
			foreach (var subview in view.Superview.Subviews)
			{
				if (addSubview)
				{
					nextViews.Add(subview);
				}
				else
				{
					addSubview = subview == view;
				}
			}
			return nextViews;
		}
	}
}