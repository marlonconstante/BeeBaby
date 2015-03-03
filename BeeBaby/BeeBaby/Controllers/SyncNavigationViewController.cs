using System;
using BeeBaby.Synchronization;
using BeeBaby.VisualElements;
using System.Drawing;
using System.Timers;
using BeeBaby.Network;
using MonoTouch.UIKit;
using PixateFreestyleLib;
using Parse;
using System.Collections.Generic;
using MonoTouch.Foundation;
using BeeBaby.Animation;

namespace BeeBaby.Controllers
{
	public abstract class SyncNavigationViewController : NavigationViewController
	{
		/// <summary>
		/// The timer interval.
		/// </summary>
		const double c_timerInterval = 30000d;

		/// <summary>
		/// Initializes the <see cref="BeeBaby.Controllers.SyncNavigationViewController"/> class.
		/// </summary>
		static SyncNavigationViewController()
		{
			LoadConfigButton();

			SyncButton = new SyncButton(new RectangleF(-6f, 0f, 24f, 24f));
			SyncBarButtonItem = new NavigationButtonItem(new RectangleF(0f, 0f, 24f, 24f), SyncButton);

			Timer = new Timer(c_timerInterval);
			Timer.Elapsed += OnTimerElapsed;
			Timer.Start();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Controllers.SyncNavigationViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public SyncNavigationViewController(IntPtr handle) : base(handle)
		{
			RightBarButtonItem = ConfigBarButtonItem;
			LeftBarButtonItem = SyncBarButtonItem;
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			SyncButton.Update();
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			WeakViewController = new WeakReference(this);
			ConfigButton.TouchUpInside += OpenConfigPage;
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			ConfigButton.TouchUpInside -= OpenConfigPage;
		}

		/// <summary>
		/// Raises the sync performed event.
		/// </summary>
		public virtual void OnSyncPerformed()
		{
		}

		/// <summary>
		/// Opens the config page.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void OpenConfigPage(object sender, EventArgs args)
		{
			ShowProgressWhilePerforming(() => {
				RootViewController.PerformSegue("segueConfig", sender as NSObject);
			}, false);
		}

		/// <summary>
		/// Raises the timer elapsed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		static async void OnTimerElapsed(object sender, ElapsedEventArgs args)
		{
			if (ParseUser.CurrentUser != null)
			{
				if (Reachability.InternetConnectionStatus() == NetworkStatus.ReachableViaWiFiNetwork)
				{
					if (await ParseCloud.CallFunctionAsync<bool>("IsSyncEnabled", new Dictionary<string, object>()))
					{
						if (await FileSyncManager.Instance.Synchronize(SyncButton))
						{
							if (WeakViewController != null && WeakViewController.IsAlive)
							{
								var viewController = (SyncNavigationViewController) WeakViewController.Target;
								viewController.InvokeOnMainThread(() => {
									viewController.OnSyncPerformed();
								});
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Loads the config button.
		/// </summary>
		static void LoadConfigButton()
		{
			ConfigButton = new Button(new RectangleF(6f, 0f, 24f, 24f));
			ConfigButton.SetStyleClass("settings");

			ConfigButton.Rotate(2d, () => {
				return ParseUser.CurrentUser == null;
			});

			ConfigBarButtonItem = new NavigationButtonItem(new RectangleF(0f, 0f, 24f, 24f), ConfigButton);
		}

		/// <summary>
		/// Gets or sets the timer.
		/// </summary>
		/// <value>The timer.</value>
		static Timer Timer { get; set; }

		/// <summary>
		/// Gets or sets the sync button.
		/// </summary>
		/// <value>The sync button.</value>
		static SyncButton SyncButton { get; set; }

		/// <summary>
		/// Gets or sets the sync bar button item.
		/// </summary>
		/// <value>The sync bar button item.</value>
		static NavigationButtonItem SyncBarButtonItem { get; set; }

		/// <summary>
		/// Gets or sets the config bar button item.
		/// </summary>
		/// <value>The config bar button item.</value>
		static NavigationButtonItem ConfigBarButtonItem { get; set; }

		/// <summary>
		/// Gets or sets the config button.
		/// </summary>
		/// <value>The config button.</value>
		static Button ConfigButton { get; set; }

		/// <summary>
		/// The weak view controller.
		/// </summary>
		static WeakReference WeakViewController;
	}
}