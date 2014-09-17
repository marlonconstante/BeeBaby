using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using BeeBaby.Notifications;

namespace BeeBaby
{
	public partial class TextField : UITextField, IKeyboardSupport
	{
		const string s_placeholderColorKey = "_placeholderLabel.textColor";
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

		/// <summary>
		/// Gets or sets the color of the placeholder.
		/// </summary>
		/// <value>The color of the placeholder.</value>
		public UIColor PlaceholderColor {
			get {
				return (UIColor) ValueForKeyPath(new NSString(s_placeholderColorKey));
			}
			set {
				SetValueForKeyPath(value, new NSString(s_placeholderColorKey));
			}
		}
	}
}