using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace BeeBaby
{
	public class KeyboardNotification
	{
		UIView m_view;
		// Amount to scroll
		float m_scrollAmount = 0.0f;
		// Extra offset
		float m_offset = 18.0f;

		public KeyboardNotification(UIView view)
		{
			m_view = view;

			// Keyboard Up
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyboardUpNotification);

			// Keyboard Down
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardDownNotification);
		}

		/// <summary>
		/// Keyboard Up Notification.
		/// </summary>
		/// <param name="notification">Notification.</param>
		private void KeyboardUpNotification(NSNotification notification)
		{
			UIView firstResponder = GetFirstResponder(m_view);
			if (firstResponder != null)
			{
				// Get the keyboard size
				RectangleF rectangle = UIKeyboard.FrameBeginFromNotification(notification);

				// Calculate how far we need to scroll
				float bottom = (firstResponder.Frame.Y + firstResponder.Frame.Height + m_offset);
				m_scrollAmount = (rectangle.Height - (m_view.Frame.Size.Height - bottom));

				// Perform the scrolling
				ScrollTheView(true);
			}
		}

		/// <summary>
		/// Get the first input that responds view.
		/// </summary>
		/// <param name="view">View.</param>
		private UIView GetFirstResponder(UIView view)
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
		/// Keyboard Down Notification.
		/// </summary>
		/// <param name="notification">Notification.</param>
		private void KeyboardDownNotification(NSNotification notification)
		{
			// Perform the scrolling
			ScrollTheView(false);
		}

		/// <summary>
		/// Scrolls the view.
		/// </summary>
		/// <param name="up">If set to <c>true</c> up.</param>
		private void ScrollTheView(bool up)
		{
			// Scroll the view up or down
			UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
			UIView.SetAnimationDuration(0.3);

			RectangleF frame = m_view.Frame;
			frame.Y += up ? -m_scrollAmount : m_scrollAmount;
			m_view.Frame = frame;

			UIView.CommitAnimations();
		}
	}
}