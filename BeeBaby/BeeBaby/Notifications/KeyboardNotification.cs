using System;
using UIKit;
using Foundation;
using CoreGraphics;
using BeeBaby.Util;
using BeeBaby.Controllers;
using BeeBaby.VisualElements;
using System.Drawing;

namespace BeeBaby.Notifications
{
	public sealed class KeyboardNotification : Notification
	{
		WeakReference m_weakResponder;

		private KeyboardNotification()
		{
			// Keyboard Up
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, KeyboardUpNotification);

			// Keyboard Down
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardDownNotification);

			KeyboardVisible = false;
		}

		/// <summary>
		/// Keyboards up notification.
		/// </summary>
		/// <param name="notification">Notification.</param>
		void KeyboardUpNotification(NSNotification notification)
		{
			m_weakResponder = new WeakReference(GetFirstResponder(CurrentViewController.View));

			MoveScroll(!KeyboardVisible, notification);
		}

		/// <summary>
		/// Keyboards down notification.
		/// </summary>
		/// <param name="notification">Notification.</param>
		void KeyboardDownNotification(NSNotification notification)
		{
			if (KeyboardVisible)
			{
				MoveScroll(false, notification);
			}
		}

		/// <summary>
		/// Moves the scroll.
		/// </summary>
		/// <param name="up">If set to <c>true</c> up.</param>
		/// <param name="notification">Notification.</param>
		void MoveScroll(bool up, NSNotification notification)
		{
			var firstResponder = m_weakResponder.Target as UIView;
			var iKeyboardSupport = firstResponder as IKeyboardSupport;
			if (iKeyboardSupport != null)
			{
				var view = firstResponder.Superview;
				var scrollView = view as UIScrollView;
				if (scrollView != null)
				{
					var rectangle = UIKeyboard.FrameBeginFromNotification(notification);
					var navigationController = Windows.GetTopViewController(firstResponder.Window).NavigationController;

					UIView.Animate(0.3d, () =>
					{
						var contentSize = scrollView.ContentSize;
						contentSize.Height += up ? rectangle.Height : -rectangle.Height;
						scrollView.ContentSize = contentSize;
					}, () => {
						if (up)
						{
							var isAdjustsInsets = navigationController.AutomaticallyAdjustsScrollViewInsets;

							var bottom = firstResponder.Frame.Y + firstResponder.Frame.Height + iKeyboardSupport.OffsetHeight;
							var spare = UIScreen.MainScreen.Bounds.Height + (isAdjustsInsets ? 0f : -64f) - rectangle.Height;

							scrollView.SetContentOffset(new PointF(0f, Math.Max(isAdjustsInsets ? -64f : 0f, bottom - spare)), true);
						}
					});
				}
			}
			KeyboardVisible = up;
		}

		/// <summary>
		/// Get the first input that responds view.
		/// </summary>
		/// <param name="view">View.</param>
		UIView GetFirstResponder(UIView view)
		{
			var elements = Views.GetSubviews(view);
			elements.Reverse();
			foreach (UIView element in elements)
			{
				if (element.IsFirstResponder)
				{
					return element;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BeeBaby.KeyboardNotification"/> keyboard visible.
		/// </summary>
		/// <value><c>true</c> if keyboard visible; otherwise, <c>false</c>.</value>
		public static bool KeyboardVisible {
			get;
			set;
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public static KeyboardNotification Initialize()
		{
			return new KeyboardNotification();
		}

	}
}