using System;
using MonoTouch.UIKit;

namespace BeeBaby
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
			var view = touch.View.Superview; 
			if (view.GetType() == typeof(ViewDatePicker))
			{
				ViewDatePicker viewDate = (ViewDatePicker) view;
				viewDate.IgnoreHide = true;
			}
			return true;
		}
	}
}