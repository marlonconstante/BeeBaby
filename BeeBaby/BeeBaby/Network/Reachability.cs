using System;
using System.Net;
using MonoTouch.SystemConfiguration;
using MonoTouch.CoreFoundation;

namespace BeeBaby.Network
{
	/// <summary>
	/// Network status.
	/// </summary>
	public enum NetworkStatus
	{
		NotReachable,
		ReachableViaCarrierDataNetwork,
		ReachableViaWiFiNetwork
	}

	/// <summary>
	/// Reachability.
	/// </summary>
	public static class Reachability
	{
		/// <summary>
		/// The name of the host.
		/// </summary>
		const string HostName = "www.google.com";

		/// <summary>
		/// The default route reachability.
		/// </summary>
		static NetworkReachability DefaultRouteReachability;

		/// <summary>
		/// The ad hoc wi fi network reachability.
		/// </summary>
		static NetworkReachability AdHocWiFiNetworkReachability;

		/// <summary>
		/// The remote host reachability.
		/// </summary>
		static NetworkReachability RemoteHostReachability;

		/// <summary>
		/// Determines if is network available the specified flags.
		/// </summary>
		/// <returns><c>true</c> if is network available the specified flags; otherwise, <c>false</c>.</returns>
		/// <param name="flags">Flags.</param>
		static bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
		{
			if (DefaultRouteReachability == null)
			{
				DefaultRouteReachability = new NetworkReachability(new IPAddress(0));
				DefaultRouteReachability.SetNotification(OnChange);
				DefaultRouteReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
			}

			return DefaultRouteReachability.TryGetFlags(out flags) && IsReachableWithoutRequiringConnection(flags);
		}

		/// <summary>
		/// Raises the change event.
		/// </summary>
		/// <param name="flags">Flags.</param>
		static void OnChange(NetworkReachabilityFlags flags)
		{
			var changed = ReachabilityChanged;
			if (changed != null)
			{
				changed(null, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Determines if is reachable without requiring connection the specified flags.
		/// </summary>
		/// <returns><c>true</c> if is reachable without requiring connection the specified flags; otherwise, <c>false</c>.</returns>
		/// <param name="flags">Flags.</param>
		public static bool IsReachableWithoutRequiringConnection(NetworkReachabilityFlags flags)
		{
			bool isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;
			bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

			if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
			{
				noConnectionRequired = true;
			}

			return isReachable && noConnectionRequired;
		}

		/// <summary>
		/// Determines if is host reachable the specified host.
		/// </summary>
		/// <returns><c>true</c> if is host reachable the specified host; otherwise, <c>false</c>.</returns>
		/// <param name="host">Host.</param>
		public static bool IsHostReachable(string host)
		{
			if (string.IsNullOrEmpty(host))
			{
				return false;
			}

			using (var net = new NetworkReachability(host))
			{
				NetworkReachabilityFlags flags;

				if (net.TryGetFlags(out flags))
				{
					return IsReachableWithoutRequiringConnection(flags);
				}
			}

			return false;
		}

		/// <summary>
		/// Determines if is ad hoc wi fi network available the specified flags.
		/// </summary>
		/// <returns><c>true</c> if is ad hoc wi fi network available the specified flags; otherwise, <c>false</c>.</returns>
		/// <param name="flags">Flags.</param>
		public static bool IsAdHocWiFiNetworkAvailable(out NetworkReachabilityFlags flags)
		{
			if (AdHocWiFiNetworkReachability == null)
			{
				AdHocWiFiNetworkReachability = new NetworkReachability(new IPAddress(new byte [] { 169, 254, 0, 0 }));
				AdHocWiFiNetworkReachability.SetNotification(OnChange);
				AdHocWiFiNetworkReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
			}

			return AdHocWiFiNetworkReachability.TryGetFlags(out flags) && IsReachableWithoutRequiringConnection(flags);
		}

		/// <summary>
		/// Remotes the host status.
		/// </summary>
		/// <returns>The host status.</returns>
		public static NetworkStatus RemoteHostStatus()
		{
			NetworkReachabilityFlags flags;
			bool reachable;

			if (RemoteHostReachability == null)
			{
				RemoteHostReachability = new NetworkReachability(HostName);

				reachable = RemoteHostReachability.TryGetFlags(out flags);

				RemoteHostReachability.SetNotification(OnChange);
				RemoteHostReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
			}
			else
			{
				reachable = RemoteHostReachability.TryGetFlags(out flags);
			}

			if (!reachable)
			{
				return NetworkStatus.NotReachable;
			}
			else if (!IsReachableWithoutRequiringConnection(flags))
			{
				return NetworkStatus.NotReachable;
			}
			else if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
			{
				return NetworkStatus.ReachableViaCarrierDataNetwork;
			}

			return NetworkStatus.ReachableViaWiFiNetwork;
		}

		/// <summary>
		/// Internets the connection status.
		/// </summary>
		/// <returns>The connection status.</returns>
		public static NetworkStatus InternetConnectionStatus()
		{
			NetworkReachabilityFlags flags;

			bool defaultNetworkAvailable = IsNetworkAvailable(out flags);
			if (defaultNetworkAvailable && ((flags & NetworkReachabilityFlags.IsDirect) != 0))
			{
				return NetworkStatus.NotReachable;
			}
			else if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
			{
				return NetworkStatus.ReachableViaCarrierDataNetwork;
			}
			else if (flags == 0)
			{
				return NetworkStatus.NotReachable;
			}

			return NetworkStatus.ReachableViaWiFiNetwork;
		}

		/// <summary>
		/// Locals the wifi connection status.
		/// </summary>
		/// <returns>The wifi connection status.</returns>
		public static NetworkStatus LocalWifiConnectionStatus()
		{
			NetworkReachabilityFlags flags;

			if (IsAdHocWiFiNetworkAvailable(out flags))
			{
				if ((flags & NetworkReachabilityFlags.IsDirect) != 0)
				{
					return NetworkStatus.ReachableViaWiFiNetwork;
				}
			}

			return NetworkStatus.NotReachable;
		}

		/// <summary>
		/// Occurs when reachability changed.
		/// </summary>
		public static event EventHandler ReachabilityChanged;
	}
}