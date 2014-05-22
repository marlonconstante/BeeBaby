using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using MonoTouch.CoreGraphics;

namespace BeeBaby
{
	public class OrientationNotification
	{
		UIView[] m_views;

		private OrientationNotification(params UIView[] views)
		{
			m_views = views;

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
				foreach (var view in m_views)
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
		/// Add the specified views.
		/// </summary>
		/// <param name="views">Views.</param>
		public static OrientationNotification Add(params UIView[] views)
		{
			var orientationNotification = new OrientationNotification(views);
			orientationNotification.DidRotation();
			return orientationNotification;
		}
	}
}