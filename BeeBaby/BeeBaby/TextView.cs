using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
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
		}

		/// <summary>
		/// Adds the placeholder.
		/// </summary>
		void AddPlaceholder()
		{
			m_placeholder = new Label(new RectangleF(10f, 10f, Frame.Width - 20f, 0f));
			m_placeholder.TextColor = UIColor.LightGray;
			m_placeholder.Lines = 3;

			AddSubview(m_placeholder);
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