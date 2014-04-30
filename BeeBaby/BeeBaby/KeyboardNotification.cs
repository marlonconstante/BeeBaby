using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace BeeBaby
{
	public class KeyboardNotification
	{
		ViewController m_viewController;
		// Amount to scroll
		float m_scrollAmount = 0.0f;

		private KeyboardNotification(ViewController viewController)
		{
			m_viewController = viewController;

			// Keyboard Up
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyboardUpNotification);

			// Keyboard Down
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardDownNotification);
		}

		/// <summary>
		/// Keyboard Up Notification.
		/// </summary>
		/// <param name="notification">Notification.</param>
		void KeyboardUpNotification(NSNotification notification)
		{
			m_viewController.StartEditing();

			if (m_viewController.IsKeyboardAnimation())
			{
				UIView firstResponder = GetFirstResponder(m_viewController.View);
				if (firstResponder != null)
				{
					// Get the keyboard size
					RectangleF rectangle = UIKeyboard.FrameBeginFromNotification(notification);

					// Calculate how far we need to scroll
					float bottom = (firstResponder.Frame.Y + firstResponder.Frame.Height);
					m_scrollAmount = (rectangle.Height - (m_viewController.View.Frame.Size.Height - bottom));

					// Perform the scrolling
					ScrollTheView(true);
				}
			}
		}

		/// <summary>
		/// Keyboard Down Notification.
		/// </summary>
		/// <param name="notification">Notification.</param>
		void KeyboardDownNotification(NSNotification notification)
		{
			if (m_viewController.IsKeyboardAnimation())
			{
				// Perform the scrolling
				ScrollTheView(false);
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
		/// Scrolls the view.
		/// </summary>
		/// <param name="up">If set to <c>true</c> up.</param>
		void ScrollTheView(bool up)
		{
			// Scroll the view up or down
			UIView.BeginAnimations(string.Empty, IntPtr.Zero);
			UIView.SetAnimationDuration(0.3);

			RectangleF frame = m_viewController.View.Frame;
			frame.Y += up ? -m_scrollAmount : m_scrollAmount;
			m_viewController.View.Frame = frame;

			UIView.CommitAnimations();
		}

		/// <summary>
		/// Add the specified viewController.
		/// </summary>
		/// <param name="viewController">View controller.</param>
		public static KeyboardNotification Add(ViewController viewController)
		{
			return new KeyboardNotification(viewController);
		}
	}
}