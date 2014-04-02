using System;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public class PlaceholderTextFieldDelegate : UITextFieldDelegate
	{
		private Placeholder placeholder;

		public PlaceholderTextFieldDelegate()
		{
			placeholder = new Placeholder();
		}

		public override void EditingStarted(UITextField textField)
		{
			textField.Text = placeholder.GetInitialText(textField.Text);
		}

		public override void EditingEnded(UITextField textField)
		{
			textField.Text = placeholder.GetFinalText(textField.Text);
		}
	}
}