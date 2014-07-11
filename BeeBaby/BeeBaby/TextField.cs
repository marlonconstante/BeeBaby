using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public partial class TextField : UITextField, IKeyboardSupport
	{
		bool m_updateKeyboardPosition = true;

		public TextField(IntPtr handle) : base(handle)
		{
			OffsetHeight = 0f;
			ShouldReturn += (textField) => { 
				textField.ResignFirstResponder();
				return true;
			};
			ShouldBeginEditing += (textField) => {
				if (m_updateKeyboardPosition && KeyboardNotification.KeyboardVisible)
				{
					InvokeInBackground(() => {
						InvokeOnMainThread(() => {
							m_updateKeyboardPosition = false;
							textField.ResignFirstResponder();
							textField.BecomeFirstResponder();
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