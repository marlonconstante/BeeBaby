using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public partial class TextView : UITextView, IKeyboardSupport
	{
		public TextView (IntPtr handle) : base (handle)
		{
			IsKeyboardAnimation = false;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is keyboard animation.
		/// </summary>
		/// <value><c>true</c> if this instance is keyboard animation; otherwise, <c>false</c>.</value>
		public bool IsKeyboardAnimation {
			get;
			set;
		}
	}
}