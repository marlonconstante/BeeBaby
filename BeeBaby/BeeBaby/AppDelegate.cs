using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Application;
using SQLite.Net;
using System;
using System.IO;
using MonoTouch.SlideoutNavigation;
using MonoTouch.Dialog;
using BeeBaby;
using MonoTouch.FacebookConnect;
using BigTed;
using PixateFreestyleLib;

namespace BeeBaby
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		public override UIWindow Window
		{
			get;
			set;
		}

		// Get your own App ID at developers.facebook.com/apps
		const string FacebookAppId = "1445498505688180";
//		const string FacebookUrlSchemeSuffix = "fb1445498505688180";
		const string DisplayName = "BeeBaby";


		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation(UIApplication application)
		{
		}
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground(UIApplication application)
		{
		}
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground(UIApplication application)
		{
		}
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate(UIApplication application)
		{
		}

		/// <summary>
		/// Finisheds the launching.
		/// </summary>
		/// <param name="application">Application.</param>
		public override void FinishedLaunching(UIApplication application)
		{
			FBSettings.DefaultAppID = FacebookAppId;
			FBSettings.DefaultDisplayName = DisplayName;
//			FBSettings.DefaultUrlSchemeSuffix = FacebookUrlSchemeSuffix;


			var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();

			var home = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var dbPath = Path.Combine(home, "BeeBaby.sql");

			SQLiteConnection connection = new SQLiteConnection(platform, dbPath);
			DomainConfig.RegisterDependencies(connection);
			DomainConfig.InitializeGlobalization();
			ProgressHUD.Shared.SetStyleClass("progress");
		}

		/// <Docs>Reference to the UIApplication that invoked this delegate method.</Docs>
		/// Raises the activated event.
		/// </summary>
		/// <param name="application">Application.</param>
		public override void OnActivated (UIApplication application)
		{
			// We need to properly handle activation of the application with regards to SSO
			// (e.g., returning from iOS 6.0 authorization dialog or from fast app switching).
			FBSession.ActiveSession.HandleDidBecomeActive();
		}
	}
}