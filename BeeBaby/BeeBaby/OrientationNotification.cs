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
			return (float) Math.PI * GetDeviceRotation() / 180;
		}

		/// <summary>
		/// Gets the device rotation.
		/// </summary>
		/// <returns>The device rotation.</returns>
		public static int GetDeviceRotation()
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
		private void DidRotation(NSNotification notification)
		{
			int deviceRotation = GetDeviceRotation();
			bool landscape = deviceRotation == 90 || deviceRotation == 270;
			CGAffineTransform transform = CGAffineTransform.MakeRotation(GetDeviceAngle());
			UIView.Animate(0.2d, () => {
				foreach (var view in m_views)
				{
					if (view.GetType() == typeof(UILabel))
					{
						UILabel label = (UILabel) view;
						label.TextAlignment	= landscape ? UITextAlignment.Center : UITextAlignment.Left;
					}
					view.Transform = transform;
				}
			});
		}
	}
}

