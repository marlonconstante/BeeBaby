using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public class Popover : View
	{
		public Popover(RectangleF frame) : base(frame)
		{
			Alpha = 0f;
			Layer.BorderWidth = 1f;
			Layer.BorderColor = UIColor.FromRGB(227, 227, 219).CGColor;
			CurrentViewController.View.AddSubview(this);
		}

		public bool IsVisible { get; set; }

		/// <summary>
		/// Show the specified point and animated.
		/// </summary>
		/// <param name="point">Point.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public void Show(PointF point, bool resize = false, bool animated = true)
		{
			Hide(() =>
			{
				var frame = Frame;

				if (resize)
				{
					frame.Height = Subviews[0].Frame.Height;
				}

				if (point.X > UIScreen.MainScreen.Bounds.Width - Frame.Width)
				{
					point.X -= Frame.Width;
				}

				if (point.Y > UIScreen.MainScreen.Bounds.Height - Frame.Height)
				{
					point.Y -= Frame.Height;
				}

				if (resize)
				{

				}

				frame.X = point.X;
				frame.Y = point.Y;
				Frame = frame;

				UIView.Animate(animated ? 0.15d : 0d, () =>
				{
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
			UIView.Animate(animated ? 0.15d : 0d, () =>
			{
				Alpha = 0f;
			}, () =>
			{
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
		/// Gets the current view controller.
		/// </summary>
		/// <value>The current view controller.</value>
		UIViewController CurrentViewController
		{
			get
			{
				return Windows.GetTopViewController(UIApplication.SharedApplication.Windows[0]);
			}
		}
	}
}