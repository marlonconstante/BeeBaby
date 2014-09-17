using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using Skahal.Infrastructure.Framework.Globalization;
using System.Collections.Generic;
using PixateFreestyleLib;
using MonoTouch.CoreAnimation;
using BeeBaby.Util;

namespace BeeBaby
{
	public partial class ViewDatePicker : View
	{
		UILabel m_label;
		UIImageView m_image;
		UIButton m_button;
		UIDatePicker m_datePicker;
		UIDatePickerMode m_mode;
		DateTime m_dateTime = DateTime.Now;
		string m_longDateMask;
		EventHandler m_clicked;

		public ViewDatePicker(IntPtr handle) : base(handle)
		{
			ExclusiveTouch = true;
			m_longDateMask = "LongDateMask".Translate();
		}

		/// <summary>
		/// Init the specified mode.
		/// </summary>
		/// <param name="mode">Mode.</param>
		public void Init(UIDatePickerMode mode)
		{
			m_mode = mode;

			AddLabel();
			AddImage();
			AddButton();
			AddDatePicker();
		}

		/// <summary>
		/// Determines whether this instance is increase touch area.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		public override bool IsIncreaseTouchArea()
		{
			return false;
		}

		/// <summary>
		/// Adds the label.
		/// </summary>
		void AddLabel()
		{
			if (m_mode == UIDatePickerMode.DateAndTime)
			{
				m_label = new UILabel(new RectangleF(247f, 12f, 63f, 21f));
				m_label.TextAlignment = UITextAlignment.Right;
				AddSubview(m_label);
			}
		}

		/// <summary>
		/// Adds the image.
		/// </summary>
		void AddImage()
		{
			m_image = new UIImageView(new RectangleF(9f, 14f, 16f, 16f));
			m_image.ContentMode = UIViewContentMode.Center;
			AddSubview(m_image);
		}

		/// <summary>
		/// Adds the button.
		/// </summary>
		void AddButton()
		{
			m_button = new UIButton(new RectangleF(0f, 0f, Frame.Width, Frame.Height));
			m_button.ContentEdgeInsets = new UIEdgeInsets(1f, 33f, 0f, 0f);
			m_button.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			m_button.SetStyleClass("highlighted");

			var proxy = new EventProxy<ViewDatePicker, EventArgs>(this);
			proxy.Action = (target, sender, args) => {
				target.UpdateFrame();
				if (target.m_clicked != null)
				{
					target.m_clicked.Invoke(sender, args);
				}
			};
			m_button.TouchUpInside += proxy.HandleEvent;

			AddSubview(m_button);
		}

		/// <summary>
		/// Adds the date picker.
		/// </summary>
		void AddDatePicker()
		{
			InvokeInBackground(() => {
				InvokeOnMainThread(() => {
					m_datePicker = new UIDatePicker(new RectangleF(0f, Frame.Height, Frame.Width, 216f));
					m_datePicker.Mode = m_mode;
					m_datePicker.Date = DateTime;
					m_datePicker.Hidden = true;

					var layer = CALayer.Create();
					layer.Opacity = 0.3f;
					layer.Frame = new RectangleF(0f, 0f, Frame.Width, 1f);
					layer.BackgroundColor = UIColor.FromRGB(185, 201, 197).CGColor;
					m_datePicker.Layer.AddSublayer(layer);

					m_datePicker.Layer.ShadowPath = UIBezierPath.FromRect(m_datePicker.Bounds).CGPath;
					m_datePicker.Layer.ShadowColor = UIColor.LightGray.CGColor;
					m_datePicker.Layer.ShadowOffset = new SizeF(0f, 0f);
					m_datePicker.Layer.ShadowOpacity = 0.05f;
					m_datePicker.Layer.ShadowRadius = 1f;

					var proxy = new EventProxy<ViewDatePicker, EventArgs>(this);
					proxy.Action = (target, sender, args) => {
						FlurryAnalytics.Flurry.LogEvent("Mudou a data.");
						target.DateTime = ((UIDatePicker) sender).Date;
						target.UpdateInfo();
					};
					m_datePicker.ValueChanged += proxy.HandleEvent;

					AddSubview(m_datePicker);
				});
			});
		}

		/// <summary>
		/// Updates the frame.
		/// </summary>
		void UpdateFrame()
		{
			if (m_datePicker != null)
			{
				if (m_datePicker.Hidden)
				{
					Superview.EndEditing(true);
				}

				m_datePicker.Hidden = !m_datePicker.Hidden;

				float height = m_datePicker.Frame.Height * (m_datePicker.Hidden ? -1f : 1f);
				AdjustConstraints(height);
			}
		}

		/// <summary>
		/// Adjusts the constraints.
		/// </summary>
		/// <param name="constant">Constant.</param>
		void AdjustConstraints(float constant)
		{
			UIView.Animate(0.3d, () => {
				Views.ChangeHeightAndDragNextViews(this, constant);
			}, () => {
				if (Superview is UIScrollView)
				{
					var scrollView = (UIScrollView) Superview;

					var contentSize = scrollView.ContentSize;
					contentSize.Height += constant;
					scrollView.ContentSize = contentSize;

					if (!KeyboardNotification.KeyboardVisible)
					{
						scrollView.ScrollRectToVisible(new RectangleF(0f, Frame.Y, 1f, UIScreen.MainScreen.Bounds.Height - 64f), true);
					}
				}
			});
		}

		/// <summary>
		/// Updates the information.
		/// </summary>
		public void UpdateInfo()
		{
			switch (m_mode)
			{
			case UIDatePickerMode.DateAndTime:
				m_label.Text = GetText("HH:mm");
				m_image.SetStyleClass("calendar");
				m_button.SetTitle(GetText(m_longDateMask), UIControlState.Normal);
				break;
			case UIDatePickerMode.Date:
				m_image.SetStyleClass("calendar");
				m_button.SetTitle(GetText(m_longDateMask), UIControlState.Normal);
				break;
			case UIDatePickerMode.Time:
				m_image.SetStyleClass("clock");
				m_button.SetTitle(GetText("HH:mm"), UIControlState.Normal);
				break;
			default:
				break;
			}
		}

		/// <summary>
		/// Hide this instance.
		/// </summary>
		public void Hide()
		{
			if (m_datePicker != null && !m_datePicker.Hidden)
			{
				UpdateFrame();
			}
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="mask">Mask.</param>
		public string GetText(string mask)
		{
			return DateTime.ToString(mask, System.Globalization.DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>
		/// Gets or sets the date time.
		/// </summary>
		/// <value>The date time.</value>
		public DateTime DateTime {
			get {
				return m_dateTime.ToLocalTime();
			}
			set {
				m_dateTime = value.ToUniversalTime();
			}
		}

		/// <summary>
		/// Occurs when clicked.
		/// </summary>
		public event EventHandler Clicked {
			add {
				m_clicked += value;
			}
			remove {
				m_clicked -= value;
			}
		}
	}
}