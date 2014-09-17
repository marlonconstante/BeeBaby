using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using BeeBaby.Util;
using BeeBaby.Controllers;

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
			var viewController = CurrentViewController;
			if (viewController is BaseViewController)
			{
				((BaseViewController) viewController).StartEditing();
			}
			m_weakResponder = new WeakReference(GetFirstResponder(viewController.View));

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
					var contentSize = scrollView.ContentSize;
					var rectangle = UIKeyboard.FrameBeginFromNotification(notification);

					UIView.Animate(0.3d, () =>
					{
						contentSize.Height += up ? rectangle.Height : -rectangle.Height;
						scrollView.ContentSize = contentSize;

						if (up)
						{
							var bottom = firstResponder.Frame.Y + firstResponder.Frame.Height + iKeyboardSupport.OffsetHeight;
							var spare = UIScreen.MainScreen.Bounds.Height - rectangle.Height;

							scrollView.ContentOffset = new PointF(0f, Math.Max(-64f, bottom - spare));
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