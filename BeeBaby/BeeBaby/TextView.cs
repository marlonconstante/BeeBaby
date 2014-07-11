using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public partial class TextView : UITextView, IKeyboardSupport
	{
		public TextView (IntPtr handle) : base (handle)
		{
			OffsetHeight = 0f;
			ShouldBeginEditing += (textView) => {
				if (KeyboardNotification.KeyboardVisible)
				{
					InvokeInBackground(() => {
						InvokeOnMainThread(() => {
							textView.ResignFirstResponder();
							textView.BecomeFirstResponder();
						});
					});
				}
				return true;
			};
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