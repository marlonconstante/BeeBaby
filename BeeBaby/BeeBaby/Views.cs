using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;

namespace BeeBaby
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
					else if (nextViews.Contains((UIView)value))
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

		/// <summary>
		/// Resizes the heigth with text.
		/// </summary>
		/// <param name="label">Label.</param>
		/// <param name="maxHeight">Max height.</param>
		public static void ResizeHeigthWithText(UILabel label, float maxHeight = 960f)
		{
			float width = label.Frame.Width; 
			SizeF size = ((NSString)label.Text).StringSize(label.Font, new SizeF(width, maxHeight), UILineBreakMode.WordWrap);
			var labelFrame = label.Frame;
			labelFrame.Size = new SizeF(width, size.Height);
			label.Frame = labelFrame;
			label.LineBreakMode = UILineBreakMode.WordWrap;
		}
	}
}