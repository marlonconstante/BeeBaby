using System;
using MonoTouch.UIKit;
using Domain.Moment;
using Domain.Baby;
using BeeBaby.ResourcesProviders;
using PixateFreestyleLib;

namespace BeeBaby.Controllers
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
		public void SetInformation(IMoment moment, UIImage backgroundImage)
		{
			ivwBackgroundImage.Image = backgroundImage;
			lblAge.Text = Baby.FormatAge(moment.BabyBirthDateTime, moment.MomentDate);
			lblDay.Text = moment.MomentDate.ToString("dd");
			lblEvent.LineHeight = lblEvent.Font.PointSize + 0.6f;
			lblEvent.Text = moment.EventDescription;
			lblMonth.Text = moment.MomentDate.ToString("MMM");
			lblWhere.Text = Location.NameOrDefault(moment.LocationName);
			lblYear.Text = moment.MomentDate.ToString("yyyy");
			imgEventBadge.SetStyleClass(moment.EventTagName);

			var frame = vwImageBadge.Frame;
			frame.Y = (lblEvent.Frame.Height / 2) - (frame.Height / 2);
			vwImageBadge.Frame = frame;

			vwLowerBackground.SetStyleClass(string.Concat("card-overlay-", moment.EventTagName));
		}
	}
}