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
			var babyId = UserDefaults.StringForKey(s_lastUsedBaby);
			if (string.IsNullOrEmpty(babyId))
			{
				var baby = babyService.CreateBaby();
				UserDefaults.SetString(baby.Id, s_lastUsedBaby);
				return baby;
			}
			else
			{
				return babyService.GetBaby(babyId);
			}
		}

		/// <summary>
		/// Gets the user defaults.
		/// </summary>
		/// <value>The user defaults.</value>
		static NSUserDefaults UserDefaults {
			get {
				return NSUserDefaults.StandardUserDefaults;
			}
		}
	}
}