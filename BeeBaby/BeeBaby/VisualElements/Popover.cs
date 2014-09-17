using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;
using PixateFreestyleLib;
using Application;
using BeeBaby.Util;
using BeeBaby.Proxy;

namespace BeeBaby.VisualElements
{
	public class Popover<TDelegateController, TEventArgs> : View 
		where TDelegateController : class
		where TEventArgs : EventArgs
	{
		const float c_lineHeight = 0.5f;
		const float c_padding = 10f;
		float m_viewHeight;
		IList<UIView> m_menuItems;

		public Popover(RectangleF frame) : base(frame)
		{
			m_viewHeight = 0f;
			m_menuItems = new List<UIView>();
			MinY = 0f;
			Alpha = 0f;
			ClipsToBounds = true;
			BackgroundColor = UIColor.White;

			Layer.CornerRadius = 8f;
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
				frame.X = point.X + c_padding;
				frame.Y = point.Y;
				frame.Height = m_viewHeight;
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
		/// Adds the popover item.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="iconClass">Icon class.</param>
		/// <param name="addLine">If set to <c>true</c> add line.</param>
		/// <param name="buttonHeight">Button height.</param>
		/// <param name="proxy">Proxy.</param>
		public void AddPopoverItem(string title, string iconClass, bool addLine, float buttonHeight, EventProxy<TDelegateController, EventArgs> proxy)
		{
			var button = new Button(new RectangleF(0f, m_viewHeight, Frame.Width - c_padding, buttonHeight));
			button.TitleEdgeInsets = new UIEdgeInsets(0.5f, 17f, 0f, 0f);
			button.ImageEdgeInsets = new UIEdgeInsets(0f, 10f, 0f, 0f);
			button.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			button.VerticalAlignment = UIControlContentVerticalAlignment.Center;
			button.SetTitle(title, UIControlState.Normal);
			button.SetStyleClass("button-popover " + iconClass);
			button.TouchUpInside += proxy.HandleEvent;

			m_menuItems.Add(button);

			if (addLine)
			{
				var line = new UIView(new RectangleF(c_padding, m_viewHeight + buttonHeight, Frame.Width - (c_padding * 2f), c_lineHeight));
				line.BackgroundColor = UIColor.FromRGB(200, 199, 204);

				m_menuItems.Add(line);
				m_viewHeight += c_lineHeight;
			}

			m_viewHeight += button.Frame.Height;
		}

		/// <summary>
		/// Gets the menu itens.
		/// </summary>
		/// <value>The menu itens.</value>
		public IList<UIView> MenuItems {
			get {
				return m_menuItems;
			}
		}

		/// <summary>
		/// Gets or sets the minimum y.
		/// </summary>
		/// <value>The minimum y.</value>
		public float MinY { get; set; }

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