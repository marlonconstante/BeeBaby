using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using BeeBaby.Util;
using BeeBaby.Notifications;

namespace BeeBaby.VisualElements
{
	public partial class TextView : UITextView, IKeyboardSupport
	{
		Label m_placeholder;
		bool m_updateKeyboardPosition = true;

		public TextView(IntPtr handle) : base(handle)
		{
			OffsetHeight = 0f;
			ShouldBeginEditing += (textView) => {
				m_placeholder.Hidden = true;
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
			ShouldEndEditing += (textView) => {
				m_placeholder.Hidden = IsHiddenPlaceholder();
				return true;
			};
			AddPlaceholder();
			SetKeyboardAcessory();
		}

		/// <summary>
		/// Adds the placeholder.
		/// </summary>
		void AddPlaceholder()
		{
			m_placeholder = new Label(new RectangleF(10f, 10f, 300f, 34f));
			m_placeholder.TextColor = UIColor.LightGray;

			AddSubview(m_placeholder);
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
					ResignFirstResponder();
				})
			};
			KeyboardAppearance = UIKeyboardAppearance.Light;
			InputAccessoryView = toolbar;
		}


		/// <summary>
		/// Determines whether this instance is hidden placeholder.
		/// </summary>
		/// <returns><c>true</c> if this instance is hidden placeholder; otherwise, <c>false</c>.</returns>
		bool IsHiddenPlaceholder()
		{
			return !string.IsNullOrEmpty(Text);
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
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public override string Text {
			get {
				return base.Text;
			}
			set {
				base.Text = value;
				m_placeholder.Hidden = IsHiddenPlaceholder();
			}
		}

		/// <summary>
		/// Gets or sets the placeholder.
		/// </summary>
		/// <value>The placeholder.</value>
		public string Placeholder {
			get {
				return m_placeholder.Text;
			}
			set {
				m_placeholder.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the placeholder.
		/// </summary>
		/// <value>The color of the placeholder.</value>
		public UIColor PlaceholderColor {
			get {
				return m_placeholder.TextColor;
			}
			set {
				m_placeholder.TextColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the placeholder frame.
		/// </summary>
		/// <value>The placeholder frame.</value>
		public RectangleF PlaceholderFrame {
			get {
				return m_placeholder.Frame;
			}
			set {
				m_placeholder.Frame = value;
			}
		}

		/// <summary>
		/// Dispose the specified disposing.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Discard.ReleaseSubviews(this);
				Discard.ReleaseProperties(this);
				Discard.ReleaseFields(this);
			}

			base.Dispose(disposing);
		}
	}
}