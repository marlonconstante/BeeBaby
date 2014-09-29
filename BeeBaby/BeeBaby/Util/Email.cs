using System;
using System.Text.RegularExpressions;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby.Util
{
	public static class Email
	{
		/// <summary>
		/// Determines if is valid the specified email.
		/// </summary>
		/// <returns><c>true</c> if is valid the specified email; otherwise, <c>false</c>.</returns>
		/// <param name="email">Email.</param>
		public static bool IsValid(string email)
		{
			return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
		}

		/// <summary>
		/// Run action if valid email.
		/// </summary>
		/// <param name="email">Email.</param>
		/// <param name="action">Action.</param>
		public static void RunIfValid(string email, Action action)
		{
			if (IsValid(email))
			{
				action();
			}
			else
			{
				new UIAlertView("Ops".Translate(), "WeNeedValidEmail".Translate(), null, "GotIt".Translate(), null).Show();
			}
		}
	}
}