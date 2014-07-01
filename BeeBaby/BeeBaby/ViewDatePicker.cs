using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using Skahal.Infrastructure.Framework.Globalization;
using System.Collections.Generic;
using PixateFreestyleLib;

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

		public ViewDatePicker(IntPtr handle) : base(handle)
		{
			IgnoreHide = false;
			MoveScroll = false;
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
					m_datePicker = new UIDatePicker(new RectangleF(0f, 25f, Frame.Width, 162f));
					m_datePicker.Mode = m_mode;
					m_datePicker.Date = DateTime;
					m_datePicker.Hidden = true;

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
				m_datePicker.Hidden = !m_datePicker.Hidden;

				float height = (m_datePicker.Frame.Height - 35f) * (m_datePicker.Hidden ? -1f : 1f);

				UpdateScroll(height);

				RectangleF frame = Frame;
				frame.Height += height;
				Frame = frame;

				IgnoreHide = false;
			}
		}

		/// <summary>
		/// Updates the scroll.
		/// </summary>
		/// <param name="y">The y coordinate.</param>
		void UpdateScroll(float y)
		{
			if (MoveScroll)
			{
				Scroller.Move(this.Superview, 0f, -y);

				InvokeInBackground(() => {
					InvokeOnMainThread(() => {
						if (NextViews != null)
						{
							foreach (var view in NextViews)
							{
								Scroller.Move(view, 0f, y, false);
							}
						}
					});
				});
			}
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
			if (m_datePicker != null && !m_datePicker.Hidden && !IgnoreHide)
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
		/// Gets or sets the next views.
		/// </summary>
		/// <value>The next views.</value>
		public IList<UIView> NextViews {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BeeBaby.ViewDatePicker"/> move scroll.
		/// </summary>
		/// <value><c>true</c> if move scroll; otherwise, <c>false</c>.</value>
		public bool MoveScroll {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BeeBaby.ViewDatePicker"/> ignore hide.
		/// </summary>
		/// <value><c>true</c> if ignore hide; otherwise, <c>false</c>.</value>
		public bool IgnoreHide {
			get;
			set;
		}
	}
}