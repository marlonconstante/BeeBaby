using System;
using BeeBaby.Synchronization;
using BeeBaby.VisualElements;
using System.Drawing;
using System.Timers;
using BeeBaby.Network;

namespace BeeBaby.Controllers
{
	public abstract class SyncNavigationViewController : NavigationViewController
	{
		/// <summary>
		/// The timer interval.
		/// </summary>
		const double c_timerInterval = 15000d;

		/// <summary>
		/// Initializes the <see cref="BeeBaby.Controllers.SyncNavigationViewController"/> class.
		/// </summary>
		static SyncNavigationViewController()
		{
			SyncButton = new SyncButton(new RectangleF(6f, 0f, 24f, 24f));
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
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			RightBarButtonItem = SyncBarButtonItem;

			base.ViewDidLoad();
		}

		/// <summary>
		/// Raises the timer elapsed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		static void OnTimerElapsed(object sender, ElapsedEventArgs args)
		{
			if (Reachability.InternetConnectionStatus() == NetworkStatus.ReachableViaWiFiNetwork)
			{
				FileSyncManager.Instance.Synchronize(SyncButton, DateTime.MinValue);
			}
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
	}
}