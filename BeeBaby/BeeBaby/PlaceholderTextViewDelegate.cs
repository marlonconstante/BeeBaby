using System;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public class PlaceholderTextViewDelegate : UITextViewDelegate
	{
		private Placeholder m_placeholder;

		public PlaceholderTextViewDelegate()
		{
			m_placeholder = new Placeholder();
		}

		/// <summary>
		/// Editings the started.
		/// </summary>
		/// <param name="textView">Text view.</param>
		public override void EditingStarted(UITextView textView)
		{
			textView.Text = m_placeholder.GetInitialText(textView.Text);
		}

		/// <summary>
		/// Editings the ended.
		/// </summary>
		/// <param name="textView">Text view.</param>
		public override void EditingEnded(UITextView textView)
		{
			textView.Text = m_placeholder.GetFinalText(textView.Text);
		}
	}
}