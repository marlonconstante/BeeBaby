using System;

namespace BeBabby
{
	public class Placeholder
	{
		private string defaultText;

		public Placeholder()
		{
		}

		/// <summary>
		/// Returns the blank text if it is the default.
		/// </summary>
		/// <param name="text">Text.</param>
		public string GetInitialText(string text)
		{
			if (defaultText == null)
			{
				defaultText = text;
			}
			return isEmpty(text) ? "" : text;
		}

		/// <summary>
		/// Returns the default text if nothing has been entered.
		/// </summary>
		/// <param name="text">Text.</param>
		public string GetFinalText(string text)
		{
			return string.IsNullOrEmpty(text) ? defaultText : text;
		}

		/// <summary>
		/// Checks if the text is empty.
		/// </summary>
		/// <param name="text">Text.</param>
		public bool isEmpty(string text)
		{
			return text.Equals(defaultText);
		}
	}
}

