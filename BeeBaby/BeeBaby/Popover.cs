using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;
using PixateFreestyleLib;
using Application;

namespace BeeBaby
{
	public class Popover<TDelegateController, TEventArgs> : View 
		where TDelegateController : class
		where TEventArgs : EventArgs
	{
		/// <summary>
		/// The height of the line.
		/// </summary>
		const float lineHeight = 3f;


		/// <summary>
		/// Gets the menu itens.
		/// </summary>
		/// <value>The menu itens.</value>
		public IList<UIView> MenuItems { get { return m_menuItems; } }


		/// <summary>
		/// The m menu items.
		/// </summary>
		IList<UIView> m_menuItems;

		/// <summary>
		/// The height of the m view.
		/// </summary>
		float m_viewHeight;

		public Popover(RectangleF frame) : base(frame)
		{
			MinY = 0f;
			Alpha = 0f;
			Layer.BorderWidth = 1f;
			Layer.BorderColor = UIColor.FromRGB(227, 227, 219).CGColor;
			CurrentViewController.View.AddSubview(this);
			m_viewHeight = 0;

			m_menuItems = new List<UIView>();
		}

		/// <summary>
		/// Show the specified point and animated.
		/// </summary>
		/// <param name="point">Point.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public void Show(PointF point, bool animated = true)
		{
			Hide(() =>
			{
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
				frame.Height = m_viewHeight;
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
		/// Gets or sets the minimum y.
		/// </summary>
		/// <value>The minimum y.</value>
		public float MinY
		{
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
		UIViewController CurrentViewController
		{
			get
			{
				return Windows.GetTopViewController(UIApplication.SharedApplication.Windows[0]);
			}
		}

		public void AddPopoverItem(string title, string iconClass, bool bottonLine, float buttonHeight, EventProxy<TDelegateController, EventArgs> proxy)
		{
			var button = new Button(new RectangleF(0f, m_viewHeight, 220f, buttonHeight));
			button.TitleEdgeInsets = new UIEdgeInsets(1f, 17f, 0f, 0f);
			button.ImageEdgeInsets = new UIEdgeInsets(0f, 10f, 0f, 0f);
			button.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			button.VerticalAlignment = UIControlContentVerticalAlignment.Center;
			button.SetTitle(title, UIControlState.Normal);
			button.SetStyleClass("button-popover " + iconClass);

			UIImageView line = null;

			if (bottonLine)
			{
				line = new UIImageView(new RectangleF(0f, m_viewHeight + buttonHeight, 220f, lineHeight));
				line.Image = new UIImage("separator.png");
			}

			button.TouchUpInside += proxy.HandleEvent;

			m_menuItems.Add(button);
			m_viewHeight += button.Frame.Height;

			if (bottonLine)
			{
				m_menuItems.Add(line);
				m_viewHeight += line.Frame.Height;
			}
		}
	}
}