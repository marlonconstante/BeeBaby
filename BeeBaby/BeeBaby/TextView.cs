using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public partial class TextView : UITextView, IKeyboardSupport
	{
		bool m_updateKeyboardPosition = true;

		public TextView (IntPtr handle) : base (handle)
		{
			OffsetHeight = 0f;
			ShouldBeginEditing += (textView) => {
				if (m_updateKeyboardPosition && KeyboardNotification.KeyboardVisible)
				{
					InvokeInBackground(() => {
						InvokeOnMainThread(() => {
							m_updateKeyboardPosition = false;
							textView.ResignFirstResponder();
							textView.BecomeFirstResponder();
							m_updateKeyboardPosition = true;
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