using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using PixateFreestyleLib;

namespace BeeBaby.Controllers
{
	public partial class MomentModalViewController : ModalViewController
	{
		public MomentModalViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Show this instance.
		/// </summary>
		public override void Show()
		{
			btnCancel.ExtraTouchArea = 20;
			base.Show();
		}

		/// <summary>
		/// Close the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Close(UIButton sender)
		{
			Hide();
		}

		/// <summary>
		/// Sets the information.
		/// </summary>
		/// <param name="moment">Moment.</param>
		public void SetInformation(IMoment moment)
		{
			lblEvent.Text = moment.EventDescription;
			lblDescription.Text = moment.MomentDescription;
			imgEventBadge.SetStyleClass(moment.EventTagName);
		}
	}
}