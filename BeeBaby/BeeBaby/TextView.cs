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
			OffsetHeight = 0f;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is keyboard animation.
		/// </summary>
		/// <value><c>true</c> if this instance is keyboard animation; otherwise, <c>false</c>.</value>
		public bool IsKeyboardAnimation {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the height of the offset.
		/// </summary>
		/// <value>The height of the offset.</value>
		public float OffsetHeight {
			get;
			set;
		}
	}
}