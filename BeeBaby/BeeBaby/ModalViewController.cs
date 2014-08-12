using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using PixateFreestyleLib;

namespace BeeBaby
{
	public partial class ModalViewController : BaseViewController
	{
		public ModalViewController (IntPtr handle) : base (handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			btnCancel.ExtraTouchArea = 20;
		}

		/// <summary>
		/// Ends the editing.
		/// </summary>
		public override void EndEditing()
		{
			base.EndEditing();
			Close(null);
		}

		/// <summary>
		/// Close the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Close(UIButton sender)
		{
			ShowProgressWhilePerforming(() => {
				PresentingViewController.DismissViewController(true, null);
			}, false);
		}

		/// <summary>
		/// Sets the information.
		/// </summary>
		/// <param name="moment">Moment.</param>
		public void SetInformation(Moment moment)
		{
			lblEvent.Text = moment.Event.Description;
			lblDescription.Text = moment.Description;
			imgEventBadge.SetStyleClass(moment.Event.TagName);
		}
	}
}