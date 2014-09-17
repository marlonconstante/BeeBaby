using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace BeeBaby.Util
{
	public abstract class Views
	{
		private Views()
		{
		}

		/// <summary>
		/// Changes the height and drag next views.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="constant">Constant.</param>
		public static void ChangeHeightAndDragNextViews(UIView view, float constant)
		{
			var superview = view.Superview;
			var nextViews = Views.GetNextViews(view);
			foreach (var constraint in superview.Constraints)
			{
				var value = constraint.FirstItem;
				if (value is UIView)
				{
					if (value == view)
					{
						if (NSLayoutAttribute.Height == constraint.FirstAttribute)
						{
							constraint.Constant += constant;
						}
					}
					else if (nextViews.Contains((UIView) value))
					{
						if (NSLayoutAttribute.Top == constraint.FirstAttribute)
						{
							constraint.Constant += constant;
						}
					}
				}
			}
			superview.LayoutSubviews();
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