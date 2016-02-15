using System;
using UIKit;

namespace BeeBaby.Gestures
{
	public class GestureRecognizerDelegate : UIGestureRecognizerDelegate
	{
		public GestureRecognizerDelegate()
		{
		}

		/// <summary>
		/// Should Receive Touch Event.
		/// </summary>
		/// <returns><c>true</c>, if receive touch was shoulded, <c>false</c> otherwise.</returns>
		/// <param name="recognizer">Recognizer.</param>
		/// <param name="touch">Touch.</param>
		public override bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
		{
			return !IsExclusiveTouch(touch.View);
		}

		/// <summary>
		/// Determines whether this instance is exclusive touch the specified view.
		/// </summary>
		/// <returns><c>true</c> if this instance is exclusive touch the specified view; otherwise, <c>false</c>.</returns>
		/// <param name="view">View.</param>
		bool IsExclusiveTouch(UIView view)
		{
			while (view != null)
			{
				if (view.ExclusiveTouch)
				{
					return true;
				}
				view = view.Superview;
			}
			return false;
		}
	}
}