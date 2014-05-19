using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace BeeBaby
{
	public class KeyboardNotification
	{
		BaseViewController m_viewController;

		private KeyboardNotification(BaseViewController viewController)
		{
			m_viewController = viewController;

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
			m_viewController.StartEditing();

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
			if (m_viewController.IsKeyboardAnimation())
			{
				UIView firstResponder = GetFirstResponder(m_viewController.View);
				if (firstResponder != null)
				{
					RectangleF rectangle = UIKeyboard.FrameBeginFromNotification(notification);

					var bottom = (firstResponder.Frame.Y + firstResponder.Frame.Height);
					var height = (rectangle.Height - (m_viewController.View.Frame.Height - bottom));

					Scroller.Move(m_viewController.View, 0f, up ? -height : height);
				}
			}
		}

		/// <summary>
		/// Get the first input that responds view.
		/// </summary>
		/// <param name="view">View.</param>
		UIView GetFirstResponder(UIView view)
		{
			foreach (UIView element in view.Subviews)
			{
				if (element.IsFirstResponder || (element.GetType() == typeof(UIView) && GetFirstResponder(element) != null))
				{
					return element;
				}
			}
			return null;
		}

		/// <summary>
		/// Add the specified viewController.
		/// </summary>
		/// <param name="viewController">View controller.</param>
		public static KeyboardNotification Add(BaseViewController viewController)
		{
			return new KeyboardNotification(viewController);
		}
	}
}