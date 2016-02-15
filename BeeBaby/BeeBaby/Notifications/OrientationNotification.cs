using System;
using UIKit;
using Foundation;
using System.Collections.Generic;
using CoreGraphics;
using BeeBaby.Controllers;

namespace BeeBaby.Notifications
{
	public sealed class OrientationNotification : Notification
	{
		private OrientationNotification()
		{
			UIDevice.CurrentDevice.BeginGeneratingDeviceOrientationNotifications();

			// Orientation Did Change Notification
			NSNotificationCenter.DefaultCenter.AddObserver(UIDevice.OrientationDidChangeNotification, DidRotation);
		}

		/// <summary>
		/// Devices the angle.
		/// </summary>
		/// <returns>The angle.</returns>
		public float DeviceAngle()
		{
			return (float) Math.PI * DeviceRotation() / 180;
		}

		/// <summary>
		/// Devices the rotation.
		/// </summary>
		/// <returns>The rotation.</returns>
		public int DeviceRotation()
		{
			switch (UIDevice.CurrentDevice.Orientation)
			{
			case UIDeviceOrientation.LandscapeLeft:
				return 90;
			case UIDeviceOrientation.PortraitUpsideDown:
				return 180;
			case UIDeviceOrientation.LandscapeRight:
				return 270;
			default:
				return 0;
			}
		}

		/// <summary>
		/// Dids the rotation.
		/// </summary>
		/// <param name="notification">Notification.</param>
		void DidRotation(NSNotification notification = null)
		{
			CGAffineTransform transform = CGAffineTransform.MakeRotation(DeviceAngle());
			UIView.Animate(0.3d, () => {
				foreach (var view in GetSupportedOrientationViews())
				{
					view.Transform = transform;
				}
			},  () => {
				if (RotationFinished != null)
				{
					RotationFinished();
				}
			});
		}

		/// <summary>
		/// Gets the supported orientation views.
		/// </summary>
		/// <returns>The supported orientation views.</returns>
		IEnumerable<UIView> GetSupportedOrientationViews()
		{
			var viewController = CurrentViewController;
			if (viewController is BaseViewController)
			{
				return ((BaseViewController) viewController).GetSupportedOrientationViews();
			}
			return new UIView[] { };
		}

		/// <summary>
		/// Occurs when rotation finished.
		/// </summary>
		public event Action RotationFinished;

		/// <summary>
		/// Determines if is portrait.
		/// </summary>
		/// <returns><c>true</c> if is portrait; otherwise, <c>false</c>.</returns>
		public static bool IsPortrait() {
			var orientation = UIDevice.CurrentDevice.Orientation;
			return orientation == UIDeviceOrientation.Portrait ||
				orientation == UIDeviceOrientation.PortraitUpsideDown;
		}

		/// <summary>
		/// Determines if is landscape.
		/// </summary>
		/// <returns><c>true</c> if is landscape; otherwise, <c>false</c>.</returns>
		public static bool IsLandscape() {
			var orientation = UIDevice.CurrentDevice.Orientation;
			return orientation == UIDeviceOrientation.LandscapeLeft ||
			orientation == UIDeviceOrientation.LandscapeRight;
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public static OrientationNotification Initialize()
		{
			var orientationNotification = new OrientationNotification();
			orientationNotification.DidRotation();
			return orientationNotification;
		}
	}
}