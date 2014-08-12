using System;
using MonoTouch.UIKit;
using Domain.Moment;
using Domain.Baby;
using BeeBaby.ResourcesProviders;
using PixateFreestyleLib;

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
		/// <param name="baby">Baby.</param>
		/// <param name="backgroundImage">Background image.</param>
		public void SetInformation(Moment moment, Baby baby, UIImage backgroundImage)
		{
			ivwBackgroundImage.Image = backgroundImage;
			lblAge.Text = Baby.FormatAge(baby.BirthDateTime, moment.Date);
			lblDay.Text = moment.Date.ToString("dd");
			lblEvent.Text = moment.Event.Description;
			lblMonth.Text = moment.Date.ToString("MMM");
			lblWhere.Text = moment.Location.PlaceName;
			lblYear.Text = moment.Date.ToString("yyyy");
			imgEventBadge.SetStyleClass(moment.Event.TagName);

			vwLowerBackground.SetStyleClass(string.Concat("card-overlay-", moment.Event.TagName));
		}
	}
}