using System;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public class PlaceholderTextViewDelegate : UITextViewDelegate
	{
		private Placeholder placeholder;

		public PlaceholderTextViewDelegate()
		{
			placeholder = new Placeholder();
		}

		public override void EditingStarted(UITextView textView)
		{
			textView.Text = placeholder.GetInitialText(textView.Text);
		}

		public override void EditingEnded(UITextView textView)
		{
			textView.Text = placeholder.GetFinalText(textView.Text);
		}
	}
}