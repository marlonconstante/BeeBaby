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

		public OrientationNotification(params UIView[] views)
		{
			m_views = views;

			UIDevice.CurrentDevice.BeginGeneratingDeviceOrientationNotifications();

			// Orientation Did Change Notification
			NSNotificationCenter.DefaultCenter.AddObserver(UIDevice.OrientationDidChangeNotification, DidRotation);
		}

		/// <summary>
		/// Gets the device angle.
		/// </summary>
		/// <returns>The device angle.</returns>
		public static float GetDeviceAngle()
		{
			float rotate;
			switch (UIDevice.CurrentDevice.Orientation) {
			case UIDeviceOrientation.LandscapeLeft:
				rotate = 90f;
				break;
			case UIDeviceOrientation.PortraitUpsideDown:
				rotate = 180f;
				break;
			case UIDeviceOrientation.LandscapeRight:
				rotate = 270f;
				break;
			default:
				rotate = 0f;
				break;
			}
			return (float) Math.PI * rotate / 180f;
		}

		/// <summary>
		/// Dids the rotation.
		/// </summary>
		/// <param name="notification">Notification.</param>
		private void DidRotation(NSNotification notification)
		{
			CGAffineTransform transform = CGAffineTransform.MakeRotation(GetDeviceAngle());
			UIView.Animate(0.2d, () => {
				foreach (var view in m_views)
				{
					view.Transform = transform;
				}
			});
		}
	}
}

