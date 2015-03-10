using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace BeeBaby.Animation
{
	public static class ViewAnimationExtension
	{
		/// <summary>
		/// Rotate the specified view, duration, isRepeat and finishedAction.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="isRepeat">Is repeat.</param>
		/// <param name="finishedAction">Finished action.</param>
		public static void Rotate(this UIView view, double duration = 1d, Func<bool> isRepeat = null, Action finishedAction = null)
		{
			var initialTransform = view.Transform;

			var options = UIViewAnimationOptions.AllowUserInteraction |
				UIViewAnimationOptions.CurveLinear;

			UIView.Animate(duration, 0d, options, () => {
				view.Transform = CGAffineTransform.MakeRotation((float) -Math.PI);
			}, () => {
				view.Transform = initialTransform;
				if (isRepeat == null || isRepeat())
				{
					view.Rotate(duration, isRepeat, finishedAction);
				}
				else if (finishedAction != null)
				{
					finishedAction();
				}
			});
		}
	}
}