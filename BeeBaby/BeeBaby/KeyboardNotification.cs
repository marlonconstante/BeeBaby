using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace BeBabby
{
	public class KeyboardNotification
	{
		private UIView view;

		// Amount to scroll
		private float scrollAmount = 0.0f;

		// Extra offset
		private float offset = 18.0f;

		public KeyboardNotification(UIView view)
		{
			this.view = view;

			// Keyboard Up
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyboardUpNotification);

			// Keyboard Down
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardDownNotification);

			AddGestureRecognizer();
		}

		/// <summary>
		/// Add tap gesture recognizer.
		/// </summary>
		private void AddGestureRecognizer() {
			var gestureRecognizer = new UITapGestureRecognizer(() => view.EndEditing(true));
			view.AddGestureRecognizer(gestureRecognizer);
		}

		/// <summary>
		/// Keyboard Up Notification.
		/// </summary>
		/// <param name="notification">Notification.</param>
		private void KeyboardUpNotification(NSNotification notification)
		{
			UIView element = null;

			// Find what opened the keyboard
			foreach (UIView view in this.view.Subviews) {
				if (view.IsFirstResponder) {
					element = view;
					break;
				}
			}

			if (element != null) {
				// Get the keyboard size
				RectangleF rectangle = UIKeyboard.FrameBeginFromNotification(notification);

				// Calculate how far we need to scroll
				float bottom = (element.Frame.Y + element.Frame.Height + offset);
				scrollAmount = (rectangle.Height - (view.Frame.Size.Height - bottom));

				// Perform the scrolling
				ScrollTheView(true);
			}
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

		private void ScrollTheView(bool up)
		{
			// Scroll the view up or down
			UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
			UIView.SetAnimationDuration(0.3);

			RectangleF frame = view.Frame;
			if (up) {
				frame.Y -= scrollAmount;
			} else {
				frame.Y += scrollAmount;
			}
			view.Frame = frame;

			UIView.CommitAnimations();
		}
	}
}

