using System;
using Foundation;
using Domain.Baby;

namespace BeeBaby.Util
{
	/// <summary>
	/// Preferences editor.
	/// </summary>
	public static class PreferencesEditor
	{
		/// <summary>
		/// Loads the last used baby.
		/// </summary>
		/// <returns>The last used baby.</returns>
		public static Baby LoadLastUsedBaby()
		{
			return GetBaby("LastUsedBaby");
		}

		/// <summary>
		/// Gets or sets a value indicating is on boarding viewed.
		/// </summary>
		/// <value><c>true</c> if is on boarding viewed; otherwise, <c>false</c>.</value>
		public static bool IsOnBoardingViewed {
			get {
				return UserDefaults.BoolForKey("IsOnBoardingViewed");
			}
			set {
				UserDefaults.SetBool(value, "IsOnBoardingViewed");
				UserDefaults.Synchronize();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating is config on boarding viewed.
		/// </summary>
		/// <value><c>true</c> if is config on boarding viewed; otherwise, <c>false</c>.</value>
		public static bool IsConfigOnBoardingViewed {
			get {
				return UserDefaults.BoolForKey("IsConfigOnBoardingViewed");
			}
			set {
				UserDefaults.SetBool(value, "IsConfigOnBoardingViewed");
				UserDefaults.Synchronize();
			}
		}

		/// <summary>
		/// Gets the device identifier.
		/// </summary>
		/// <value>The device identifier.</value>
		public static string DeviceId {
			get {
				return GetUniqueIdentifier("DeviceId");
			}
		}

		/// <summary>
		/// Gets the session identifier.
		/// </summary>
		/// <value>The session identifier.</value>
		public static string SessionId {
			get;
			private set;
		}

		/// <summary>
		/// Creates the session.
		/// </summary>
		public static void CreateSession()
		{
			SessionId = Guid.NewGuid().ToString();
		}

		/// <summary>
		/// Gets the unique identifier.
		/// </summary>
		/// <returns>The unique identifier.</returns>
		/// <param name="key">Key.</param>
		static string GetUniqueIdentifier(string key)
		{
			var id = UserDefaults.StringForKey(key);
			if (string.IsNullOrEmpty(id))
			{
				id = Guid.NewGuid().ToString();
				UserDefaults.SetString(id, key);
				UserDefaults.Synchronize();
			}
			return id;
		}

		/// <summary>
		/// Gets the baby.
		/// </summary>
		/// <returns>The baby.</returns>
		/// <param name="key">Key.</param>
		static Baby GetBaby(string key)
		{
			var babyService = new BabyService();
			var babyId = UserDefaults.StringForKey(key);
			if (string.IsNullOrEmpty(babyId))
			{
				var baby = babyService.CreateBaby();
				UserDefaults.SetString(baby.Id, key);
				UserDefaults.Synchronize();
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