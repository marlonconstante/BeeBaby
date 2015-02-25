using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.PCL.Globalization;

namespace BeeBaby.VisualElements
{
	public partial class SearchBar : UISearchBar
	{
		UIColor m_backgroundColor = UIColor.FromRGB(242, 245, 245);
		UIColor m_inputBorderColor = UIColor.FromRGB(230, 230, 229);

		public SearchBar(IntPtr handle) : base(handle)
		{
			BarTintColor = m_backgroundColor;
			Placeholder = "Search".Translate();
		}

		/// <Docs>Lays out subviews.</Docs>
		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			var view = Subviews[0];
			view.Layer.BorderWidth = 1f;
			view.Layer.BorderColor = m_backgroundColor.CGColor;

			foreach (var subview in view.Subviews)
			{
				if (subview.GetType() == typeof(UITextField))
				{
					subview.Layer.CornerRadius = 5f;
					subview.Layer.BorderWidth = 1f;
					subview.Layer.BorderColor = m_inputBorderColor.CGColor;
					subview.BackgroundColor = UIColor.White;
				}
			}
		}
	}
}