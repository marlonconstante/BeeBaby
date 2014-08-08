using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public class Popover : View
	{
		public Popover(RectangleF frame) : base(frame)
		{
			MinY = 0f;
			Alpha = 0f;
			Layer.BorderWidth = 1f;
			Layer.BorderColor = UIColor.FromRGB(227, 227, 219).CGColor;
			CurrentViewController.View.AddSubview(this);
		}

		/// <summary>
		/// Show the specified point and animated.
		/// </summary>
		/// <param name="point">Point.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public void Show(PointF point, bool animated = true)
		{
			Hide(() => {
				if (point.X > UIScreen.MainScreen.Bounds.Width - Frame.Width)
				{
					point.X -= Frame.Width;
				}

				if (point.Y < MinY)
				{
					point.Y = MinY;
				}
				else if (point.Y > UIScreen.MainScreen.Bounds.Height - Frame.Height)
				{
					point.Y -= Frame.Height;
				}

				var frame = Frame;
				frame.X = point.X;
				frame.Y = point.Y;
				Frame = frame;

				UIView.Animate(animated ? 0.15d : 0d, () => {
					Alpha = 1f;
				});

				IsVisible = true;
			}, animated);
		}

		/// <summary>
		/// Hide the specified completion and animated.
		/// </summary>
		/// <param name="completion">Completion.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public void Hide(Action completion = null, bool animated = true)
		{
			UIView.Animate(animated ? 0.15d : 0d, () => {
				Alpha = 0f;
			}, () => {
				if (completion != null)
				{
					completion();
				}
				else
				{
					IsVisible = false;
				}
			});
		}

		/// <summary>
		/// Gets or sets the minimum y.
		/// </summary>
		/// <value>The minimum y.</value>
		public float MinY {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is visible.
		/// </summary>
		/// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
		public bool IsVisible { get; set; }

		/// <summary>
		/// Gets the current view controller.
		/// </summary>
		/// <value>The current view controller.</value>
		UIViewController CurrentViewController {
			get {
				return Windows.GetTopViewController(UIApplication.SharedApplication.Windows[0]);
			}
		}
	}
}