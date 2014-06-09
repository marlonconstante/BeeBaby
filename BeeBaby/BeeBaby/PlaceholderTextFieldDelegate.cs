using System;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public class PlaceholderTextFieldDelegate : UITextFieldDelegate
	{
		public PlaceholderTextFieldDelegate()
		{
			Placeholder = new Placeholder();
		}

		/// <summary>
		/// Editings the started.
		/// </summary>
		/// <param name="textField">Text field.</param>
		public override void EditingStarted(UITextField textField)
		{
			textField.Text = Placeholder.GetInitialText(textField.Text);
		}

		/// <summary>
		/// Editings the ended.
		/// </summary>
		/// <param name="textField">Text field.</param>
		public override void EditingEnded(UITextField textField)
		{
			textField.Text = Placeholder.GetFinalText(textField.Text);
		}

		/// <summary>
		/// Gets the placeholder.
		/// </summary>
		/// <value>The placeholder.</value>
		public Placeholder Placeholder {
			get;
			set;
		}
	}
}