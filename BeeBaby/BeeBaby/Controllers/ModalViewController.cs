using System;
using MonoTouch.UIKit;
using PixateFreestyleLib;

namespace BeeBaby.Controllers
{
	public class ModalViewController : BaseViewController
	{
		UIView m_modalView;

		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Controllers.ModalViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
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
			Hide();
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
		public virtual void Show()
		{
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
		/// Hide this instance.
		/// </summary>
		public virtual void Hide()
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
	}
}