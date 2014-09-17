using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Domain.Moment;
using PixateFreestyleLib;

namespace BeeBaby.Controllers
{
	public partial class ModalViewController : BaseViewController
	{
		UIView m_modalView;

		public ModalViewController(IntPtr handle) : base(handle)
		{
			View.Alpha = 0f;

			m_modalView = new UIView(UIScreen.MainScreen.Bounds);
			m_modalView.SetStyleClass("view modal-background");
			m_modalView.AddGestureRecognizer(EditingTapGestureRecognizer);
			m_modalView.Alpha = 0f;

			RootViewController.View.AddSubview(m_modalView);
		}

		/// <summary>
		/// Touchs the action.
		/// </summary>
		public override void TouchAction()
		{
			Close(btnCancel);
		}

		/// <summary>
		/// Determines whether this instance is show progress.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		public override bool IsShowProgress()
		{
			return false;
		}

		/// <summary>
		/// Show this instance.
		/// </summary>
		public void Show()
		{
			btnCancel.ExtraTouchArea = 20;
			foreach (var subview in View.Subviews)
			{
				subview.ExclusiveTouch = true;
			}

			UIView.Animate(0.3d, () => {
				m_modalView.AddSubview(View);
				m_modalView.Alpha = 1f;
			}, () => {
				UIView.Animate(0.3d, () => {
					View.Alpha = 1f;
				});
			});
		}

		/// <summary>
		/// Close the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Close(UIButton sender)
		{
			UIView.Animate(0.3d, () => {
				View.Alpha = 0f;
			}, () => {
				UIView.Animate(0.3d, () => {
					m_modalView.Alpha = 0f;
					View.RemoveFromSuperview();
				});
			});
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