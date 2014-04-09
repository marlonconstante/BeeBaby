using System;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public class PlaceholderTextFieldDelegate : UITextFieldDelegate
	{
		private Placeholder m_placeholder;

		public PlaceholderTextFieldDelegate()
		{
			m_placeholder = new Placeholder();
		}

		/// <summary>
		/// Editings the started.
		/// </summary>
		/// <param name="textField">Text field.</param>
		public override void EditingStarted(UITextField textField)
		{
			textField.Text = m_placeholder.GetInitialText(textField.Text);
		}

		/// <summary>
		/// Editings the ended.
		/// </summary>
		/// <param name="textField">Text field.</param>
		public override void EditingEnded(UITextField textField)
		{
			textField.Text = m_placeholder.GetFinalText(textField.Text);
		}
	}
}