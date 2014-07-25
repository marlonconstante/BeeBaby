using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

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

			SetKeyboardAcessory();
		}

		/// <summary>
		/// Gets or sets the height of the offset.
		/// </summary>
		/// <value>The height of the offset.</value>
		public float OffsetHeight {
			get;
			set;
		}

		void SetKeyboardAcessory()
		{
			UIToolbar toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 240f, 44.0f));
			toolbar.TintColor = UIColor.FromRGB(0, 174, 173);
			toolbar.BarStyle = UIBarStyle.Default;
			toolbar.Translucent = true;
			toolbar.Items = new[] {
				new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
				new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
				{
					ResignFirstResponder();
				})
			};
			KeyboardAppearance = UIKeyboardAppearance.Light;
			InputAccessoryView = toolbar;
		}
	}
}