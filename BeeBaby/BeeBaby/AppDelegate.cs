using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Application;
using SQLite.Net;
using System;
using System.IO;
using BeeBaby;
using BigTed;
using PixateFreestyleLib;
using Parse;
using BeeBaby.Globalization;
using Infrastructure.Configuration;
using MonoTouch.FacebookConnect;

namespace BeeBaby
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		/// <Docs>Reference to the UIApplication that invoked this delegate method.</Docs>
		/// <remarks>To be added.</remarks>
		/// <summary>
		/// Raises the activated event.
		/// </summary>
		/// <param name="application">Application.</param>
		public override void OnActivated(UIApplication application)
		{
			FBAppEvents.ActivateApp();
		}

		/// <Docs>Reference to the UIApplication that invoked this delegate method.</Docs>
		/// <remarks>To be added.</remarks>
		/// <summary>
		/// Wills the terminate.
		/// </summary>
		/// <param name="application">Application.</param>
		public override void WillTerminate(UIApplication application)
		{
			FlurryAnalytics.Flurry.LogEvent("Fechou o App.");
		}

		/// <summary>
		/// Finisheds the launching.
		/// </summary>
		/// <param name="application">Application.</param>
		public override void FinishedLaunching(UIApplication application)
		{
			ThirdPartyIntegrationsRegister();

			var currentCulture = SHCultureInfo.From(NSLocale.CurrentLocale);

			var connection = SetupConnection();
			DomainConfig.InitializeGlobalization(currentCulture);
			DomainConfig.RegisterDependencies(connection, currentCulture);

			KeyboardNotification.Initialize();
			OrientationNotification.Initialize();
			MediaLibrary.Instance.Initialize();

			InitProgressHUD();
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
			var topViewController = Windows.GetTopViewController(window);
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
		/// Setups the connection.
		/// </summary>
		/// <returns>The connection.</returns>
		SQLiteConnection SetupConnection()
		{
			var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			var home = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var dbPath = Path.Combine(home, "BeeBaby.sql");
			SQLiteConnection connection = new SQLiteConnection(platform, dbPath);
			return connection;
		}

		/// <summary>
		/// Thirds the party integrations register.
		/// </summary>
		void ThirdPartyIntegrationsRegister()
		{
			InitFacebook();
			InitParse();
			InitFlurry();
		}

		/// <summary>
		/// Inits the facebook.
		/// </summary>
		void InitFacebook()
		{
			FBSettings.DefaultAppID = "689915811057213";
			FBSettings.DefaultDisplayName = "BeeBaby";
		}

		/// <summary>
		/// Inits the parse.
		/// </summary>
		void InitParse()
		{
			ParseClient.Initialize("YHCep6FtlizzWo4SEHWVUimSoFwBykLXkwJxcnXm", "eLsMXi61ILhUyOAIlmjxGE8L74GmoIGsWvqUwTYI");
		}

		/// <summary>
		/// Inits the flurry.
		/// </summary>
		void InitFlurry()
		{
			FlurryAnalytics.Flurry.StartSession("FJBPW26D4GK7PZ568RBF");
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

		/// <summary>
		/// Gets or sets the window.
		/// </summary>
		/// <value>The window.</value>
		public override UIWindow Window {
			get;
			set;
		}
	}
}