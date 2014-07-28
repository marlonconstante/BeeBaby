using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

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
			SetKeyboardAcessory();
		}

		/// <summary>
		/// Sets the keyboard acessory.
		/// </summary>
		void SetKeyboardAcessory()
		{
			var toolbar = new UIToolbar(new RectangleF(0f, 0f, 240f, 44f));
			toolbar.TintColor = UIColor.FromRGB(0, 174, 173);
			toolbar.BarStyle = UIBarStyle.Default;
			toolbar.Translucent = true;
			toolbar.Items = new[] {
				new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
				new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate {
					ShouldReturn(this);
				})
			};
			KeyboardAppearance = UIKeyboardAppearance.Light;
			InputAccessoryView = toolbar;
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