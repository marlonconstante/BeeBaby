using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public partial class ViewDatePicker : UIView
	{
		UIDatePicker m_datePicker;
		UIButton m_button;
		UILabel m_label;
		string m_longDateMask;

		public ViewDatePicker(IntPtr handle) : base(handle)
		{
			IgnoreHide = false;
			m_longDateMask = "LongDateMask".Translate();

			foreach (UIView element in Subviews)
			{
				switch (element.GetType().Name)
				{
				case "UIDatePicker":
					m_datePicker = (UIDatePicker) element;
					break;
				case "UIButton":
					m_button = (UIButton) element;
					break;
				case "UILabel":
					m_label = (UILabel) element;
					break;
				default:
					break;
				}
			}

			m_button.TouchUpInside += (sender, e) => UpdateFrame();
			m_datePicker.ValueChanged += (s, args) => {
				UpdateInfo();
			};
		}

		/// <summary>
		/// Updates the frame.
		/// </summary>
		public void UpdateFrame()
		{
			m_datePicker.Hidden = !m_datePicker.Hidden;

			float height = m_datePicker.Frame.Height - 35f;
			RectangleF frame = Frame;
			frame.Height += (m_datePicker.Hidden) ? -height : height;

			Frame = frame;

			IgnoreHide = false;
		}

		/// <summary>
		/// Updates the information.
		/// </summary>
		public void UpdateInfo()
		{
			switch (m_datePicker.Mode)
			{
			case UIDatePickerMode.DateAndTime:
				m_button.SetTitle(GetText(m_longDateMask), UIControlState.Normal);
				m_label.Text = GetText("HH:mm");
				break;
			case UIDatePickerMode.Date:
				m_button.SetTitle(GetText(m_longDateMask), UIControlState.Normal);
				break;
			case UIDatePickerMode.Time:
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
			if (!m_datePicker.Hidden && !IgnoreHide)
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
			return GetLocalTime().ToString(mask, System.Globalization.DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>
		/// Gets the local time.
		/// </summary>
		/// <returns>The local time.</returns>
		public DateTime GetLocalTime()
		{
			return GetDateTime().ToLocalTime();
		}

		/// <summary>
		/// Gets the date time.
		/// </summary>
		/// <returns>The date time.</returns>
		public DateTime GetDateTime()
		{
			DateTime dateTime = m_datePicker.Date;
			return dateTime.ToUniversalTime();
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