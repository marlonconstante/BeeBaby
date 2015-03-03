using System;
using System.IO;
using System.Net;
using Application;
using BigTed;
using MonoTouch.FacebookConnect;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Parse;
using PixateFreestyleLib;
using RestSharp;
using SQLite.Net;
using Xamarin;
using BeeBaby.Globalization;
using BeeBaby.Media;
using BeeBaby.Notifications;
using BeeBaby.Util;
using MonoTouch.ObjCRuntime;

namespace BeeBaby
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		const string ParseApplicationId = "YHCep6FtlizzWo4SEHWVUimSoFwBykLXkwJxcnXm";
		const string ParseRestApiKey = "fN0UAe2LfUygCeKSUbDqDUD7fVtlOv0oyIBBYAsz";
		const string ParseMasterKey = "pTgx6IPaCTicPzecba03g2rcDeZwknSgukMxUBFX";
		const string ParseDotNetKey = "eLsMXi61ILhUyOAIlmjxGE8L74GmoIGsWvqUwTYI";
		const string FlurryApiKey = "FJBPW26D4GK7PZ568RBF";
		const string FacebookApplicationId = "689915811057213";
		const string FacebookDisplayName = "BeeBaby";

		/// <Docs>Reference to the UIApplication that invoked this delegate method.</Docs>
		/// <summary>
		/// Registereds for remote notifications.
		/// </summary>
		/// <param name="application">Application.</param>
		/// <param name="deviceToken">Device token.</param>
		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			try
			{
				var client = new RestClient("https://api.parse.com");
				var request = new RestRequest("1/installations/", Method.POST);

				var token = deviceToken.Description.Replace(" ", string.Empty).Replace("<", string.Empty).Replace(">", string.Empty);
				var languageChannel = SHCultureInfo.From(NSLocale.CurrentLocale).Name;
				var json = "{ \"deviceType\": \"ios\", \"deviceToken\": \"" + token + "\", \"channels\": [\"" + languageChannel + "\"] }";

				request.Credentials = new NetworkCredential(ParseApplicationId, ParseMasterKey);

				request.AddHeader("Content-Type", "application/json");
				request.AddHeader("X-Parse-Application-Id", ParseApplicationId);
				request.AddHeader("X-Parse-REST-API-Key", ParseRestApiKey);

				request.Parameters.Clear();
				request.AddParameter("application/json", json, ParameterType.RequestBody);

				client.ExecuteAsync(request, response => {
					Console.WriteLine("Dispositivo registrado com sucesso:\n" + response.Content);
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine("Ocorreu um erro no registro das notificações remotas para o dispositivo:\n" + ex.Message);
			}
		}

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
			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			ThirdPartyIntegrationsRegister();
			RegisterNotifications(application);

			application.StatusBarStyle = UIStatusBarStyle.LightContent;

			var currentCulture = SHCultureInfo.From(NSLocale.CurrentLocale);

			var connection = SetupConnection();
			DomainConfig.InitializeGlobalization(currentCulture);
			DomainConfig.RegisterDependencies(connection, currentCulture);

			KeyboardNotification.Initialize();
			OrientationNotification.Initialize();
			MediaLibrary.Instance.Initialize();

			PreferencesEditor.CreateSession();

			InitProgressHUD();

			Window.RootViewController = RootViewController;
			Window.MakeKeyAndVisible();
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
		/// Registers the notifications.
		/// </summary>
		/// <param name="application">Application.</param>
		void RegisterNotifications(UIApplication application)
		{
			if (application.RespondsToSelector(new Selector("registerForRemoteNotifications")))
			{
				var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge, null);
				application.RegisterUserNotificationSettings(notificationSettings);
				application.RegisterForRemoteNotifications();
			}
			else
			{
				var notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge;
				application.RegisterForRemoteNotificationTypes(notificationTypes);
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
			InitInsights();
		}

		/// <summary>
		/// Inits the insights.
		/// </summary>
		void InitInsights()
		{
			Insights.Initialize("adcd994ba92f33cf0ee12721261321c4c9c7632f");
		}

		/// <summary>
		/// Inits the facebook.
		/// </summary>
		void InitFacebook()
		{
			FBSettings.DefaultAppID = FacebookApplicationId;
			FBSettings.DefaultDisplayName = FacebookDisplayName;
		}

		/// <summary>
		/// Inits the parse.
		/// </summary>
		void InitParse()
		{
			ParseClient.Initialize(ParseApplicationId, ParseDotNetKey);
		}

		/// <summary>
		/// Inits the flurry.
		/// </summary>
		void InitFlurry()
		{
			FlurryAnalytics.Flurry.StartSession(FlurryApiKey);
		}

		/// <summary>
		/// Inits the ProgressHUD.
		/// </summary>
		void InitProgressHUD()
		{
			var hud = ProgressHUD.Shared;
			hud.SetStyleClass("progress");
			hud.HudFont = UIFont.FromName("Quicksand", 16f);

			var frame = hud.Frame;
			frame.Y = (float) Math.Ceiling(hud.Bounds.Height / 20f);
			hud.Frame = frame;
		}

		/// <summary>
		/// Gets the root view controller.
		/// </summary>
		/// <value>The root view controller.</value>
		UIViewController RootViewController {
			get {
				var storyboardId = "MainNavigationController";
				var board = UIStoryboard.FromName("MainStoryboard", null);
				return board.InstantiateViewController(storyboardId) as UIViewController;
			}
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