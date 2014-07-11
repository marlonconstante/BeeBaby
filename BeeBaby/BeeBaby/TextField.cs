using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public partial class TextField : UITextField, IKeyboardSupport
	{
		public TextField(IntPtr handle) : base(handle)
		{
			OffsetHeight = 0f;
			ShouldReturn += (textField) => { 
				textField.ResignFirstResponder();
				return true;
			};
			ShouldBeginEditing += (textField) => {
				if (KeyboardNotification.KeyboardVisible)
				{
					InvokeInBackground(() => {
						InvokeOnMainThread(() => {
							textField.ResignFirstResponder();
							textField.BecomeFirstResponder();
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