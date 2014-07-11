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
		const string s_lastUsedBaby = "LastUsedBaby";

		/// <summary>
		/// Loads the last used baby.
		/// </summary>
		/// <returns>The last used baby.</returns>
		public static Baby LoadLastUsedBaby()
		{
			var babyService = new BabyService();
			var userDefaults = NSUserDefaults.StandardUserDefaults;
			var babyId = userDefaults.StringForKey(s_lastUsedBaby);
			if (string.IsNullOrEmpty(babyId))
			{
				var baby = babyService.CreateBaby();
				userDefaults.SetString(baby.Id, s_lastUsedBaby);
				return baby;
			}
			else
			{
				return babyService.GetBaby(babyId);
			}
		}
	}
}