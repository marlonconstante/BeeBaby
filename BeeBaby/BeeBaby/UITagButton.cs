using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public class UITagButton : UIButton
	{
		public UITagButton(RectangleF frame) : base(frame)
		{
		}

		/// <summary>
		/// Gets or sets the name of the tag.
		/// </summary>
		/// <value>The name of the tag.</value>
		public string TagName
		{
			get;
			set;
		}
	}
}

