using System;
using MonoTouch.UIKit;
using Domain.Moment;
using Domain.Baby;

namespace BeeBaby
{
	public partial class ImageShareViewController : UIViewController
	{
		public ImageShareViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Sets the information.
		/// </summary>
		/// <param name="moment">Moment.</param>
		/// <param name="backgroundImage">Background image.</param>
		/// <param name="baby">Baby.</param>
		public void SetInformation(Moment moment, UIImage backgroundImage, Baby baby)
		{
			ivwBackgroundImage.Image = backgroundImage;
			lblAge.Text = baby.AgeInWords;
			lblDay.Text = moment.Date.ToString("dd");
			lblEvent.Text = moment.Event.Description;
			lblMonth.Text = moment.Date.ToString("MMMMM");
			lblWhere.Text = "Lighthouse SA";
			lblYear.Text = moment.Date.ToString("yyyy");
		}
	}
}
