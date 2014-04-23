using System;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public class PlaceholderTextViewDelegate : UITextViewDelegate
	{
		public PlaceholderTextViewDelegate()
		{
			Placeholder = new Placeholder();
		}

		/// <summary>
		/// Editings the started.
		/// </summary>
		/// <param name="textView">Text view.</param>
		public override void EditingStarted(UITextView textView)
		{
			textView.Text = Placeholder.GetInitialText(textView.Text);
		}

		/// <summary>
		/// Editings the ended.
		/// </summary>
		/// <param name="textView">Text view.</param>
		public override void EditingEnded(UITextView textView)
		{
			textView.Text = Placeholder.GetFinalText(textView.Text);
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