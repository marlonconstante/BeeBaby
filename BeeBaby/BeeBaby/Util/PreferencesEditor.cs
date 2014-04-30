using System;
using MonoTouch.Foundation;
using Domain.Baby;

namespace BeeBaby.Util
{
	/// <summary>
	/// Preferences editor.
	/// </summary>
	static public class PreferencesEditor
	{
		/// <summary>
		/// Saves the last used baby.
		/// </summary>
		/// <param name="babyId">Baby identifier.</param>
		public static void SaveLastUsedBaby(string babyId)
		{
			var user = NSUserDefaults.StandardUserDefaults;
			user.SetString(babyId, "LastUsedBaby");
		}

		/// <summary>
		/// Loads the last used baby.
		/// </summary>
		/// <returns>The last used baby.</returns>
		public static Baby LoadLastUsedBaby()
		{
			var user = NSUserDefaults.StandardUserDefaults;
			var babyId = user.StringForKey("LastUsedBaby");

			return new BabyService().GetBaby(babyId);
		}
	}
}

