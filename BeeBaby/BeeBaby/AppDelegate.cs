using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Application;
using SQLite.Net;
using System;
using System.IO;
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
		public override UIWindow Window {
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

		/// <Docs>Reference to the UIApplication that invoked this delegate method.</Docs>
		/// <summary>
		/// Gets the supported interface orientations.
		/// </summary>
		/// <returns>The supported interface orientations.</returns>
		/// <param name="application">Application.</param>
		/// <param name="window">Window.</param>
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow window)
		{
			UIViewController topViewController = TopViewController(window);
			if (topViewController != null)
			{
				return topViewController.GetSupportedInterfaceOrientations();
			}
			else
			{
				return UIInterfaceOrientationMask.Portrait;
			}
		}

		/// <summary>
		/// Finisheds the launching.
		/// </summary>
		/// <param name="application">Application.</param>
		public override void FinishedLaunching(UIApplication application)
		{
//			FlurryAnalytics.Flurry.SetCrashReportingEnabled(true);
			FlurryAnalytics.Flurry.StartSession("FJBPW26D4GK7PZ568RBF");

			FBSettings.DefaultAppID = FacebookAppId;
			FBSettings.DefaultDisplayName = DisplayName;

			var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
		
			var home = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var dbPath = Path.Combine(home, "BeeBaby.sql");

			SQLiteConnection connection = new SQLiteConnection(platform, dbPath);
			DomainConfig.RegisterDependencies(connection);
			DomainConfig.InitializeGlobalization();

			InitProgressHUD();
		}

		/// <Docs>Reference to the UIApplication that invoked this delegate method.</Docs>
		/// Raises the activated event.
		/// </summary>
		/// <param name="application">Application.</param>
		public override void OnActivated(UIApplication application)
		{
			// We need to properly handle activation of the application with regards to SSO
			// (e.g., returning from iOS 6.0 authorization dialog or from fast app switching).
			FBSession.ActiveSession.HandleDidBecomeActive();
		}

		/// <summary>
		/// Tops the view controller.
		/// </summary>
		/// <returns>The view controller.</returns>
		/// <param name="window">Window.</param>
		UIViewController TopViewController(UIWindow window)
		{
			UIViewController topViewController = (window != null) ? window.RootViewController : null;

			if (topViewController != null)
			{
				while (topViewController.PresentedViewController != null)
				{
					topViewController = topViewController.PresentedViewController;
				}
			}

			return topViewController;
		}

		/// <summary>
		/// Inits the ProgressHUD.
		/// </summary>
		void InitProgressHUD()
		{
			var hud = ProgressHUD.Shared;
			hud.SetStyleClass("progress");

			var frame = hud.Frame;
			frame.Y = (float) Math.Ceiling(hud.Bounds.Height / 20f);
			hud.Frame = frame;
		}
	}
}