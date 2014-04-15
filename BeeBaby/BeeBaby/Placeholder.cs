using System;

namespace BeeBaby
{
	public class Placeholder
	{
		string m_defaultText;

		public Placeholder()
		{
		}

		/// <summary>
		/// Returns the blank text if it is the default.
		/// </summary>
		/// <param name="text">Text.</param>
		public string GetInitialText(string text)
		{
			if (m_defaultText == null)
			{
				m_defaultText = text;
			}
			return isEmpty(text) ? "" : text;
		}

		/// <summary>
		/// Returns the default text if nothing has been entered.
		/// </summary>
		/// <param name="text">Text.</param>
		public string GetFinalText(string text)
		{
			return string.IsNullOrEmpty(text) ? m_defaultText : text;
		}

		/// <summary>
		/// Checks if the text is empty.
		/// </summary>
		/// <param name="text">Text.</param>
		public bool isEmpty(string text)
		{
			return text.Equals(m_defaultText);
		}
	}
}

