using System;
using System.Text.RegularExpressions;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.PCL.Globalization;

namespace BeeBaby.Util
{
	public static class Validators
	{
		/// <summary>
		/// Determines if is valid email the specified email.
		/// </summary>
		/// <returns><c>true</c> if is valid email the specified email; otherwise, <c>false</c>.</returns>
		/// <param name="email">Email.</param>
		public static bool IsValidEmail(string email)
		{
			return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
		}

		/// <summary>
		/// Determines if is valid password the specified password.
		/// </summary>
		/// <returns><c>true</c> if is valid password the specified password; otherwise, <c>false</c>.</returns>
		/// <param name="password">Password.</param>
		public static bool IsValidPassword(string password)
		{
			return !string.IsNullOrWhiteSpace(password);
		}

		/// <summary>
		/// Runs if valid login.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
		/// <param name="action">Action.</param>
		public static void RunIfValidLogin(string username, string password, Action action)
		{
			if (CheckEmail(username) && CheckPassword(password))
			{
				action();
			}
		}

		/// <summary>
		/// Runs if valid email.
		/// </summary>
		/// <param name="email">Email.</param>
		/// <param name="action">Action.</param>
		public static void RunIfValidEmail(string email, Action action)
		{
			if (CheckEmail(email))
			{
				action();
			}
		}

		/// <summary>
		/// Checks the email.
		/// </summary>
		/// <returns><c>true</c>, if email was checked, <c>false</c> otherwise.</returns>
		/// <param name="email">Email.</param>
		static bool CheckEmail(string email)
		{
			var valid = IsValidEmail(email);
			if (!valid)
			{
				new UIAlertView("Ops".Translate(), "WeNeedValidEmail".Translate(), null, "GotIt".Translate(), null).Show();
			}
			return valid;
		}

		/// <summary>
		/// Checks the password.
		/// </summary>
		/// <returns><c>true</c>, if password was checked, <c>false</c> otherwise.</returns>
		/// <param name="password">Password.</param>
		static bool CheckPassword(string password)
		{
			var valid = IsValidPassword(password);
			if (!valid)
			{
				new UIAlertView("Ops".Translate(), "PasswordRequired".Translate(), null, "GotIt".Translate(), null).Show();
			}
			return valid;
		}
	}
}