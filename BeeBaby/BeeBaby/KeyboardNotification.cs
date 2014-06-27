using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace BeeBaby
{
	public class KeyboardNotification : Notification
	{
		WeakReference m_weakResponder;

		protected KeyboardNotification()
		{
			// Keyboard Up
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyboardUpNotification);

			// Keyboard Down
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardDownNotification);
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

			MoveScroll(true, notification);
		}

		/// <summary>
		/// Keyboards down notification.
		/// </summary>
		/// <param name="notification">Notification.</param>
		void KeyboardDownNotification(NSNotification notification)
		{
			MoveScroll(false, notification);
		}

		/// <summary>
		/// Moves the scroll.
		/// </summary>
		/// <param name="up">If set to <c>true</c> up.</param>
		/// <param name="notification">Notification.</param>
		void MoveScroll(bool up, NSNotification notification)
		{
			var view = CurrentViewController.View;
			var firstResponder = m_weakResponder.Target as UIView;
			if (firstResponder != null && firstResponder is IKeyboardSupport)
			{
				var keyboardSupport = (IKeyboardSupport) firstResponder;
				if (keyboardSupport.IsKeyboardAnimation)
				{
					RectangleF rectangle = UIKeyboard.FrameBeginFromNotification(notification);
					var bottom = (firstResponder.Frame.Y + firstResponder.Frame.Height + keyboardSupport.OffsetHeight);
					if (view is ViewScrollable)
					{
						bottom += 64f;
					}
					var height = (rectangle.Height - (view.Frame.Height - bottom));

					Scroller.Move(view, 0f, up ? -height : height);
				}
			}
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
		/// Initialize this instance.
		/// </summary>
		public static KeyboardNotification Initialize()
		{
			return new KeyboardNotification();
		}
	}
}